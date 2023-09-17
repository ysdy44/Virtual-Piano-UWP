using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Virtual_Piano.Midi.Controllers
{
    [ContentProperty(Name = nameof(Text))]
    public sealed partial class TrackChannel : UserControl
    {
        public string Label
        {
            get => this.LabelTextBlock.Text;
            set => this.LabelTextBlock.Text = value;
        }
        public string Text
        {
            get => this.TextBlock.Text;
            set => this.TextBlock.Text = value;
        }
        public object CommandParameter
        {
            get => this.Button.CommandParameter;
            set => this.Button.CommandParameter = value;
        }
        public ICommand Command 
        {
            get => this.Button.Command;
            set => this.Button.Command = value;
        }

        public TrackChannel()
        {
            this.InitializeComponent();
        }
    }
}