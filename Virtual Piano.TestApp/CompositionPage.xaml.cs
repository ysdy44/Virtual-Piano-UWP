using Virtual_Piano.Elements;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.TestApp
{
    public sealed partial class CompositionPage : Page
    {
        public CompositionPage()
        {
            this.InitializeComponent();
            var m = this.ScrollViewer.GetScroller();
            var x = m.SnapScrollerX();
            var y = m.SnapScrollerY();
            this.Left.GetVisual().AnimationX(x);
            this.Top.GetVisual().AnimationY(y);
            var v = this.LeftTop.GetVisual();
            v.AnimationX(x);
            v.AnimationY(y);
        }
    }
}