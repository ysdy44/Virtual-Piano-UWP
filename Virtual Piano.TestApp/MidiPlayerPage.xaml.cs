using System;
using System.Threading.Tasks;
using Virtual_Piano.Elements;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Controllers;
using Virtual_Piano.Midi.Core;
using Windows.Devices.Midi;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano.TestApp
{
    public sealed partial class MidiPlayerPage : Page
    {
        TrackCollection Collections;
        MidiSynthesizer Synthesizer;

        MidiNote Note;
        readonly ITickPlayer Player = new TickPlayer();

        public MidiPlayerPage()
        {
            this.InitializeComponent();
            this.BeatTextBlock.Text = this.Player.BeatChar;
            this.Player.TickBeat += (s, e) => this.BeatTextBlock.Text = e;
            this.Player.TickProgress += (s, e) =>
            {
                if (this.Collections is null) return;

                if (this.Player.PositionMilliseconds >= this.Collections.Duration)
                {
                    this.Player.Stop();
                }

                this.Progress();
            };
            this.PauseButton.Click += (s, e) =>
            {
                if (this.Collections is null) return;

                if (this.Player.IsPlaying)
                {
                    this.Player.Pause();
                }
            };
            this.PlayButton.Click += (s, e) =>
            {
                if (this.Collections is null) return;

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
                using (var stream = await file.OpenAsync(default))
                {
                    this.Collections = new TrackCollection(stream);
                }
            };
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.Synthesizer?.Dispose();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Synthesizer?.Dispose();
            this.Synthesizer = await MidiSynthesizer.CreateAsync();
        }

        private void Reset()
        {
            this.ProgressBar.Value = 0;
            this.Run2.Text = "00:00.00";
            this.Rectangle.Width = 0;
        }
        private void Progress()
        {
            this.ProgressBar.Value = (double)this.Player.PositionMilliseconds * 100 / this.Collections.Duration;
            this.Run2.Text = this.Player.Position.ToString();
            this.Rectangle.Width = (int)this.Note;
        }

        private void Play()
        {
            foreach (ContentControl item in this.Collections)
            {
                if (item.Tag is TrackInfo trackInfo)
                {
                    this.PlayTrack(trackInfo);
                }
            }
        }
        private async void PlayTrack(TrackInfo trackInfo)
        {
            foreach (ContentControl item in trackInfo.Notes)
            {
                if (this.Player.IsPlaying is false)
                {
                    return;
                }

                if (item.Tag is MidiMessage message)
                {
                    int delay = (int)(message.AbsoluteTime - this.Player.PositionMilliseconds);
                    if (delay > 0)
                    {
                        await Task.Delay(delay);
                        this.Synthesizer.SendMessage(message);
                        this.Note = message.Note;
                    }
                    else if (delay == 0)
                    {
                        this.Synthesizer.SendMessage(message);
                        this.Note = message.Note;
                    }
                }
            }
        }
    }
}