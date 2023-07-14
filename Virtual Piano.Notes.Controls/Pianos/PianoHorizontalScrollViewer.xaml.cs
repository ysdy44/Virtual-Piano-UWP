using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Notes.Controls
{
    public sealed partial class PianoHorizontalScrollViewer : UserControl, IPianoScrollViewer
    {

        private PianoSize PianoSize = new PianoSize(20);
        private double Length = NoteExtensions.NoteWhiteCount * 20 * NoteExtensions.ToneCount / NoteExtensions.WhiteCount;
        public int ItemSize
        {
            get => this.PianoSize.ItemSize;
            set
            {
                if (this.PianoSize.ItemSize == value) return;
                this.PianoSize = new PianoSize(value);
                this.Length = NoteExtensions.NoteWhiteCount * this.PianoSize.WhiteSize;

                this.Rectangle.Width = base.ActualWidth * this.RootGrid.ActualWidth / this.Length;

                if (this.ScrollViewer is null) return;
                Canvas.SetLeft(this.Rectangle, this.ScrollViewer.HorizontalOffset * this.RootGrid.ActualWidth / this.Length);
            }
        }

        private ScrollViewer scrollViewer;
        public ScrollViewer ScrollViewer
        {
            get => this.scrollViewer;
            set
            {
                if (this.scrollViewer != null) this.scrollViewer.ViewChanging -= this.ViewChanging;
                this.scrollViewer = value;
                if (this.scrollViewer != null) this.scrollViewer.ViewChanging += this.ViewChanging;
            }
        }

        double Offset;

        public PianoHorizontalScrollViewer()
        {
            this.InitializeComponent();
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                if (e.NewSize.Width == e.PreviousSize.Width) return;

                this.Rectangle.Width = e.NewSize.Width * this.RootGrid.ActualWidth / this.Length;
            };
            this.RootGrid.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                if (e.NewSize.Width == e.PreviousSize.Width) return;

                this.Rectangle.Width = base.ActualWidth * e.NewSize.Width / this.Length;

                if (this.ScrollViewer is null) return;
                Canvas.SetLeft(this.Rectangle, this.ScrollViewer.HorizontalOffset * e.NewSize.Width / this.Length);
            };

            this.PreviousButton.Click += (s, e) => this.PageUp();
            this.NextButton.Click += (s, e) => this.PageDown();

            this.Thumb.DragStarted += (s, e) =>
            {
                this.Offset = e.HorizontalOffset - this.Rectangle.ActualWidth / 2;
                this.Offset = this.Offset * this.Length / this.RootGrid.ActualWidth;
                this.ScrollViewer.ChangeView(this.Offset, null, null, true);
            };
            this.Thumb.DragDelta += (s, e) =>
            {
                this.Offset += e.HorizontalChange * this.Length / this.RootGrid.ActualWidth;
                this.ScrollViewer.ChangeView(this.Offset, null, null, true);
            };
            this.Thumb.DragCompleted += (s, e) =>
            {
                this.ScrollViewer.ChangeView(this.Offset, null, null, true);
            };
        }

        private void ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            Canvas.SetLeft(this.Rectangle, e.NextView.HorizontalOffset * this.RootGrid.ActualWidth / this.Length);
        }

        public void PageUp()
        {
            if (this.ScrollViewer is null) return;
            this.Offset = this.ScrollViewer.HorizontalOffset;
            this.Offset -= this.PianoSize.WhiteSize;
            this.ScrollViewer.ChangeView(this.Offset, null, null, false);
        }
        public void PageDown()
        {
            if (this.ScrollViewer is null) return;
            this.Offset = this.ScrollViewer.HorizontalOffset;
            this.Offset += this.PianoSize.WhiteSize;
            this.ScrollViewer.ChangeView(this.Offset, null, null, false);
        }
    }
}