using Windows.UI.Xaml;

namespace Virtual_Piano.Midi.Core
{
    public readonly struct TrackLayout
    {
        //@Const
        public const int Scaling = 4;
        public const int ItemSize = 66;
        public const int ItemSizeHalf = ItemSize / 2;

        public const int Step = 120;
        public const int StepCount = 4;
        public const int StepSpacing = Step / StepCount;
        public const int StepSpacing2 = StepSpacing / StepCount;

        public readonly int Pane;
        public readonly int Head;
        public int Timerline1 => Head;
        public Thickness Margin => new Thickness(this.Pane, this.Head, 0, 0);

        public int ExtentHeight => 16 * ItemSize;
        public int ExtentHeightHead => ExtentHeight + Head;
        public int ExtentHeightFoot => ExtentHeight + Head;
        public int ExtentHeightHeadFoot => ExtentHeight + Head;

        public TrackLayout(int pane, int head) // 175, 18
        {
            this.Pane = pane;
            this.Head = head;
        }
    }
}