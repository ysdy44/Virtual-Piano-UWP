using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Controls
{
    public sealed class VelocityToggleButton : Button
    {
        public bool IsChecked
        {
            get => this.isChecked;
            set
            {
                this.isChecked = value;
                this.SymbolIcon.Symbol = value ? Symbol.Volume : Symbol.Mute;
            }
        }
        private bool isChecked;

        readonly SymbolIcon SymbolIcon = new SymbolIcon();

        //@Construct
        public VelocityToggleButton()
        {
            base.Content = this.SymbolIcon;
            base.Click += (s, e) => base.Command?.Execute(this.IsChecked is false); // Command
        }
    }
}