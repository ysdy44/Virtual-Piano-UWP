using Virtual_Piano.Midi.Core;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.TestApp
{
    public sealed partial class CompositionPage : Page
    {
        readonly Windows.UI.Composition.CompositionPropertySet ScrollProperties;
        ~CompositionPage() => this.ScrollProperties.Dispose();
        public CompositionPage()
        {
            this.InitializeComponent();
            this.ScrollProperties = this.ScrollViewer.GetScroller();
            var x = this.ScrollProperties.SnapScrollerX();
            var y = this.ScrollProperties.SnapScrollerY();
            this.Left.GetVisual().StartX(x);
            this.Top.GetVisual().StartY(y);
            this.LeftTop.GetVisual().StartXY(x, y);
            this.LeftBottom.GetVisual().StartX(x);

            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                if ((int)e.NewSize.Height == (int)e.PreviousSize.Height) return;

                var y2 = this.ScrollProperties.SnapScrollerY((int)e.NewSize.Height - 75);
                this.Bottom.GetVisual().StartY(y2);
                this.LeftBottom.GetVisual().StartY(y2);
            };
        }
    }
}