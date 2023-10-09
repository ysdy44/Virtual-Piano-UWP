using System.Runtime.CompilerServices;

namespace Virtual_Piano.Midi
{
    public readonly struct TimeSignatureTicks
    {
        //public readonly int BeatsPerBar; // 4
     
        public readonly int TicksPerBar; // 480
        public readonly int TicksPerBeat; // 120

        public TimeSignatureTicks(TimeSignature timeSignature, Tempo tempo)
        {
            //this.BeatsPerBar = timeSignature.Numerator;
         
            this.TicksPerBar = (timeSignature.Numerator * tempo.TicksPerQuarterNote * 4) / (timeSignature.Denominator);
            this.TicksPerBeat = this.TicksPerBar / timeSignature.Numerator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToBar(long ticks) => 1 + (ticks / this.TicksPerBar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToBeat(long ticks) => 1 + ((ticks % this.TicksPerBar) / this.TicksPerBeat);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToTick(long ticks) => ticks % this.TicksPerBeat;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToMBT(long ticks) => $"{this.ToBar(ticks)}:{this.ToBeat(ticks)}:{this.ToTick(ticks)}";
    }
}