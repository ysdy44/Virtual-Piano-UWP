using System.Windows.Input;
using Virtual_Piano.Elements;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano.Views
{
    public sealed partial class ChordView : Page
    {
        //@String
        FlowDirection Direction => CultureInfoCollection.FlowDirection;

        //@Converter
        private Visibility BooleanToVisibilityConverter(bool value) => value ? Visibility.Visible : Visibility.Collapsed;
        private Thickness BooleanToThickness163Converter(bool value) => value ? new Thickness(0, 163, 0, -163) : default;
        private Thickness BooleanToThickness203Converter(bool value) => value ? new Thickness(0, 203, 0, -203) : default;

        ICommand Command;

        public ChordView()
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