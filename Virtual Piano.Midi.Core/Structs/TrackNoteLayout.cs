using Windows.UI.Xaml;

namespace Virtual_Piano.Midi.Core
{
    public readonly struct TrackNoteLayout
    {
        //@Const
        public const int Scaling = 5;
        public const int ItemSize = 21;
        public const int ItemSizeHalf = ItemSize / 2;

        public readonly int Pane;
        public readonly int Head;
        public readonly int Timerline1;
        public readonly int Timerline2;
        public readonly int Foot;
        public readonly int FootPlus;

        public readonly int ExtentHeight;
        public readonly int ExtentHeightHead;
        public readonly int ExtentHeightFoot;
        public readonly int ExtentHeightHeadFoot;

        public TrackNoteLayout(int pane, int timerline1, int timerline2, int foot) // 75 22 22 150
        {
            var head = timerline1 + timerline2;

            this.Pane = pane;
            this.Head = head;
            this.Timerline1 = timerline1;
            this.Timerline2 = timerline2;
            this.Foot = foot;
            this.FootPlus = foot + 1;

            int h = NoteExtensions.NoteCount * TrackNoteLayout.ItemSize;
            this.ExtentHeight = h;
            this.ExtentHeightHead = h + head;
            this.ExtentHeightFoot = h + foot;
            this.ExtentHeightHeadFoot = h + head + foot;
        }
    }
}