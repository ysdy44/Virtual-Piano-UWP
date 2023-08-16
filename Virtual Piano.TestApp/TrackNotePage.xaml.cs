using System;
using System.Collections.ObjectModel;
using System.Linq;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Controllers;
using Windows.Devices.Midi;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano.TestApp
{
    public sealed partial class TrackNotePage : Page
    {
        TrackCollection Collections;
        int Index = -1;
        double Offset;

        MidiSynthesizer Synthesizer;
        ~TrackNotePage() => this.Synthesizer?.Dispose();
        public TrackNotePage()
        {
            this.InitializeComponent();
            this.TrackNotePanel.DragStarted += (s, e) =>
            {
                this.Offset = e.HorizontalOffset;
                var t = this.TrackNotePanel.UpdateTimeline((int)this.Offset);
                this.DSTimer.Time = (TimeSpan.FromMilliseconds(System.Math.Max(0, t)));
            };
            this.TrackNotePanel.DragDelta += (s, e) =>
            {
                this.Offset += e.HorizontalChange;
                var t = this.TrackNotePanel.UpdateTimeline((int)this.Offset);
                this.DSTimer.Time = (TimeSpan.FromMilliseconds(System.Math.Max(0, t)));
            };
            this.TrackNotePanel.DragCompleted += (s, e) =>
            {
                var t = this.TrackNotePanel.UpdateTimeline((int)this.Offset);
                this.DSTimer.Time = (TimeSpan.FromMilliseconds(System.Math.Max(0, t)));
            };

            this.TrackNotePanel.FootItemClick += (s, e) =>
            {
                if (Collections is null) return;

                if (e.ClickedItem is MidiControlController item)
                {
                    var item2 = this.Collections[Index];
                    var info = item2.Tag as TrackInfo;
                    this.TrackNotePanel.LoadCC(info.Controllers[item]);
                }
            };
            this.TrackListView.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is ContentControl item)
                {
                    var info = item.Tag as TrackInfo;
                    this.Index = this.Collections.IndexOf(item);
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

                using (var stream = await file.OpenAsync(default))
                {
                    this.Collections = new TrackCollection(stream);
                    this.TrackListView.ItemsSource = this.Collections;
                    this.Index = 0;
                    if (this.Collections.Count is 0)
                    {
                        this.TrackNotePanel.LoadInfo(null);
                    }
                    else
                    {
                        this.TrackNotePanel.LoadInfo(this.Collections.First().Tag as TrackInfo);
                    }
                }
            };
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e) => this.Synthesizer?.Dispose();
        protected async override void OnNavigatedTo(NavigationEventArgs e) => this.Synthesizer = await MidiSynthesizer.CreateAsync();
    }
}