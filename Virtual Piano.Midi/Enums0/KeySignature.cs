namespace Virtual_Piano.Midi
{
    public readonly struct KeySignature
    {
        public readonly int SharpsFlats;
        public readonly int MajorMinor;

        public KeySignature(int sharpsFlats, int majorMinor)
        {
            this.SharpsFlats = sharpsFlats;
            this.MajorMinor = majorMinor;
        }

        public override string ToString() => $"KeySignature {this.SharpsFlats} {this.MajorMinor}";
    }
}