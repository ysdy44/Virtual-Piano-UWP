using System.Collections.Generic;
using System.Collections.ObjectModel;
using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed class TrackInfo : Canvas
    {
        const int Count2 = NoteExtensions.NoteCount / 2;
        const int Count4 = Count2 / 2;

        public readonly int Time;
        public readonly int Duration;

        public readonly ObservableCollection<ContentControl> Notes = new ObservableCollection<ContentControl>();
        public readonly ObservableCollection<ContentControl> Programs = new ObservableCollection<ContentControl>();
        public readonly Dictionary<MidiControlController, ControllerCollection> Controllers = new Dictionary<MidiControlController, ControllerCollection>();

        public TrackInfo(MidiTrack track)
        {
            this.Time = track.Time;
            this.Duration = track.Duration;

            foreach (MidiMessage item in track.Notes)
            {
                ContentControl control = new ContentControl
                {
                    Width = item.Duration / TrackNoteLayout.Scaling,
                    Height = TrackNoteLayout.ItemSize,
                    Content = item
                };
                Canvas.SetLeft(control, (double)(item.AbsoluteTime / TrackNoteLayout.Scaling));
                Canvas.SetTop(control, (double)((NoteExtensions.NoteCount - (int)item.Note - 1) * TrackNoteLayout.ItemSize));
                this.Notes.Add(control);

                int x = item.AbsoluteTime / TrackNoteLayout.Scaling;
                int y = item.Note.ToInedx();
                var w = System.Math.Max(4, item.Duration / TrackNoteLayout.Scaling);
                base.Children.Add(new Line
                {
                    X1 = x,
                    Y1 = y - Count4,
                    X2 = x + w,
                    Y2 = y - Count4
                });
            }

            foreach (MidiMessage item in track.Programs)
            {
                ContentControl control = new ContentControl
                {
                    Height = TrackNoteLayout.ItemSize,
                    Content = item
                };
                Canvas.SetLeft(control, item.AbsoluteTime / TrackNoteLayout.Scaling);
                this.Programs.Add(control);
            }

            foreach (var item in track.Controllers)
            {
                this.Controllers.Add(item.Key, new ControllerCollection(item.Value, this.Duration));
            }
        }
    }
}