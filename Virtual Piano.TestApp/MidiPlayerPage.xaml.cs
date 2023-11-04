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
        readonly IPlayer Player = new Player();

        ~MidiPlayerPage() => this.MidiSynthesizer?.Dispose();
        public MidiPlayerPage()
        {
            this.InitializeComponent();
            this.TrackTicks = new Ticks(this.TrackTimeSignature, 480);
            this.TrackTempo = new Tempo(this.TrackTicks, 120);
            this.TrackDuration = new TempoDuration(this.TrackTempo, 120);
            this.BeatTextBlock.Text = this.Player.BeatChar;
            this.Player.Beat += (s, e) => this.BeatTextBlock.Text = e;
            this.Player.Tick += (s, e) =>
            {
                if (this.TrackCollection is null) return;

                if (this.Player.PositionMilliseconds >= this.TrackDuration.DurationMilliseconds)
                {
                    this.Player.Reset();
                }

                this.Progress();
            };
            this.Player.CurrentStateChanged += (s, e) =>
            {
                switch (e)
                {
                    case Windows.Media.Playback.MediaPlaybackState.None:
                        break;
                    case Windows.Media.Playback.MediaPlaybackState.Playing:
                        break;
                    default:
                        break;
                }
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
                    this.Initialize(new TrackCollection(stream));
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


        private void Initialize(TrackCollection tracks)
        {
            // Track
            this.TrackCollection = tracks;
            this.TrackKeySignature = new KeySignature(tracks.SharpsFlats, tracks.MajorMinor);
            this.TrackTimeSignature = new TimeSignature(tracks.Numerator, tracks.Denominator);
            this.TrackTicks = new Ticks(this.TrackTimeSignature, tracks.DeltaTicksPerQuarterNote);
            this.TrackTempo = new Tempo(this.TrackTicks, tracks.Tempo);
            this.TrackDuration = new TempoDuration(this.TrackTempo, tracks.Duration);
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
                this.Play(item);
            }
        }

        private void Play(ContentControl item)
        {
            if (item.Content is Track track)
            {
                this.Programs(track);
                this.Play(track);
                foreach (var item2 in track.Controllers)
                {
                    this.Controllers(item2.Value);
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
                    }
                    else if (delayMilliseconds > 0)
                    {
                        positionMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime);

                        await Task.Delay((int)delayMilliseconds);
                        this.MidiSynthesizer.SendMessage(message);
                    }
                    else if (delayMilliseconds > 20) // Async Position
                    {
                        positionMilliseconds = this.Player.PositionMilliseconds;
                        delayMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime) - positionMilliseconds;

                        await Task.Delay((int)delayMilliseconds);
                        this.MidiSynthesizer.SendMessage(message);
                    }
                }
            }
        }

        private async void Programs(Track track)
        {
            long positionMilliseconds = this.Player.PositionMilliseconds;
            foreach (ContentControl item in track.Programs)
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

        private async void Controllers(ControllerCollection messages)
        {
            long positionMilliseconds = this.Player.PositionMilliseconds;
            foreach (MidiMessage message in messages)
            {
                if (this.Player.IsPlaying is false)
                {
                    return;
                }

                long delayMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime) - positionMilliseconds;

                if (delayMilliseconds < 0)
                {
                    continue;
                }
                else if (delayMilliseconds == 0)
                {
                    positionMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime);

                    this.MidiSynthesizer.SendMessage(message);
                }
                else if (delayMilliseconds > 0)
                {
                    positionMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime);

                    await Task.Delay((int)delayMilliseconds);
                    this.MidiSynthesizer.SendMessage(message);
                }
                else if (delayMilliseconds > 20) // Async Position
                {
                    positionMilliseconds = this.Player.PositionMilliseconds;
                    delayMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime) - positionMilliseconds;

                    await Task.Delay((int)delayMilliseconds);
                    this.MidiSynthesizer.SendMessage(message);
                }
            }
        }
    }
}