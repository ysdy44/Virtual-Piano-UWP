﻿using System.Runtime.CompilerServices;

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
        public long ToBar(long time) => 1 + (time / this.TicksPerBar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToBeat(long time) => 1 + ((time % this.TicksPerBar) / this.TicksPerBeat);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToTick(long time) => time % this.TicksPerBeat;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToMBT(long time) => $"{this.ToBar(time)}:{this.ToBeat(time)}:{this.ToTick(time)}";
    }
}