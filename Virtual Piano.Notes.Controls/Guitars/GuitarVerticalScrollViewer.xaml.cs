using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Notes.Controls
{
    public sealed partial class GuitarVerticalScrollViewer : UserControl
    {

        private ScrollViewer scrollViewer;
        public ScrollViewer ScrollViewer
        {
            get => this.scrollViewer;
            set => this.scrollViewer = value;
        }

        double Offset;

        public GuitarVerticalScrollViewer()
        {
            this.InitializeComponent();
            this.PreviousButton.Click += (s, e) => this.PageUp();
            this.NextButton.Click += (s, e) => this.PageDown();
        }

        public void PageUp()
        {
            if (this.ScrollViewer is null) return;
            this.Offset = this.ScrollViewer.HorizontalOffset;
            this.Offset -= 20;
            this.ScrollViewer.ChangeView(this.Offset, null, null, false);
        }
        public void PageDown()
        {
            if (this.ScrollViewer is null) return;
            this.Offset = this.ScrollViewer.HorizontalOffset;
            this.Offset += 20;
            this.ScrollViewer.ChangeView(this.Offset, null, null, false);
        }
    }
}