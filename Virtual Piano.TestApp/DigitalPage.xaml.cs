using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.TestApp
{
    public sealed partial class DigitalPage : Page
    {
        public DigitalPage()
        {
            this.InitializeComponent();
            this.Slider1.ValueChanged += (s, e) => this.DigitalScorer.Denominator = (int)e.NewValue;
            this.Slider2.ValueChanged += (s, e) => this.DigitalScorer.Numerator = (int)e.NewValue;
        }
    }
}