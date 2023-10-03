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
        public readonly double Tempo;
        public readonly int Numerator;
        public readonly int Denominator;
        public readonly int SharpsFlats;
        public readonly int MajorMinor;

        public TrackCollection(IRandomAccessStream accessStream) : this(accessStream.AsStream()) { }
        public TrackCollection(Stream inputStream) : this(new NAudio.Midi.MidiFile(inputStream, false).Events) { }
        public TrackCollection(NAudio.Midi.MidiEventCollection events)
        {
            for (int i = 0; i < events.Tracks; i++)
            {
                IList<NAudio.Midi.MidiEvent> e = events[i];
                if (e.Count == 0) continue;

                Track track = new Track(e);
                if (this.Duration < track.Source.Duration) this.Duration = track.Source.Duration;
                if (this.Tempo < track.Source.Tempo) this.Tempo = track.Source.Tempo;
                if (this.Numerator < track.Source.Numerator) this.Numerator = track.Source.Numerator;
                if (this.Denominator < track.Source.Denominator) this.Denominator = track.Source.Denominator;
                if (this.SharpsFlats < track.Source.SharpsFlats) this.SharpsFlats = track.Source.SharpsFlats;
                if (this.MajorMinor < track.Source.MajorMinor) this.MajorMinor = track.Source.MajorMinor;

                ContentControl control = new ContentControl
                {
                    Width = track.Source.Duration / TrackLayout.Scaling,
                    Height = TrackLayout.ItemSize,
                    Content = track
                };
                Canvas.SetTop(control, i * TrackLayout.ItemSize);
                base.Add(control);
            }
        }
    }
}