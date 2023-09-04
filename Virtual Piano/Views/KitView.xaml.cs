using System.Windows.Input;
using Virtual_Piano.Elements;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano.Views
{
    public sealed partial class KitView : Page
    {
        //@String
        FlowDirection Direction => CultureInfoCollection.FlowDirection;

        ICommand Command;

        public KitView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.Command = null;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Command = e.Parameter as ICommand;
        }
    }
}