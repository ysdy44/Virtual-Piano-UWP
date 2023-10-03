namespace Virtual_Piano.Elements
{
    internal readonly struct RadarGraph
    {
        public readonly RadarLine L0;
        public readonly RadarLine L1;
        public readonly RadarLine L2;
        public readonly RadarLine L3;
        public readonly RadarLine L4;
        public readonly RadarLine L5;
        
        public RadarGraph(int angle)
        {
            this.L0 = new RadarLine(angle);
            this.L1 = new RadarLine(1 * 60 + angle);
            this.L2 = new RadarLine(2 * 60 + angle);
            this.L3 = new RadarLine(this.L0);
            this.L4 = new RadarLine(this.L1);
            this.L5 = new RadarLine(this.L2);
        }
    }
}