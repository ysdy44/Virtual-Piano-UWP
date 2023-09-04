using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Virtual_Piano.Midi;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Foundation;
using Virtual_Piano.Elements;

namespace Virtual_Piano.Views
{
    public sealed partial class DrumView : Page
    {
        //@String
        FlowDirection Direction => CultureInfoCollection.FlowDirection;

        //@Converter
        private Visibility BooleanToVisibilityConverter(bool value) => value ? Visibility.Visible : Visibility.Collapsed;

        ICommand Command;

        public DrumView()
        {
            this.InitializeComponent();
            this.DrumScrollViewer.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                if (e.NewSize.Width == e.PreviousSize.Width) return;

                this.DrumPanel.UpdateWidthCount(e.NewSize.Width);
            };
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