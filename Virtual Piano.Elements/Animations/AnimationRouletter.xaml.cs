using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Elements
{
    public sealed partial class AnimationRouletter : Canvas
    {
        public AnimationRouletter ()
        {
            this.InitializeComponent();
            this.HideStoryboard.Completed += (s, e) =>
            {
                base.Visibility = Visibility.Collapsed;
            };
        }

        public void Hide()
        {
            this.ShowStoryboard.Stop(); // Storyboard
            this.HideStoryboard.Begin(); // Storyboard
        }

        public void Show()
        {
            base.Visibility = Visibility.Visible;
            this.HideStoryboard.Stop(); // Storyboard
            this.ShowStoryboard.Begin(); // Storyboard
        }
    }
}