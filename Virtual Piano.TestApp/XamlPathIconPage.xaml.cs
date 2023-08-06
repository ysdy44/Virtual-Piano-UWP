using Virtual_Piano.Elements;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.TestApp
{
    public sealed partial class XamlPathIconPage : Page
    {
        public XamlPathIconPage()
        {
            this.InitializeComponent();
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources["Piano"].FindGeometry() });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources["Chord"].FindGeometry() });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources["Machine"].FindGeometry() });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources["Drum"].FindGeometry() });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources["Guitar"].FindGeometry() });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources["Bass"].FindGeometry() });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources["Harp"].FindGeometry() });
        }
    }
}