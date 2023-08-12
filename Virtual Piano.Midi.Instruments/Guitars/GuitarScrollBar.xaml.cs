using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Instruments
{
    public sealed partial class GuitarScrollBar : StackPanel
    {

        private ScrollViewer scrollViewer;
        public ScrollViewer ScrollViewer
        {
            get => this.scrollViewer;
            set => this.scrollViewer = value;
        }

        double Offset;

        public GuitarScrollBar()
        {
            this.InitializeComponent();
            this.PreviousButton.Click += (s, e) => this.PageUp();
            this.NextButton.Click += (s, e) => this.PageDown();
        }

        public void PageUp()
        {
            if (this.ScrollViewer is null) return;
            this.Offset = this.ScrollViewer.HorizontalOffset;
            this.Offset -= 40;
            this.ScrollViewer.ChangeView(this.Offset, null, null, false);
        }
        public void PageDown()
        {
            if (this.ScrollViewer is null) return;
            this.Offset = this.ScrollViewer.HorizontalOffset;
            this.Offset += 40;
            this.ScrollViewer.ChangeView(this.Offset, null, null, false);
        }
    }
}