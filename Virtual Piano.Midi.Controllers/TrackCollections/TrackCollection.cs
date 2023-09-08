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
        public readonly int Duration;

        public TrackCollection(IRandomAccessStream accessStream) : this(accessStream.AsStream()) { }
        public TrackCollection(Stream inputStream) : this(new NAudio.Midi.MidiFile(inputStream, false).Events) { }
        public TrackCollection(NAudio.Midi.MidiEventCollection events)
        {
            for (int i = 0; i < events.Tracks; i++)
            {
                IList<NAudio.Midi.MidiEvent> e = events[i];
                if (e.Count == 0) continue;

                MidiTrack track = new MidiTrack(e, events.StartAbsoluteTime);

                TrackInfo info = new TrackInfo(track);
                if (this.Duration < info.Duration) this.Duration = info.Duration;

                var control = new ContentControl
                {
                    Tag = info,
                    Width = info.Duration / TrackLayout.Scaling,
                    Height = TrackLayout.ItemSize,
                    Content = new TrackItem(track)
                    {
                        Text = MidiProgram.AcousticGrand.ToString()
                    }
                };
                Canvas.SetTop(control, i * TrackLayout.ItemSize);
                Canvas.SetLeft(control, info.Time / TrackLayout.Scaling);
                base.Add(control);
            }
        }
    }
}