using Virtual_Piano.Elements;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.TestApp
{
    public sealed partial class XamlPathIconPage : Page
    {
        public XamlPathIconPage()
        {
            this.InitializeComponent();
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources["Piano"].ToGeometry() });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources["Chord"].ToGeometry() });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources["Machine"].ToGeometry() });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources["Drum"].ToGeometry() });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources["Guitar"].ToGeometry() });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources["Bass"].ToGeometry() });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources["Harp"].ToGeometry() });
        }
    }
}