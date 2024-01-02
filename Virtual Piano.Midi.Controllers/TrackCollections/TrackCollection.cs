using NAudio.Midi;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Virtual_Piano.Midi.Core;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed class TrackCollection : ObservableCollection<ContentControl>
    {
        public readonly long StartAbsoluteTime;
        public readonly int DeltaTicksPerQuarterNote; // PPQN

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
            this.StartAbsoluteTime = events.StartAbsoluteTime;
            this.DeltaTicksPerQuarterNote = events.DeltaTicksPerQuarterNote;

            int tracks = events.Tracks;

            switch (tracks)
            {
                case 0:
                    break;
                case 1:
                    IList<NAudio.Midi.MidiEvent> events1 = events.Single();
                    IDictionary<int, IList<MidiEvent>> dictionary = new Dictionary<int, IList<MidiEvent>>();

                    foreach (NAudio.Midi.MidiEvent item in events1)
                    {
                        int channel = item.Channel;
                        if (tracks < channel) tracks = channel;

                        if (dictionary.ContainsKey(channel))
                            dictionary[channel].Add(item);
                        else
                            dictionary.Add(channel, new List<MidiEvent>
                            {
                                item
                            });
                    }

                    for (int i = 0; i <= tracks; i++)
                    {
                        if (dictionary.ContainsKey(i) is false) continue;

                        IList<NAudio.Midi.MidiEvent> events2 = dictionary[i];
                        if (events2 == null) continue;
                        if (events2.Count == 0) continue;

                        Track track = new Track(events2);
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
                    break;
                default:
                    for (int i = 0; i < tracks; i++)
                    {
                        IList<NAudio.Midi.MidiEvent> events3 = events[i];
                        if (events3 == null) continue;
                        if (events3.Count == 0) continue;

                        Track track = new Track(events3);
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
                    break;
            }
        }
    }
}