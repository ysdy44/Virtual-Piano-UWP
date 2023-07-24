using Ex = Virtual_Piano.Midi.NoteExtensions;

namespace Virtual_Piano.Midi
{
    public static partial class NoteExtensions
    {
        public const int OctaveCount = 11;

        public const int ToneCount = BlackCount + WhiteCount;
        public const int WhiteCount = 7;
        public const int BlackCount = 5;

        public const int NoteCount = 128;
        public const int NoteWhiteCount = 75;
        public const int NoteBlackCount = NoteCount - NoteWhiteCount;

        public static Tone ToTone(this MidiNote note) => (Tone)((int)note % 12);
        public static Octave ToOctave(this MidiNote note) => (Octave)((int)note / 12);
        public static ToneType ToType(this Tone tone)
        {
            switch (tone)
            {
                case Tone.Csharp:
                case Tone.Dsharp:
                case Tone.Fsharp:
                case Tone.Gsharp:
                case Tone.Asharp:
                    return ToneType.Black;
                default:
                    return ToneType.White;
            }
        }
        public static ToneType ToType(this MidiNote note)
        {
            switch ((int)note % 12)
            {
                case 1:
                case 3:
                case 6:
                case 8:
                case 10:
                    return ToneType.Black;
                default:
                    return ToneType.White;
            }
        }
        public static string ToLabel(this MidiNote note, KeyLabel label)
        {
            switch (label)
            {
                case KeyLabel.Off: return null;
                case KeyLabel.Conly: return note.ToTone() == Tone.C ? note.ToCDE() : null;
                case KeyLabel.CDE: return note.ToCDE();
                case KeyLabel.DoReMi: return note.ToTone().ToDoReMi();
                default: return null;
            }
        }

        public static int ToIndex(this Tone item)
        {
            switch (item)
            {
                case Tone.C:
                case Tone.Csharp: return 0;
                case Tone.D:
                case Tone.Dsharp: return 1;
                case Tone.E: return 2;
                case Tone.F:
                case Tone.Fsharp: return 3;
                case Tone.G:
                case Tone.Gsharp: return 4;
                case Tone.A:
                case Tone.Asharp: return 5;
                case Tone.B: return 6;
                default: return 0;
            }
        }
        public static string ToCDE(this Tone item)
        {
            switch (item)
            {
                case Tone.C: return "C";
                case Tone.Csharp: return "C#";
                case Tone.D: return "D";
                case Tone.Dsharp: return "D#";
                case Tone.E: return "E";
                case Tone.F: return "F";
                case Tone.Fsharp: return "F#";
                case Tone.G: return "G";
                case Tone.Gsharp: return "G#";
                case Tone.A: return "A";
                case Tone.Asharp: return "A#";
                case Tone.B: return "B";
                default: return null;
            }
        }
        public static string ToDoReMi(this Tone item)
        {
            switch (item)
            {
                case Tone.C: return "Do";
                case Tone.Csharp: return "Do#";
                case Tone.D: return "Re";
                case Tone.Dsharp: return "Re#";
                case Tone.E: return "Mi";
                case Tone.F: return "Fa";
                case Tone.Fsharp: return "Fa#";
                case Tone.G: return "Sol";
                case Tone.Gsharp: return "Sol#";
                case Tone.A: return "La";
                case Tone.Asharp: return "La#";
                case Tone.B: return "Si";
                default: return null;
            }
        }
        public static string ToCDE(this Octave octave, Tone tone)
        {
            return $"{tone.ToCDE()}{(int)octave + 1}";
        }
        public static string ToCDE(this MidiNote note)
        {
            int i = (int)note;
            Tone tone = (Tone)(i % 12);
            return $"{tone.ToCDE()}{i / 12 + 1}";
        }
        public static MidiNote ToNote(this Octave octave, Tone tone)
        {
            return (MidiNote)(12 * (int)octave + (int)tone);
        }


        public static Tone ToTone(this Chord item)
        {
            switch (item)
            {
                case Chord.C:
                case Chord.Cm:
                    return Tone.C;
                case Chord.D:
                case Chord.Dm:
                    return Tone.D;
                case Chord.E:
                case Chord.Em:
                    return Tone.E;
                case Chord.F:
                case Chord.Fm:
                    return Tone.F;
                case Chord.G:
                case Chord.Gm:
                    return Tone.G;
                case Chord.A:
                case Chord.Am:
                    return Tone.A;
                case Chord.B:
                case Chord.Bm:
                case Chord.Bdim:
                    return Tone.B;
                default:
                    return default;
            }
        }

        public static int ToTone2(this Chord item)
        {
            switch (item)
            {
                case Chord.C:
                case Chord.D:
                case Chord.E:
                case Chord.F:
                case Chord.G:
                case Chord.A:
                case Chord.B:
                    return 4;
                default:
                    return 3;
            }
        }

        public static int ToTone3(this Chord item)
        {
            switch (item)
            {
                case Chord.Bdim:
                    return 6;
                default:
                    return 7;
            }
        }
    }
}