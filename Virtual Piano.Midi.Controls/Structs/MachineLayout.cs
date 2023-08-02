using Windows.UI.Xaml;

namespace Virtual_Piano.Midi.Controls
{
    public readonly struct MachineLayout
    {
        //@Const
        public const int Spacing = 80;

        public readonly int Pane;
        public readonly int Head;

        public readonly Thickness Margin;

        public readonly int ExtentWidth;
        public readonly int ExtentHeight;

        public MachineLayout(int pane, int head, int width, int height) // 160 40 32 15
        {
            this.Pane = pane;
            this.Head = head;

            this.Margin = new Thickness(pane, head, 0, 0);

            this.ExtentWidth = width * MachineLayout.Spacing;
            this.ExtentHeight = height * MachineLayout.Spacing;
        }
    }
}