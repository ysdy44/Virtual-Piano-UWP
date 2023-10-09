using System;
using System.Runtime.CompilerServices;

namespace Virtual_Piano.Midi
{
    public readonly struct Tempo
    {
        //@Const
        private const long N = 480 * 120;
        private readonly long D;

        public readonly int Bpm; // 120
        public readonly int TicksPerQuarterNote; // 480
        public readonly TimeSpan MillisecondsPerBeat; // 125

        public Tempo(int bpm = 120, int ticksPerQuarterNote = 480)
        {
            if (bpm == 0)
            {
                this.D = N;
                this.Bpm = 120;
                this.TicksPerQuarterNote = 480;
                this.MillisecondsPerBeat = TimeSpan.FromMilliseconds(125);
            }
            else
            {
                this.D = bpm * ticksPerQuarterNote;
                this.Bpm = bpm;
                this.TicksPerQuarterNote = ticksPerQuarterNote;
                this.MillisecondsPerBeat = TimeSpan.FromMilliseconds(60 * 1000 / bpm / 4);
            }
        }

        public Tempo(Tempo oldTempo, TimeSignature oldTimeSignature, TimeSignature timeSignature)
            : this(oldTempo.Bpm, oldTempo.TicksPerQuarterNote * oldTimeSignature.Numerator / timeSignature.Numerator)
        {
        }

        internal double ToTicksPercent(double ticks) => 100d * D / N / ticks;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToTicks(long milliseconds) => milliseconds * D / N;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToMilliseconds(long ticks) => ticks * N / D;

        public override string ToString() => $"Tempo {this.Bpm}bpm ({this.TicksPerQuarterNote})";
    }
}