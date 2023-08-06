using Virtual_Piano.Elements;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.TestApp
{
    public sealed partial class XamlPathIconPage : Page
    {
        public XamlPathIconPage()
        {
            this.InitializeComponent();
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources.FindGeometry("Piano") });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources.FindGeometry("Chord") });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources.FindGeometry("Machine") });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources.FindGeometry("Drum") });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources.FindGeometry("Guitar") });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources.FindGeometry("Bass") });
            this.StackPanel.Children.Add(new PathIcon { Data = base.Resources.FindGeometry("Harp") });
        }
    }
}