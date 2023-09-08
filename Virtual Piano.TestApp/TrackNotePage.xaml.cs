using System;
using System.Linq;
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
    public sealed partial class TrackNotePage : Page
    {
        TrackCollection TrackCollection;
        MidiSynthesizer MidiSynthesizer;

        int Index = -1;
        double Offset;

        ~TrackNotePage() => this.MidiSynthesizer?.Dispose();
        public TrackNotePage()
        {
            this.InitializeComponent();
            this.TrackNotePanel.DragStarted += (s, e) =>
            {
                this.Offset = e.HorizontalOffset;
                int t = this.TrackNotePanel.UpdateTimeline((int)this.Offset);
                this.DSTimer.Time = (TimeSpan.FromMilliseconds(System.Math.Max(0, t)));
            };
            this.TrackNotePanel.DragDelta += (s, e) =>
            {
                this.Offset += e.HorizontalChange;
                int t = this.TrackNotePanel.UpdateTimeline((int)this.Offset);
                this.DSTimer.Time = (TimeSpan.FromMilliseconds(System.Math.Max(0, t)));
            };
            this.TrackNotePanel.DragCompleted += (s, e) =>
            {
                int t = this.TrackNotePanel.UpdateTimeline((int)this.Offset);
                this.DSTimer.Time = (TimeSpan.FromMilliseconds(System.Math.Max(0, t)));
            };

            this.TrackNotePanel.FootItemClick += (s, e) =>
            {
                if (this.TrackCollection is null) return;

                if (e.ClickedItem is MidiControlController item)
                {
                    ContentControl item2 = this.TrackCollection[Index];
                    TrackInfo info = item2.Tag as TrackInfo;
                    this.TrackNotePanel.LoadCC(info.Controllers[item]);
                }
            };
            this.TrackListView.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is ContentControl item)
                {
                    TrackInfo info = item.Tag as TrackInfo;
                    this.Index = this.TrackCollection.IndexOf(item);
                    this.TrackNotePanel.LoadInfo(info);
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
                    this.Index = 0;
                    if (this.TrackCollection.Count is 0)
                    {
                        this.TrackNotePanel.LoadInfo(null);
                    }
                    else
                    {
                        this.TrackNotePanel.LoadInfo(this.TrackCollection.First().Tag as TrackInfo);
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