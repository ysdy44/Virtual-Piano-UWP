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

        /// <summary> <see cref="MidiControlChangeMessage.Controller"/> </summary>
        public MidiControlController Controller;
        /// <summary> <see cref="MidiControlChangeMessage.ControlValue"/> </summary>
        public byte ControllerValue;
        /// <summary> <see cref="MidiControlChangeMessage"/> </summary>

        /// <summary> <see cref="MidiProgramChangeMessage.Program"/> </summary>
        public MidiProgram Program;
        /// <summary> <see cref="MidiProgramChangeMessage"/> </summary>

        /// <summary> <see cref="MidiChannelPressureMessage.Pressure"/> </summary>
        public byte Pressure;
        /// <summary> <see cref="MidiChannelPressureMessage"/> </summary>

        /// <summary> <see cref="MidiPitchBendChangeMessage.Bend"/> </summary>
        public ushort Bend;
        /// <summary> <see cref="MidiPitchBendChangeMessage"/> </summary>

        /// <summary> <see cref="MidiSystemExclusiveMessage"/> </summary>

        public override string ToString()
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
    }
}