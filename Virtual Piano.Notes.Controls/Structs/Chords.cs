﻿using System.Collections.Generic;

namespace Virtual_Piano.Notes
{
    public readonly struct Chords
    {
        public readonly Chord Source;

        readonly Tone Root;
        readonly Tone Root2;
        readonly Tone Root3;

        public Chords(Chord source)
        {
            this.Source = source;
            this.Root = this.Source.ToTone();

            this.Root2 = this.Root + this.Source.ToTone2();
            this.Root3 = this.Root + this.Source.ToTone3();
        }

        public Note Play(Octave octave)
        {
            return octave.ToNote(this.Root);
        }

        public IEnumerable<Note> Plays(Octave octave)
        {
            yield return octave.ToNote(this.Root2);
            yield return octave.ToNote(this.Root3);
        }
    }
}