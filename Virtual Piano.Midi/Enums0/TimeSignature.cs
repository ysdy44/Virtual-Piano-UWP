namespace Virtual_Piano.Midi
{
    public readonly struct TimeSignature
    {
        public readonly int Numerator;
        public readonly Beat Beat;
        public int Denominator
        {
            get
            {
                switch (this.Beat)
                {
                    case Beat.WholeNote: return 1;
                    case Beat.HalfNote: return 2;
                    case Beat.QuarterNote: return 4;
                    case Beat.EighthNote: return 8;
                    case Beat.SixteenthNote: return 16;
                    case Beat.ThirtySecond: return 32;
                    default: return 0;
                }
            }
        }

        public TimeSignature(int numerator = 3, Beat beat = Beat.QuarterNote)
        {
            this.Numerator = numerator;
            this.Beat = beat;
        }

        public override string ToString() => $"TimeSignature {this.Numerator}/{this.Denominator}";
    }
}