using System.Collections.Generic;

namespace Virtual_Piano.Midi
{
    public sealed class MidiTrack
    {
        public readonly long Time;
        public readonly long Duration;
        public readonly double Tempo;
        public readonly int Numerator;
        public readonly int Denominator;
        public readonly int SharpsFlats;
        public readonly int MajorMinor;

        public readonly List<MidiMessage> Notes = new List<MidiMessage>();
        public readonly List<MidiMessage> Programs = new List<MidiMessage>();
        public readonly Dictionary<MidiControlController, List<MidiMessage>> Controllers = new Dictionary<MidiControlController, List<MidiMessage>>();

        public MidiTrack(IList<NAudio.Midi.MidiEvent> events, long time)
        {
            this.Time = time;

            foreach (NAudio.Midi.MidiEvent item in events)
            {
                switch (item.CommandCode)
                {
                    case NAudio.Midi.MidiCommandCode.NoteOff:
                        break;
                    case NAudio.Midi.MidiCommandCode.NoteOn:
                        if (item is NAudio.Midi.NoteOnEvent noteOnEvent)
                            if (noteOnEvent.OffEvent is NAudio.Midi.NoteEvent offEvent)
                                this.Notes.Add(new MidiMessage(noteOnEvent, offEvent, time));
                        break;
                    case NAudio.Midi.MidiCommandCode.KeyAfterTouch:
                        break;

                    case NAudio.Midi.MidiCommandCode.ControlChange:
                        if (item is NAudio.Midi.ControlChangeEvent controlChangeEvent)
                        {
                            MidiControlController controller = (MidiControlController)controlChangeEvent.Controller;
                            if (this.Controllers.ContainsKey(controller))
                                this.Controllers[controller].Add(new MidiMessage(controlChangeEvent, time));
                            else
                                this.Controllers.Add(controller, new List<MidiMessage>
                                {
                                    new MidiMessage(controlChangeEvent, time)
                                });
                        }
                        break;
                    case NAudio.Midi.MidiCommandCode.PatchChange:
                        if (item is NAudio.Midi.PatchChangeEvent patchChangeEvent)
                            this.Programs.Add(new MidiMessage(patchChangeEvent, time));
                        break;
                    case NAudio.Midi.MidiCommandCode.ChannelAfterTouch:
                        if (item is NAudio.Midi.ChannelAfterTouchEvent channelAfterTouchEvent)
                        {
                        }
                        break;
                    case NAudio.Midi.MidiCommandCode.PitchWheelChange:
                        if (item is NAudio.Midi.PitchWheelChangeEvent pitchWheelChangeEvent)
                        {
                        }
                        break;

                    case NAudio.Midi.MidiCommandCode.TimingClock:
                    case NAudio.Midi.MidiCommandCode.StartSequence:
                    case NAudio.Midi.MidiCommandCode.ContinueSequence:
                    case NAudio.Midi.MidiCommandCode.StopSequence:
                    case NAudio.Midi.MidiCommandCode.AutoSensing:
                        if (item is NAudio.Midi.MidiEvent MidiEvent)
                        {
                        }
                        break;

                    case NAudio.Midi.MidiCommandCode.Sysex:
                    case NAudio.Midi.MidiCommandCode.MetaEvent:
                        if (item is NAudio.Midi.MetaEvent metaEvent)
                        {
                            switch (metaEvent.MetaEventType)
                            {
                                case NAudio.Midi.MetaEventType.TrackSequenceNumber:
                                    if (metaEvent is NAudio.Midi.TrackSequenceNumberEvent trackSequenceNumber)
                                    {
                                    }
                                    break;
                                case NAudio.Midi.MetaEventType.TextEvent:
                                case NAudio.Midi.MetaEventType.Copyright:
                                case NAudio.Midi.MetaEventType.SequenceTrackName:
                                case NAudio.Midi.MetaEventType.TrackInstrumentName:
                                case NAudio.Midi.MetaEventType.Lyric:
                                case NAudio.Midi.MetaEventType.Marker:
                                case NAudio.Midi.MetaEventType.CuePoint:
                                case NAudio.Midi.MetaEventType.ProgramName:
                                case NAudio.Midi.MetaEventType.DeviceName:
                                    if (metaEvent is NAudio.Midi.TextEvent textEvent)
                                    {
                                    }
                                    break;
                                case NAudio.Midi.MetaEventType.EndTrack:
                                    if (metaEvent is NAudio.Midi.MetaEvent endEvent)
                                    {
                                        this.Duration = endEvent.AbsoluteTime;
                                    }
                                    break;
                                case NAudio.Midi.MetaEventType.SetTempo:
                                    if (metaEvent is NAudio.Midi.TempoEvent tempoEvent)
                                    {
                                        this.Tempo = (int)tempoEvent.Tempo;
                                    }
                                    break;
                                case NAudio.Midi.MetaEventType.TimeSignature:
                                    if (metaEvent is NAudio.Midi.TimeSignatureEvent timeSignatureEvent)
                                    {
                                        this.Numerator = timeSignatureEvent.Numerator;
                                        this.Denominator = timeSignatureEvent.Denominator;
                                    }
                                    break;
                                case NAudio.Midi.MetaEventType.KeySignature:
                                    if (metaEvent is NAudio.Midi.KeySignatureEvent keySignatureEvent)
                                    {
                                        this.SharpsFlats = keySignatureEvent.SharpsFlats;
                                        this.MajorMinor = keySignatureEvent.MajorMinor;
                                    }
                                    break;
                                case NAudio.Midi.MetaEventType.SequencerSpecific:
                                    if (metaEvent is NAudio.Midi.SequencerSpecificEvent sequencerSpecificEvent)
                                    {
                                    }
                                    break;
                                case NAudio.Midi.MetaEventType.SmpteOffset:
                                    if (metaEvent is NAudio.Midi.SmpteOffsetEvent smpteOffsetEvent)
                                    {
                                    }
                                    break;
                                default:
                                    if (metaEvent is NAudio.Midi.RawMetaEvent rawMetaEvent)
                                    {
                                    }
                                    break;
                            }
                        }
                        break;

                    case NAudio.Midi.MidiCommandCode.Eox:
                        if (item is NAudio.Midi.MidiEvent eoxEvent)
                        {
                        }
                        break;

                    default:
                        break;
                }
            }
        }

    }
}