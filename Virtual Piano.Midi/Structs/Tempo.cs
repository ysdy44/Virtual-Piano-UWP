using System;
using System.Runtime.CompilerServices;

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ReverseScale(long position) => (int)(1d * this.Bpm / 120d * position);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Scale(int duration) => (int)(duration * 120d / this.Bpm);

        public override string ToString() => $"Tempo {this.Bpm}bpm ({this.MicrosecondsPerQuarterNote})";
    }
}