using System.Threading.Tasks;
using Windows.Devices.Midi;

namespace Virtual_Piano.Midi
{
    public static class MidiExtensions
    {

        public static void NoteOff(this IMidiOutPort synthesizer, MidiNote note, byte channel = 0)
        {
            synthesizer?.SendMessage(new MidiNoteOffMessage(channel, (byte)note, 0));
        }
        public static void NoteOn(this IMidiOutPort synthesizer, MidiNote note, byte channel = 0, byte velocity = 127)
        {
            synthesizer?.SendMessage(new MidiNoteOnMessage(channel, (byte)note, velocity));
        }

        public static void NoteOff(this IMidiOutPort synthesizer, MidiPercussionNote note)
        {
            synthesizer?.SendMessage(new MidiNoteOffMessage(9, (byte)note, 0));
        }
        public static void NoteOn(this IMidiOutPort synthesizer, MidiPercussionNote note, byte velocity = 127)
        {
            synthesizer?.SendMessage(new MidiNoteOnMessage(9, (byte)note, velocity));
        }

        public static void PolyphonicKeyPressure(this IMidiOutPort synthesizer, MidiNote note, byte pressure, byte channel = 0)
        {
            synthesizer?.SendMessage(new MidiPolyphonicKeyPressureMessage(channel, (byte)note, pressure));
        }

        public static void ControlChange(this IMidiOutPort synthesizer, ControlController controller, byte controlValue, byte channel = 0)
        {
            synthesizer?.SendMessage(new MidiControlChangeMessage(channel, (byte)controller, controlValue));
        }

        public static void ProgramChange(this IMidiOutPort synthesizer, MidiProgram program, byte channel = 0)
        {
            synthesizer?.SendMessage(new MidiProgramChangeMessage(channel, (byte)program));
        }

        public static void ChannelPressure(this IMidiOutPort synthesizer, byte pressure, byte channel = 0)
        {
            synthesizer?.SendMessage(new MidiChannelPressureMessage(channel, pressure));
        }

        public static void PitchBendChange(this IMidiOutPort synthesizer, ushort bend, byte channel = 0)
        {
            synthesizer?.SendMessage(new MidiPitchBendChangeMessage(channel, bend));
        }

        public static void SystemReset(this IMidiOutPort synthesizer)
        {
            synthesizer?.SendMessage(new MidiSystemResetMessage());
        }

        public static void SendMessage(this IMidiOutPort synthesizer, Message item)
        {
            switch (item.Type)
            {
                case MidiMessageType.NoteOff:
                case MidiMessageType.NoteOn:
                    synthesizer.SendNote(item);
                    break;
                case MidiMessageType.PolyphonicKeyPressure:
                    synthesizer.PolyphonicKeyPressure(item.Note, item.Pressure, item.Channel);
                    break;
                case MidiMessageType.ControlChange:
                    synthesizer.ControlChange(item.Controller, item.ControllerValue, item.Channel);
                    break;
                case MidiMessageType.ProgramChange:
                    synthesizer.ProgramChange(item.Program, item.Channel);
                    break;
                case MidiMessageType.ChannelPressure:
                    synthesizer.ChannelPressure(item.Pressure, item.Channel);
                    break;
                case MidiMessageType.PitchBendChange:
                    synthesizer.PitchBendChange(item.Bend, item.Channel);
                    break;
                case MidiMessageType.SystemReset:
                    synthesizer.SystemReset();
                    break;
                default:
                    break;
            }
        }

        private async static void SendNote(this IMidiOutPort synthesizer, Message item)
        {
            if (synthesizer is null) return;
            synthesizer.NoteOn(item.Note, item.Channel, item.Velocity);
            await Task.Delay(item.Duration);
            synthesizer.NoteOff(item.Note, item.Channel);
        }
    }
}