namespace Virtual_Piano.Notes
{
    public static class NoteExtensions
    {
        public const int OctaveCount = 5;
        public const int ToneCount = BlackCount + WhiteCount;
        public const int WhiteCount = 7;
        public const int BlackCount = 5;

        public const int Instrument = 8;

        // 1
        public static Note ToNote(this Octave octave, Tone tone)
        {
            return (Note)(12 * (int)octave + (int)tone);
        }
        public static string ToCDE(this Octave octave, Tone tone)
        {
            return $"{tone.ToCDE()}{(int)octave + 2}";
        }
        public static ToneType ToType(this Tone item)
        {
            switch (item)
            {
                case Tone.DoT:
                case Tone.ReT:
                case Tone.FaT:
                case Tone.SolT:
                case Tone.LaT:
                    return ToneType.Black;
                default:
                    return ToneType.White;
            }
        }
        public static int ToIndex(this Tone item)
        {
            switch (item)
            {
                case Tone.Do:
                case Tone.DoT: return 0;
                case Tone.Re:
                case Tone.ReT: return 1;
                case Tone.Mi: return 2;
                case Tone.Fa:
                case Tone.FaT: return 3;
                case Tone.Sol:
                case Tone.SolT: return 4;
                case Tone.La:
                case Tone.LaT: return 5;
                case Tone.Si: return 6;
                default: return 0;
            }
        }
        public static string ToCDE(this Tone item)
        {
            switch (item)
            {
                case Tone.Do: return "C";
                case Tone.DoT: return "C#";
                case Tone.Re: return "D";
                case Tone.ReT: return "D#";
                case Tone.Mi: return "E";
                case Tone.Fa: return "F";
                case Tone.FaT: return "F#";
                case Tone.Sol: return "G";
                case Tone.SolT: return "G#";
                case Tone.La: return "A";
                case Tone.LaT: return "A#";
                case Tone.Si: return "B";
                default: return null;
            }
        }


        public static Tone ToTone(this Chord item)
        {
            switch (item)
            {
                case Chord.C:
                case Chord.Cm:
                    return Tone.Do;
                case Chord.D:
                case Chord.Dm:
                    return Tone.Re;
                case Chord.E:
                case Chord.Em:
                    return Tone.Mi;
                case Chord.F:
                case Chord.Fm:
                    return Tone.Fa;
                case Chord.G:
                case Chord.Gm:
                    return Tone.Sol;
                case Chord.A:
                case Chord.Am:
                    return Tone.La;
                case Chord.B:
                case Chord.Bm:
                case Chord.Bdim:
                    return Tone.Si;
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


        // 2
        public static string ToCDE(this Note item)
        {
            return item.ToOctave().ToCDE(item.ToTone());
        }
        public static Tone ToTone(this Note item)
        {
            return (Tone)(((int)item) % 12);
        }
        public static Octave ToOctave(this Note item)
        {
            return (Octave)(((int)item) / 12);
        }
    }
}