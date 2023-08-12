using Windows.UI.Xaml;

namespace Virtual_Piano.Midi.Core
{
    public readonly struct TrackLayout
    {
        //@Const
        public const int Scaling = 4;
        public const int Spacing = 21;
        public const int SpacingHalf = Spacing / 2;

        public const int Step = 120;
        public const int StepCount = 4;

        public const int StepSpacing = Step / StepCount;
        public const int StepSpacing2 = StepSpacing / StepCount;

        public readonly int Pane;
        public readonly int Head;
        public readonly int Foot;

        public readonly Thickness Margin;

        public readonly int ExtentHeight;
        public readonly int ExtentHeightHead;
        public readonly int ExtentHeightFoot;

        public TrackLayout(int pane, int head, int foot) // 75, 18, 140
        {
            this.Pane = pane;
            this.Head = head;
            this.Foot = foot;

            this.Margin = new Thickness(pane, head, 0, 0);

            int h = NoteExtensions.NoteCount * TrackLayout.Spacing;
            this.ExtentHeight = h;
            this.ExtentHeightHead = h + head;
            this.ExtentHeightFoot = h + foot;
        }
    }
}