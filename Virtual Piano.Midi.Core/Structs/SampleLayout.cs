using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Core
{
    public readonly struct SampleLayout
    {
        public readonly int ItemSize;
        public readonly int ItemSizeHalf;

        public readonly int Length;

        public readonly int Pane;
        public readonly int Bar;
        public readonly int PaneBar;

        public SampleLayout(int itemSize, int pane, int bar) // 32 120 40
        {
            this.ItemSize = itemSize;
            this.ItemSizeHalf = itemSize / 2;

            int h = NoteExtensions.NoteCount * itemSize;
            this.Length = h;

            this.Pane = pane;
            this.Bar = bar;
            this.PaneBar = pane + bar;
        }

        public ContentControl ToItem(int index, object content, object tag)
        {
            ContentControl irem = new ContentControl
            {
                TabIndex = index,
                Content = content,
                Height = this.ItemSize,
                Tag = tag
            };

            Canvas.SetLeft(irem, this.ItemSize);
            Canvas.SetTop(irem, index * this.ItemSize);
            return irem;
        }
    }
}