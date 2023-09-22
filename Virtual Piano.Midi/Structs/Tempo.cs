using System;

namespace Virtual_Piano.Midi
{
    public readonly struct Tempo
    {
        public readonly int Bpm;
        public readonly int MicrosecondsPerQuarterNote;
        public readonly TimeSpan Delay;

        public Tempo(int bpm = 60)
        {
            this.Bpm = bpm;
            this.MicrosecondsPerQuarterNote = 60 * 1000 * 1000 / bpm;
            this.Delay = TimeSpan.FromMilliseconds(60 * 1000 / bpm / 4);
        }

        public override string ToString() => $"Tempo {this.Bpm}bpm ({this.MicrosecondsPerQuarterNote})";
    }
}