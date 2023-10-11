using System.Runtime.CompilerServices;

namespace Virtual_Piano.Midi
{
    public readonly struct Ticks
    {
        // PPQN
        public readonly int TicksPerQuarterNote; // 480
        public readonly int BeatsPerBar; // 4

        public readonly int TicksPerBar; // 480
        public readonly int TicksPerBeat; // 120

        public Ticks(TimeSignature timeSignature, int ticksPerQuarterNote = 480)
        {
            this.TicksPerQuarterNote = ticksPerQuarterNote;
            this.BeatsPerBar = timeSignature.Numerator;

            this.TicksPerBar = (timeSignature.Numerator * ticksPerQuarterNote * 4) / (1 << timeSignature.Denominator);
            this.TicksPerBeat = this.TicksPerBar / this.BeatsPerBar;
        }

        public Ticks(Ticks oldTicks, TimeSignature oldTimeSignature, TimeSignature timeSignature)
        {
            this.TicksPerQuarterNote = oldTicks.TicksPerQuarterNote;
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