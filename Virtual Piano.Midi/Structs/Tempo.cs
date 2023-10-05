using System;
using System.Runtime.CompilerServices;

namespace Virtual_Piano.Midi
{
    public readonly struct Tempo
    {
        //@Const
        private const long D = 480 * 120;
        private readonly long M;

        public readonly int Bpm; // 120
        public readonly int TicksPerQuarterNote; // 480
        public readonly TimeSpan Delay; // 125

        public Tempo(int bpm = 120, int ticksPerQuarterNote = 480)
        {
            if (bpm == 0)
            {
                this.M = D;
                this.Bpm = 120;
                this.TicksPerQuarterNote = 480;
                this.Delay = TimeSpan.FromMilliseconds(125);
            }
            else
            {
                this.M = bpm * ticksPerQuarterNote;
                this.Bpm = bpm;
                this.TicksPerQuarterNote = ticksPerQuarterNote;
                this.Delay = TimeSpan.FromMilliseconds(60 * 1000 / bpm / 4);
            }
        }

        public Tempo(Tempo oldTempo, TimeSignature oldTimeSignature, TimeSignature timeSignature)
            : this(oldTempo.Bpm, oldTempo.TicksPerQuarterNote * oldTimeSignature.Numerator / timeSignature.Numerator)
        {
        }

        internal double ReverseScalePercent(double duration) => 100d * M / D / duration;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ReverseScale(long position) => position * M / D;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long Scale(long duration) => duration * D / M;

        public override string ToString() => $"Tempo {this.Bpm}bpm ({this.TicksPerQuarterNote})";
    }
}