using Windows.Devices.Midi;

namespace Virtual_Piano.Notes
{
    public static class MidiExtensions
    {
        public static void ProgramChange(this IMidiOutPort synthesizer, MidiProgram program)
        {
            byte p = (byte)program;
            synthesizer?.SendMessage(new MidiProgramChangeMessage(0, p));
        }

        public static void NoteOff(this IMidiOutPort synthesizer, Note note)
        {
            byte n = (byte)(note + 36);
            synthesizer?.SendMessage(new MidiNoteOffMessage(0, n, 127));
        }
        public static void NoteOn(this IMidiOutPort synthesizer, Note note)
        {
            byte n = (byte)(note + 36);
            synthesizer?.SendMessage(new MidiNoteOnMessage(0, n, 127));
        }

        public static void NoteOff(this IMidiOutPort synthesizer, MidiPercussionNote note)
        {
            byte n = (byte)note;
            synthesizer?.SendMessage(new MidiNoteOffMessage(9, n, 127));
        }
        public static void NoteOn(this IMidiOutPort synthesizer, MidiPercussionNote note)
        {
            byte n = (byte)note;
            synthesizer?.SendMessage(new MidiNoteOnMessage(9, n, 127));
        }
    }
}