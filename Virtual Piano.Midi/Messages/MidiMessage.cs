using Windows.Devices.Midi;

namespace Virtual_Piano.Midi
{
    /// <summary> <see cref="IMidiMessage"/> </summary>
    public struct MidiMessage
    {
        public MidiMessageType Type;
        public byte Channel;
        public int AbsoluteTime;

        public int Duration;
        /// <summary> <see cref="MidiNoteOnMessage.Note"/> </summary>
        public MidiNote Note;
        /// <summary> <see cref="MidiNoteOnMessage.Velocity"/> </summary>
        public byte Velocity;
        /// <summary> <see cref="MidiNoteOnMessage"/> </summary>
        public MidiMessage(NAudio.Midi.NoteOnEvent item, NAudio.Midi.NoteEvent offEvent, long time)
        {
            this.Type = MidiMessageType.NoteOn;
            this.Channel = (byte)item.Channel;
            this.AbsoluteTime = (int)(item.AbsoluteTime - time);

            this.Duration = (int)(offEvent.AbsoluteTime - item.AbsoluteTime);
            this.Note = (MidiNote)(byte)item.NoteNumber;
            this.Velocity = (byte)item.Velocity;

            this.Controller = default;
            this.ControllerValue = default;

            this.Program = default;

            this.Pressure = default;

            this.Bend = default;
        }

        /// <summary> <see cref="MidiControlChangeMessage.Controller"/> </summary>
        public MidiControlController Controller;
        /// <summary> <see cref="MidiControlChangeMessage.ControlValue"/> </summary>
        public byte ControllerValue;
        /// <summary> <see cref="MidiControlChangeMessage"/> </summary>
        public MidiMessage(NAudio.Midi.ControlChangeEvent item, long time)
        {
            this.Type = MidiMessageType.ControlChange;
            this.Channel = (byte)item.Channel;
            this.AbsoluteTime = (int)(item.AbsoluteTime - time);

            this.Duration = default;
            this.Note = default;
            this.Velocity = default;

            this.Controller = (MidiControlController)(byte)item.Controller;
            this.ControllerValue = (byte)item.ControllerValue;

            this.Program = default;

            this.Pressure = default;

            this.Bend = default;
        }

        /// <summary> <see cref="MidiProgramChangeMessage.Program"/> </summary>
        public MidiProgram Program;
        /// <summary> <see cref="MidiProgramChangeMessage"/> </summary>
        public MidiMessage(NAudio.Midi.PatchChangeEvent item, long time)
        {
            this.Type = MidiMessageType.ProgramChange;
            this.Channel = (byte)item.Channel;
            this.AbsoluteTime = (int)(item.AbsoluteTime - time);

            this.Duration = default;
            this.Note = default;
            this.Velocity = default;

            this.Controller = default;
            this.ControllerValue = default;

            this.Program = (MidiProgram)(byte)item.Patch;

            this.Pressure = default;

            this.Bend = default;
        }

        /// <summary> <see cref="MidiChannelPressureMessage.Pressure"/> </summary>
        public byte Pressure;
        /// <summary> <see cref="MidiChannelPressureMessage"/> </summary>
        public MidiMessage(NAudio.Midi.ChannelAfterTouchEvent item, long time)
        {
            this.Type = MidiMessageType.ChannelPressure;
            this.Channel = (byte)item.Channel;
            this.AbsoluteTime = (int)(item.AbsoluteTime - time);

            this.Duration = default;
            this.Note = default;
            this.Velocity = default;

            this.Controller = default;
            this.ControllerValue = default;

            this.Program = default;

            this.Pressure = (byte)item.AfterTouchPressure;

            this.Bend = default;
        }

        /// <summary> <see cref="MidiPitchBendChangeMessage.Bend"/> </summary>
        public ushort Bend;
        /// <summary> <see cref="MidiPitchBendChangeMessage"/> </summary>
        public MidiMessage(NAudio.Midi.PitchWheelChangeEvent item, long time)
        {
            this.Type = MidiMessageType.PitchBendChange;
            this.Channel = (byte)item.Channel;
            this.AbsoluteTime = (int)(item.AbsoluteTime - time);

            this.Duration = default;
            this.Note = default;
            this.Velocity = default;

            this.Controller = default;
            this.ControllerValue = default;

            this.Program = default;

            this.Pressure = default;

            this.Bend = (ushort)item.Pitch;
        }

        /// <summary> <see cref="MidiSystemExclusiveMessage"/> </summary>
        public MidiMessage(NAudio.Midi.MetaEvent item, long time)
        {
            this.Type = MidiMessageType.SystemExclusive;
            this.Channel = (byte)item.Channel;
            this.AbsoluteTime = (int)(item.AbsoluteTime - time);

            this.Duration = default;
            this.Note = default;
            this.Velocity = default;

            this.Controller = default;
            this.ControllerValue = default;

            this.Program = default;

            this.Bend = default;

            this.Pressure = default;
        }

        public string ToXml()
        {
            switch (this.Type)
            {
                case MidiMessageType.NoteOff:
                case MidiMessageType.NoteOn:
                case MidiMessageType.PolyphonicKeyPressure:
                    return $"<{this.Type} Channel=\"{this.Channel}\" AbsoluteTime=\"{this.AbsoluteTime}\" Duration=\"{this.Duration}\" Note=\"{this.Note}\" Velocity=\"{this.Velocity}\"/>";
                case MidiMessageType.ControlChange:
                    return $"<{this.Type} Channel=\"{this.Channel}\" AbsoluteTime=\"{this.AbsoluteTime}\" Controller=\"{this.Controller}\" ControllerValue=\"{this.ControllerValue}\"/>";
                case MidiMessageType.ProgramChange:
                    return $"<{this.Type} Channel=\"{this.Channel}\" AbsoluteTime=\"{this.AbsoluteTime}\" Program=\"{this.Program}\"/>";
                case MidiMessageType.ChannelPressure:
                    return $"<{this.Type} Channel=\"{this.Channel}\" AbsoluteTime=\"{this.AbsoluteTime}\" Pressure=\"{this.Pressure}\"/>";
                case MidiMessageType.PitchBendChange:
                    return $"<{this.Type} Channel=\"{this.Channel}\" AbsoluteTime=\"{this.AbsoluteTime}\" Bend=\"{this.Bend}\"/>";
                case MidiMessageType.SystemExclusive:
                case MidiMessageType.SystemReset:
                    return $"<{this.Type} Channel=\"{this.Channel}\" AbsoluteTime=\"{this.AbsoluteTime}\"/>";
                default:
                    return $"<{this.Type} Channel=\"{this.Channel}\" AbsoluteTime=\"{this.AbsoluteTime}\"/>";
            }
        }

        public override string ToString()
        {
            switch (this.Type)
            {
                case MidiMessageType.NoteOff:
                case MidiMessageType.NoteOn:
                case MidiMessageType.PolyphonicKeyPressure:
                    return $"{this.Note} ({this.Velocity})";
                case MidiMessageType.ControlChange:
                    return $"{this.Controller} ({this.ControllerValue})";
                case MidiMessageType.ProgramChange:
                    return $"{this.Program}";
                case MidiMessageType.ChannelPressure:
                    return $"{this.Pressure}";
                case MidiMessageType.PitchBendChange:
                    return $"{this.Bend}";
                case MidiMessageType.SystemExclusive:
                case MidiMessageType.SystemReset:
                    return string.Empty;
                default:
                    return string.Empty;
            }
        }
    }
}