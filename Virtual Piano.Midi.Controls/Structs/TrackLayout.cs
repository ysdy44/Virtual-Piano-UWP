using Windows.UI.Xaml;

namespace Virtual_Piano.Midi.Controls
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

        public readonly int Left;
        public readonly int Top;
        public readonly int Bottom;

        public readonly Thickness MarginLeft;
        public readonly Thickness MarginLeftTop;

        public readonly int ExtentHeight;
        public readonly int ExtentHeightTop;
        public readonly int ExtentHeightBottom;

        public TrackLayout(int left, int top, int bottom) // 75, 18, 140
        {
            this.Left = left;
            this.Top = top;
            this.Bottom = bottom;

            this.MarginLeft = new Thickness(left, 0, 0, 0);
            this.MarginLeftTop = new Thickness(left, top, 0, 0);

            int h = NoteExtensions.NoteCount * TrackLayout.Spacing;
            this.ExtentHeight = h;
            this.ExtentHeightTop = h + top;
            this.ExtentHeightBottom = h + bottom;
        }
    }
}