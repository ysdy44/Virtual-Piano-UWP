﻿using Windows.Devices.Midi;

namespace Virtual_Piano.Notes
{
    public struct Message
    {
        public MidiMessageType Type;
        public byte Channel;
        public int AbsoluteTime;

        public int Duration;
        public Note Note;
        public byte Velocity;

        public ControlController Controller { get; set; }
        public byte ControllerValue { get; set; }

        public MidiProgram Program;

        public byte Pressure { get; }

        public ushort Bend;

        public override string ToString()
        {
            switch (this.Type)
            {
                case MidiMessageType.NoteOff:
                case MidiMessageType.NoteOn:
                    return $"<{this.Type} Channel=\"{this.Channel}\" AbsoluteTime=\"{this.AbsoluteTime}\" Duration=\"{this.Duration}\" Note=\"{this.Note}\" Velocity=\"{this.Velocity}\"/>";
                case MidiMessageType.PolyphonicKeyPressure:
                    return $"<{this.Type} Channel=\"{this.Channel}\" AbsoluteTime=\"{this.AbsoluteTime}\" Pressure=\"{this.Pressure}\"/>";
                case MidiMessageType.ControlChange:
                    return $"<{this.Type} Channel=\"{this.Channel}\" AbsoluteTime=\"{this.AbsoluteTime}\" Controller=\"{this.Controller}\" ControllerValue=\"{this.ControllerValue}\"/>";
                case MidiMessageType.ProgramChange:
                    return $"<{this.Type} Channel=\"{this.Channel}\" AbsoluteTime=\"{this.AbsoluteTime}\" Program=\"{this.Program}\"/>";
                case MidiMessageType.ChannelPressure:
                    return $"<{this.Type} Channel=\"{this.Channel}\" AbsoluteTime=\"{this.AbsoluteTime}\" Pressure=\"{this.Pressure}\"/>";
                case MidiMessageType.PitchBendChange:
                    return $"<{this.Type} Channel=\"{this.Channel}\" AbsoluteTime=\"{this.AbsoluteTime}\" Bend=\"{this.Bend}\"/>";
                case MidiMessageType.SystemReset:
                    return $"<{this.Type} Channel=\"{this.Channel}\" AbsoluteTime=\"{this.AbsoluteTime}\"/>";
                default:
                    return $"<{this.Type} Channel=\"{this.Channel}\" AbsoluteTime=\"{this.AbsoluteTime}\"/>";
            }
        }
    }
}