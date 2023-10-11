using System;
using System.Linq;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Controllers;
using Virtual_Piano.Midi.Core;
using Windows.Devices.Midi;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano.TestApp
{
    public sealed partial class TrackNotePage : Page
    {
        // Synthesizer
        MidiSynthesizer MidiSynthesizer;
        // Track
        int TrackIndex = -1;
        TrackCollection TrackCollection;
        KeySignature TrackKeySignature = new KeySignature(4, 4);
        TimeSignature TrackTimeSignature = new TimeSignature(4, 4);
        Ticks TrackTicks;
        Tempo TrackTempo;
        TempoDuration TrackDuration;
        // Player
        double Offset;

        double X;
        double Y;
        UIElement Source;

        ~TrackNotePage() => this.MidiSynthesizer?.Dispose();
        public TrackNotePage()
        {
            this.InitializeComponent();
            this.TrackTicks = new Ticks(this.TrackTimeSignature, 480);
            this.TrackTempo = new Tempo(this.TrackTicks, 120);
            this.TrackDuration = new TempoDuration(this.TrackTempo, 120);
            this.TrackNotePanel.ManipulationStarted += (s, e) =>
            {
                this.Source = e.OriginalSource as UIElement;
                if (this.Source is null) return;

                this.X = Canvas.GetLeft(this.Source);
                this.Y = Canvas.GetTop(this.Source);
            };
            this.TrackNotePanel.ManipulationDelta += (s, e) =>
            {
                // X
                this.X += e.Delta.Translation.X;
                Canvas.SetLeft(this.Source, System.Math.Max(0, this.X));

                // Y
                this.Y += e.Delta.Translation.Y;
                int yHalf = (int)System.Math.Max(0, this.Y) + TrackLayout.ItemSizeHalf;

                int index = yHalf / TrackNoteLayout.ItemSize;
                Canvas.SetTop(this.Source, index * TrackNoteLayout.ItemSize);
            };
            this.TrackNotePanel.ManipulationCompleted += (s, e) =>
            {
            };

            this.TrackNotePanel.DragStarted += (s, e) =>
            {
                this.Offset = e.HorizontalOffset;

                this.TrackNotePanel.ChangePositionUI((int)this.Offset);
                long milliseconds = System.Math.Max(0, this.TrackTempo.ToMilliseconds(this.TrackNotePanel.Position));
                TimeSpan timespan = TimeSpan.FromMilliseconds(milliseconds);

                this.DSTimer.Time = timespan;
            };
            this.TrackNotePanel.DragDelta += (s, e) =>
            {
                this.Offset += e.HorizontalChange;

                this.TrackNotePanel.ChangePositionUI((int)this.Offset);
                long milliseconds = System.Math.Max(0, this.TrackTempo.ToMilliseconds(this.TrackNotePanel.Position));
                TimeSpan timespan = TimeSpan.FromMilliseconds(milliseconds);

                this.DSTimer.Time = timespan;
            };
            this.TrackNotePanel.DragCompleted += (s, e) =>
            {
                this.TrackNotePanel.ChangePositionUI((int)this.Offset);
                long milliseconds = System.Math.Max(0, this.TrackTempo.ToMilliseconds(this.TrackNotePanel.Position));
                TimeSpan timespan = TimeSpan.FromMilliseconds(milliseconds);

                this.DSTimer.Time = timespan;
            };

            this.TrackNotePanel.FootItemClick += (s, e) =>
            {
                if (this.TrackCollection is null) return;

                if (e.ClickedItem is MidiControlController item)
                {
                    ContentControl item2 = this.TrackCollection[TrackIndex];
                    Track track = item2.Content as Track;
                    this.TrackNotePanel.LoadCC(track.Controllers[item]);
                }
            };
            this.TrackListView.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is ContentControl item)
                {
                    Track track = item.Content as Track;
                    this.TrackIndex = this.TrackCollection.IndexOf(item);
                    this.TrackNotePanel.Load(track);
                }
            };
            this.Button.Click += async (s, e) =>
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

                using (IRandomAccessStream stream = await file.OpenAsync(default))
                {
                    this.TrackCollection = new TrackCollection(stream);
                    this.TrackListView.ItemsSource = this.TrackCollection;
                    this.TrackIndex = 0;
                    if (this.TrackCollection.Count is 0)
                    {
                        this.TrackNotePanel.Load(null);
                    }
                    else
                    {
                        this.TrackNotePanel.Load(this.TrackCollection.First().Content as Track);
                    }
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
    }
}