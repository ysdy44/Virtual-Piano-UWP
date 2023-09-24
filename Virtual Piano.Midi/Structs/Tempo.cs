using System;

namespace Virtual_Piano.Midi
{
    public readonly struct Tempo
    {
        public readonly int Bpm;
        public readonly int MicrosecondsPerQuarterNote;
        public readonly TimeSpan Delay;

        public Tempo(int bpm = 120)
        {
            if (bpm == 0)
            {
                this.Bpm = 120;
                this.MicrosecondsPerQuarterNote = 500000;
                this.Delay = TimeSpan.FromMilliseconds(125);
            }
            else
            {
                this.Bpm = bpm;
                this.MicrosecondsPerQuarterNote = 60 * 1000 * 1000 / bpm;
                this.Delay = TimeSpan.FromMilliseconds(60 * 1000 / bpm / 4);
            }
        }

        public int GetPosition(long position) => (int)(1d * this.Bpm / 120d * position);

        public int GetDuration(int duration) => (int)(120d * duration / this.Bpm);

        public override string ToString() => $"Tempo {this.Bpm}bpm ({this.MicrosecondsPerQuarterNote})";
    }
}