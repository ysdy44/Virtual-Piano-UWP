using System.Collections.Generic;
using System.Collections.ObjectModel;
using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed class TrackInfo
    {
        public readonly int Time;
        public readonly int Duration;

        public readonly ObservableCollection<ContentControl> Notes = new ObservableCollection<ContentControl>();
        public readonly ObservableCollection<ContentControl> Programs = new ObservableCollection<ContentControl>();
        public readonly Dictionary<MidiControlController, ControllerCollection> Controllers = new Dictionary<MidiControlController, ControllerCollection>();

        public TrackInfo(MidiTrack track)
        {
            this.Time = track.Time;
            this.Duration = track.Duration;

            foreach (var item in track.Notes)
            {
                var control = new ContentControl
                {
                    Tag = item,
                    Width = item.Duration / TrackNoteLayout.Scaling,
                    Height = TrackNoteLayout.ItemSize,
                    Content = $"{(MidiNote)item.Note} ({item.Velocity})",
                };
                Canvas.SetLeft(control, (double)(item.AbsoluteTime / TrackNoteLayout.Scaling));
                Canvas.SetTop(control, (double)((NoteExtensions.NoteCount - (int)item.Note - 1) * TrackNoteLayout.ItemSize));
                this.Notes.Add(control);
            }

            foreach (var item in track.Programs)
            {
                ContentControl control = new ContentControl
                {
                    Height = TrackNoteLayout.ItemSize,
                    Content = item.Program.ToString(),
                    Tag = item
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