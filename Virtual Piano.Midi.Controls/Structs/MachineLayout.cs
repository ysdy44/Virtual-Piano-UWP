namespace Virtual_Piano.Midi.Controls
{
    public readonly struct MachineLayout
    {
        //@Const
        public const int Spacing = 56;

        public readonly int Pane;
        public readonly int Head;

        public readonly int ExtentWidth;
        public readonly int ExtentHeight;

        public MachineLayout(int pane, int head, int width, int height) // 160 40 32 15
        {
            this.Pane = pane;
            this.Head = head;

            this.ExtentWidth = pane + width * MachineLayout.Spacing;
            this.ExtentHeight = head + height * MachineLayout.Spacing;
        }
    }
}