using System;
using System.Runtime.CompilerServices;

namespace Virtual_Piano.Midi
{
    public readonly struct Tempo
    {
        private readonly int N;
        private readonly int D;

        public readonly int Bpm; // 120
        public readonly int TicksPerBeat; // 480
        public readonly TimeSpan MillisecondsPerBeat; // 125

        public Tempo(TimeSignatureTicks ticks, double bpm = 120)
        {
            if (bpm == 0)
            {
                //this.N = 1000 * 60;
                //this.D = 120 * 480;

                this.N = 25;
                this.D = 24;

                this.Bpm = 120;
                this.TicksPerBeat = 480;
                this.MillisecondsPerBeat = TimeSpan.FromMilliseconds(125);
            }
            else
            {
                this.N = 1000 * 60;
                this.D = (int)(bpm * ticks.TicksPerBeat);

                this.Bpm = (int)bpm;
                this.TicksPerBeat = ticks.TicksPerBeat;
                this.MillisecondsPerBeat = TimeSpan.FromMilliseconds(1000 * 60 / bpm);
            }
        }

        public Tempo(int bpm, int ticksPerBeat = 480)
        {
            this.N = 1000 * 60;
            this.D = bpm * ticksPerBeat;

            this.Bpm = bpm;
            this.TicksPerBeat = 480;
            this.MillisecondsPerBeat = TimeSpan.FromMilliseconds(60 * 1000 / bpm / 4);
        }

        internal double ToTicksPercent(double ticks) => 100d * D / N / ticks;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToTicks(long milliseconds) => milliseconds * D / N;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToMilliseconds(long ticks) => ticks * N / D;

        public override string ToString() => $"Tempo {this.Bpm}bpm ({this.TicksPerBeat})";
    }
}