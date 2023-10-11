using System;
using System.Threading.Tasks;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Controllers;
using Windows.Devices.Midi;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano.TestApp
{
    public sealed partial class MidiPlayerPage : Page
    {
        // Synthesizer
        MidiSynthesizer MidiSynthesizer;
        // Track
        TrackCollection TrackCollection;
        KeySignature TrackKeySignature = new KeySignature(4, 4);
        TimeSignature TrackTimeSignature = new TimeSignature(4, 4);
        Ticks TrackTicks;
        Tempo TrackTempo;
        TempoDuration TrackDuration;
        // Player
        MidiNote Note;
        readonly ITickPlayer Player = new TickPlayer();

        ~MidiPlayerPage() => this.MidiSynthesizer?.Dispose();
        public MidiPlayerPage()
        {
            this.InitializeComponent(); 
            this.TrackTicks = new Ticks(this.TrackTimeSignature, 480);
            this.TrackTempo = new Tempo(this.TrackTicks, 120);
            this.TrackDuration = new TempoDuration(this.TrackTempo, 120);
            this.BeatTextBlock.Text = this.Player.BeatChar;
            this.Player.TickBeat += (s, e) => this.BeatTextBlock.Text = e;
            this.Player.TickProgress += (s, e) =>
            {
                if (this.TrackCollection is null) return;

                if (this.Player.PositionMilliseconds >= this.TrackDuration.DurationMilliseconds)
                {
                    this.Player.Stop();
                }

                this.Progress();
            };
            this.PauseButton.Click += (s, e) =>
            {
                if (this.TrackCollection is null) return;

                if (this.Player.IsPlaying)
                {
                    this.Player.Pause();
                }
            };
            this.PlayButton.Click += (s, e) =>
            {
                if (this.TrackCollection is null) return;

                if (this.Player.IsPlaying is false)
                {
                    this.Player.Play();
                    this.Play();
                }
            };
            this.OpenFileButton.Click += async (s, e) =>
            {
                // Picker
                FileOpenPicker openPicker = new FileOpenPicker
                {
                    ViewMode = PickerViewMode.Thumbnail,
                    SuggestedStartLocation = default,
                    FileTypeFilter =
                    {
                        ".mid", ".midi"
                    }
                };

                // File
                StorageFile file = await openPicker.PickSingleFileAsync();
                if (file is null) return;

                this.Run1.Text = file.Name;
                using (IRandomAccessStream stream = await file.OpenAsync(default))
                {
                    this.TrackCollection = new TrackCollection(stream);
                }
            };
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.MidiSynthesizer?.Dispose();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.MidiSynthesizer?.Dispose();
            this.MidiSynthesizer = await MidiSynthesizer.CreateAsync();
        }

        private void Stop()
        {
            this.ProgressBar.Value = 0;
            this.Run2.Text = "00:00.00";
            this.Rectangle.Width = 0;
        }

        private void Progress()
        {
            this.ProgressBar.Value = this.TrackDuration.GetPercent(this.Player.PositionMilliseconds);
            this.Run2.Text = this.Player.Position.ToString();
            this.Rectangle.Width = (int)this.Note;
        }

        private void Play()
        {
            foreach (ContentControl item in this.TrackCollection)
            {
                if (item.Content is Track track)
                {
                    this.Play(track);
                }
            }
        }

        private async void Play(Track track)
        {
            long positionMilliseconds = this.Player.PositionMilliseconds;
            foreach (ContentControl item in track.Notes)
            {
                if (this.Player.IsPlaying is false)
                {
                    return;
                }

                if (item.Content is MidiMessage message)
                {
                    long delayMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime) - positionMilliseconds;

                    if (delayMilliseconds < 0)
                    {
                        continue;
                    }
                    else if (delayMilliseconds == 0)
                    {
                        positionMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime);

                        this.MidiSynthesizer.SendMessage(message);
                        this.Note = message.Note;
                    }
                    else if (delayMilliseconds > 0)
                    {
                        positionMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime);

                        await Task.Delay((int)delayMilliseconds);
                        this.MidiSynthesizer.SendMessage(message);
                        this.Note = message.Note;
                    }
                    else if (delayMilliseconds > 20) // Async Position
                    {
                        positionMilliseconds = this.Player.PositionMilliseconds;
                        delayMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime) - positionMilliseconds;

                        await Task.Delay((int)delayMilliseconds);
                        this.MidiSynthesizer.SendMessage(message);
                        this.Note = message.Note;
                    }
                }
            }
        }
    }
}