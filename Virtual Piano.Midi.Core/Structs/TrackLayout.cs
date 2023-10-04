using Windows.UI.Xaml;

namespace Virtual_Piano.Midi.Core
{
    public readonly struct TrackLayout
    {
        //@Const
        public const int Scaling = 5;
        public const int ItemSize = 66;
        public const int ItemSizeHalf = ItemSize / 2;

        public readonly int Pane;
        public readonly int Head;
        public readonly int Timerline1;

        public readonly int ExtentHeight;
        public readonly int ExtentHeightHead;
        public readonly int ExtentHeightFoot;
        public readonly int ExtentHeightHeadFoot;

        public TrackLayout(int pane, int head) // 175, 18
        {
            this.Pane = pane;
            this.Head = head;
            this.Timerline1 = head;

            int h = 16 * ItemSize;
            this.ExtentHeight = h;
            this.ExtentHeightHead = h + head;
            this.ExtentHeightFoot = h + head;
            this.ExtentHeightHeadFoot = h + head;
        }
    }
}