using System.Collections.Generic;
using System.Linq;

namespace Virtual_Piano.Notes
{
    public readonly struct Chords
    {
        public static readonly Octave[] Octaves = System.Enum.GetValues(typeof(Octave)).Cast<Octave>().Reverse().ToArray();
        public static readonly Octave Octave = Octaves.First();

        public readonly Chord Source;

        readonly Tone Root;
        readonly Tone Root2;
        readonly Tone Root3;

        readonly bool IsDefined2;
        readonly bool IsDefined3;

        readonly Tone Next2;
        readonly Tone Next3;

        public Chords(Chord source)
        {
            this.Source = source;
            this.Root = this.Source.ToTone();

            this.Root2 = this.Root + this.Source.ToTone2();
            this.Root3 = this.Root + this.Source.ToTone3();

            this.IsDefined2 = System.Enum.IsDefined(typeof(Tone), this.Root2);
            this.IsDefined3 = System.Enum.IsDefined(typeof(Tone), this.Root3);

            this.Next2 = this.Root2 - NoteExtensions.ToneCount;
            this.Next3 = this.Root3 - NoteExtensions.ToneCount;
        }

        public Note Play(Octave octave)
        {
            return octave.ToNote(this.Root);
        }

        public IEnumerable<Note> Plays(Octave octave)
        {
            if (this.IsDefined2 && this.IsDefined3)
            {
                yield return octave.ToNote(this.Root2);
                yield return octave.ToNote(this.Root3);
            }
            else if (octave == Chords.Octave)
            {
                if (this.IsDefined2)
                    yield return octave.ToNote(this.Root2);

                if (this.IsDefined3)
                    yield return octave.ToNote(this.Root3);
            }
            else
            {
                Octave nextOctave = octave + 1;

                if (this.IsDefined2)
                    yield return octave.ToNote(this.Root2);
                else
                    yield return nextOctave.ToNote(this.Next2);

                if (this.IsDefined3)
                    yield return octave.ToNote(this.Root3);
                else
                    yield return nextOctave.ToNote(this.Next3);
            }
        }
    }
}