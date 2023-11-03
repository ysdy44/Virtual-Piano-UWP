using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.TestApp
{
    public sealed partial class DSPage : Page
    {
        public DSPage()
        {
            this.InitializeComponent();
            this.Slider1.ValueChanged += (s, e) => this.DSScorer.Denominator = (int)e.NewValue;
            this.Slider2.ValueChanged += (s, e) => this.DSScorer.Numerator = (int)e.NewValue;
        }
    }
}