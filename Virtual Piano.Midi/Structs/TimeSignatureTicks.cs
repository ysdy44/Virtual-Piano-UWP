using System.Runtime.CompilerServices;

namespace Virtual_Piano.Midi
{
    public readonly struct TimeSignatureTicks
    {
        public readonly int BeatsPerBar; // 4

        public readonly int TicksPerBar; // 480
        public readonly int TicksPerBeat; // 120

        public TimeSignatureTicks(TimeSignature timeSignature, int ticksPerQuarterNote = 480)
        {
            this.BeatsPerBar = timeSignature.Numerator;

            this.TicksPerBar = (timeSignature.Numerator * ticksPerQuarterNote * 4) / (1 << timeSignature.Denominator);
            this.TicksPerBeat = this.TicksPerBar / this.BeatsPerBar;
        }

        public TimeSignatureTicks(TimeSignatureTicks oldTicks, TimeSignature oldTimeSignature, TimeSignature timeSignature)
        {
            this.BeatsPerBar = timeSignature.Numerator;

            this.TicksPerBar = oldTicks.TicksPerBar * timeSignature.Numerator / oldTimeSignature.Numerator;
            this.TicksPerBeat = oldTicks.TicksPerBeat;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToBar(long ticks) => 1 + (ticks / this.TicksPerBar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToBeat(long ticks) => 1 + ((ticks % this.TicksPerBar) / this.TicksPerBeat);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToTick(long ticks) => ticks % this.TicksPerBeat;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToMBT(long ticks) => $"{this.ToBar(ticks)}:{this.ToBeat(ticks)}:{this.ToTick(ticks)}";

        public override string ToString() => $"Ticks {this.TicksPerBeat}ms/Beat";
    }
}