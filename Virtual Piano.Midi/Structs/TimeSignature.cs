namespace Virtual_Piano.Midi
{
    public readonly struct TimeSignature
    {
        public readonly int Numerator;
        public readonly int Denominator;
        public readonly Beat Beat;

        public TimeSignature(int numerator = 4, Beat beat = Beat.QuarterNote)
        {
            this.Numerator = numerator;
            this.Beat = beat;
            switch (beat)
            {
                case Beat.WholeNote: this.Denominator = 1; break;
                case Beat.HalfNote: this.Denominator = 2; break;
                case Beat.QuarterNote: this.Denominator = 4; break;
                case Beat.EighthNote: this.Denominator = 8; break;
                case Beat.SixteenthNote: this.Denominator = 16; break;
                case Beat.ThirtySecond: this.Denominator = 32; break;
                default: this.Denominator = 4; break;
            }
        }

        public TimeSignature(int numerator = 4, int fenominator = 4)
        {
            this.Numerator = numerator;
            this.Denominator = fenominator;
            switch (fenominator)
            {
                case 1: this.Beat = Beat.WholeNote; break;
                case 2: this.Beat = Beat.HalfNote; break;
                case 4: this.Beat = Beat.QuarterNote; break;
                case 8: this.Beat = Beat.EighthNote; break;
                case 16: this.Beat = Beat.SixteenthNote; break;
                case 32: this.Beat = Beat.ThirtySecond; break;
                default: this.Beat = Beat.QuarterNote; break;
            }
        }

        public override string ToString() => $"TimeSignature {this.Numerator}/{this.Denominator}";
    }
}