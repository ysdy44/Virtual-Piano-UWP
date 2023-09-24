using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Virtual_Piano.Midi.Core;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed class TrackCollection : ObservableCollection<ContentControl>
    {
        public readonly long Duration;

        public TrackCollection(IRandomAccessStream accessStream) : this(accessStream.AsStream()) { }
        public TrackCollection(Stream inputStream) : this(new NAudio.Midi.MidiFile(inputStream, false).Events) { }
        public TrackCollection(NAudio.Midi.MidiEventCollection events)
        {
            for (int i = 0; i < events.Tracks; i++)
            {
                IList<NAudio.Midi.MidiEvent> e = events[i];
                if (e.Count == 0) continue;

                Track track = new Track(e, events.StartAbsoluteTime);
                if (this.Duration < track.Source.Duration) this.Duration = track.Source.Duration;

                ContentControl control = new ContentControl
                {
                    Width = track.Source.Duration / TrackLayout.Scaling,
                    Height = TrackLayout.ItemSize,
                    Content = track
                };
                Canvas.SetTop(control, i * TrackLayout.ItemSize);
                Canvas.SetLeft(control, track.Source.Time / TrackLayout.Scaling);
                base.Add(control);
            }
        }
    }
}