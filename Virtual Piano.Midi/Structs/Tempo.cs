using System;
using System.Runtime.CompilerServices;

namespace Virtual_Piano.Midi
{
    public readonly struct Tempo
    {
        private readonly int N;
        private readonly int D;

        public readonly int Bpm; // 120
        public readonly TimeSpan MillisecondsPerBeat; // 125

        public Tempo(Ticks ticks, double bpm = 120)
        {
            if (bpm == 0)
            {
                //this.N = 1000 * 60;
                //this.D = 120 * 480;

                this.N = 25;
                this.D = 24;

                this.Bpm = 120;
                this.MillisecondsPerBeat = TimeSpan.FromMilliseconds(125);
            }
            else
            {
                this.N = 1000 * 60;
                this.D = (int)(bpm * ticks.TicksPerBeat);

                this.Bpm = (int)bpm;
                this.MillisecondsPerBeat = TimeSpan.FromMilliseconds(1000 * 60 / bpm);
            }
        }

        internal double ToTicksPercent(double ticks) => 100d * D / N / ticks;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToTicks(long milliseconds) => milliseconds * D / N;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToMilliseconds(long ticks) => ticks * N / D;

        public override string ToString() => $"Tempo {this.Bpm}bpm";
    }
}