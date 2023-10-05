using System;
using System.Runtime.CompilerServices;

namespace Virtual_Piano.Midi
{
    public readonly struct Tempo
    {
        public readonly int Bpm; // 120
        public readonly int MicrosecondsPerQuarterNote; // 500000
        public readonly TimeSpan Delay; // 125

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
        public long ReverseScale(long position) => 1 * this.Bpm / 120 * position;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long Scale(long duration) => duration * 120 / this.Bpm;

        public override string ToString() => $"Tempo {this.Bpm}bpm ({this.MicrosecondsPerQuarterNote})";
    }
}