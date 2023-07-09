using Virtual_Piano.Elements;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.TestApp
{
    public sealed partial class CompositionPage : Page
    {
        public CompositionPage()
        {
            this.InitializeComponent();
            var m = this.ScrollViewer.GetManipulation();
            var x = m.ExpressionX();
            var y = m.ExpressionY();
            this.Left.GetVisual().OffsetX(x);
            this.Top.GetVisual().OffsetY(y);
            var v = this.LeftTop.GetVisual();
            v.OffsetX(x);
            v.OffsetY(y);
        }
    }
}