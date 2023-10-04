namespace Virtual_Piano.Midi.Core
{
    public readonly struct XYPadLines
    {
        public readonly int L1;
        public readonly int L2;
        public readonly int L3;
        public readonly int L4;
        public readonly int L5;

        public XYPadLines(int min, int max)
        {
            this.L3 = (min + max) / 2;

            this.L1 = (min + min + this.L3) / 3;
            this.L2 = (min + this.L3 + this.L3) / 3;

            this.L4 = (max + this.L3 + this.L3) / 3;
            this.L5 = (max + max + this.L3) / 3;
        }
    }
}