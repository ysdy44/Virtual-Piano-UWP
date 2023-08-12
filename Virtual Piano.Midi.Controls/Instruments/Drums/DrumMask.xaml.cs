using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Instruments
{
    public sealed partial class DrumMask : UserControl
    {
        public DrumMask()
        {
            this.InitializeComponent();
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                int w = (int)e.NewSize.Width;
                int h = (int)e.NewSize.Height;
                int min = System.Math.Min(w, h);

                this.Shape1.Width = w;
                this.Shape1.Height = h;

                int s2 = min * 5 / 12;
                this.Shape2.Width =
                this.Shape2.Height = s2 + s2;
                Canvas.SetLeft(this.Shape2, w / 2 - s2);
                Canvas.SetTop(this.Shape2, h / 2 - s2);

                int s3 = min * 3 / 12;
                this.Shape3.Width =
                this.Shape3.Height = s3 + s3;
                Canvas.SetLeft(this.Shape3, w / 2 - s3);
                Canvas.SetTop(this.Shape3, h / 2 - s3);
            };
        }
    }
}