using System.Runtime.CompilerServices;

namespace Virtual_Piano.Midi
{
    public readonly struct TimeSignatureTicks
    {
        public readonly int TicksPerQuarterNote; // 480

        public readonly int TicksPerBar; // 480
        public readonly int TicksPerBeat; // 120

        public TimeSignatureTicks(TimeSignature timeSignature, int ticksPerQuarterNote = 480)
        {
            this.TicksPerQuarterNote = ticksPerQuarterNote;

            this.TicksPerBar = (timeSignature.Numerator * ticksPerQuarterNote * 4) / (1 << timeSignature.Denominator);
            this.TicksPerBeat = this.TicksPerBar / timeSignature.Numerator;
        }

        public TimeSignatureTicks(TimeSignatureTicks oldTicks, TimeSignature oldTimeSignature, TimeSignature timeSignature)
        {
            this.TicksPerBeat = oldTicks.TicksPerBeat;
            this.TicksPerBar = this.TicksPerBeat * timeSignature.Numerator;

            this.TicksPerQuarterNote = (1 << timeSignature.Denominator) * this.TicksPerBeat / 4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToBar(long time) => 1 + (time / this.TicksPerBar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToBeat(long time) => 1 + ((time % this.TicksPerBar) / this.TicksPerBeat);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToTick(long time) => time % this.TicksPerBeat;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToMBT(long time) => $"{this.ToBar(time)}:{this.ToBeat(time)}:{this.ToTick(time)}";
    }
}