using System.Collections.Generic;
using System.Collections.ObjectModel;
using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed class Track : Canvas
    {
        //@Const
        const int Count2 = NoteExtensions.NoteCount / 2;
        const int Count4 = Count2 / 2;

        public readonly MidiTrack Source;
        public readonly ObservableCollection<ContentControl> Notes = new ObservableCollection<ContentControl>();
        public readonly ObservableCollection<ContentControl> Programs = new ObservableCollection<ContentControl>();
        public readonly Dictionary<MidiControlController, ControllerCollection> Controllers = new Dictionary<MidiControlController, ControllerCollection>();

        public Track(IList<NAudio.Midi.MidiEvent> events)
        {
            this.Source = new MidiTrack(events);

            foreach (MidiMessage item in this.Source.Notes)
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

                long x = item.AbsoluteTime / TrackNoteLayout.Scaling;
                int y = item.Note.ToInedx();
                long w = System.Math.Max(4, item.Duration / TrackNoteLayout.Scaling);
                base.Children.Add(new Line
                {
                    X1 = x,
                    Y1 = y - Count4,
                    X2 = x + w,
                    Y2 = y - Count4
                });
            }

            foreach (MidiMessage item in this.Source.Programs)
            {
                ContentControl control = new ContentControl
                {
                    Height = TrackNoteLayout.ItemSize,
                    Content = item
                };
                Canvas.SetLeft(control, item.AbsoluteTime / TrackNoteLayout.Scaling);
                this.Programs.Add(control);
            }

            foreach (var item in this.Source.Controllers)
            {
                this.Controllers.Add(item.Key, new ControllerCollection(item.Value, this.Source.Duration));
            }
        }
    }
}