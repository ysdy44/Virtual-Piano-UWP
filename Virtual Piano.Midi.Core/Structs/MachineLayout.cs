namespace Virtual_Piano.Midi.Core
{
    public readonly struct MachineLayout
    {
        public readonly int Spacing;

        public readonly int Pane;
        public readonly int Head;
        public readonly int Timeline1;
        public readonly int Timeline2;

        public readonly int ExtentWidth;
        public readonly int ExtentHeight;

        public MachineLayout(int spacing, int pane, int timeline1, int timeline2, int widthCount, int heightCount) // 50 60 24 16 32 4
        {
            var head = timeline1 + timeline2;
            this.Spacing = spacing;

            this.Pane = pane;
            this.Head = head;
            this.Timeline1 = timeline1;
            this.Timeline2 = timeline2;

            this.ExtentWidth = pane + widthCount * spacing;
            this.ExtentHeight = head + heightCount * spacing;
        }
    }
}