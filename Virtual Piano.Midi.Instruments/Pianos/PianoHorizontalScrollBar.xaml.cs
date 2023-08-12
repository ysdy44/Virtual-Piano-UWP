using Virtual_Piano.Midi.Core;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Instruments
{
    public sealed partial class PianoHorizontalScrollBar : UserControl
    {

        private PianoSize PianoSize = new PianoSize(20);

        public int ItemSize
        {
            get => this.PianoSize.ItemSize;
            set
            {
                if (this.PianoSize.ItemSize == value) return;
                this.PianoSize = new PianoSize(value);

                this.Rectangle.Width = base.ActualWidth * this.RootGrid.ActualWidth / this.PianoSize.Length;

                if (this.ScrollViewer is null) return;
                Canvas.SetLeft(this.Rectangle, this.ScrollViewer.HorizontalOffset * this.RootGrid.ActualWidth / this.PianoSize.Length);
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

        public PianoHorizontalScrollBar()
        {
            this.InitializeComponent();
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                if (e.NewSize.Width == e.PreviousSize.Width) return;

                this.Rectangle.Width = e.NewSize.Width * this.RootGrid.ActualWidth / this.PianoSize.Length;
            };
            this.RootGrid.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                if (e.NewSize.Width == e.PreviousSize.Width) return;

                this.Rectangle.Width = base.ActualWidth * e.NewSize.Width / this.PianoSize.Length;

                if (this.ScrollViewer is null) return;
                Canvas.SetLeft(this.Rectangle, this.ScrollViewer.HorizontalOffset * e.NewSize.Width / this.PianoSize.Length);
            };

            this.PreviousButton.Click += (s, e) => this.PageUp();
            this.NextButton.Click += (s, e) => this.PageDown();

            this.Thumb.DragStarted += (s, e) =>
            {
                this.Offset = e.HorizontalOffset - this.Rectangle.ActualWidth / 2;
                this.Offset = this.Offset * this.PianoSize.Length / this.RootGrid.ActualWidth;
                this.ScrollViewer.ChangeView(this.Offset, null, null, true);
            };
            this.Thumb.DragDelta += (s, e) =>
            {
                this.Offset += e.HorizontalChange * this.PianoSize.Length / this.RootGrid.ActualWidth;
                this.ScrollViewer.ChangeView(this.Offset, null, null, true);
            };
            this.Thumb.DragCompleted += (s, e) =>
            {
                this.ScrollViewer.ChangeView(this.Offset, null, null, true);
            };
        }

        private void ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            Canvas.SetLeft(this.Rectangle, e.NextView.HorizontalOffset * this.RootGrid.ActualWidth / this.PianoSize.Length);
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