using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Elements
{
    public sealed partial class AnimationRing : UserControl
    {
        public AnimationRing()
        {
            this.InitializeComponent();
        }
        public void Show()
        {
            if (base.IsEnabled)
            {
                this.Storyboard.Stop(); // Storyboard
                this.Storyboard.Begin(); // Storyboard
            }
        }
    }
}