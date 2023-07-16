using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Elements
{
    public class CommandCheckedButton : Button
    {
        public Symbol CheckedSymbol { get; set; }
        public Symbol UncheckedSymbol { get; set; }

        public Symbol Symbol
        {
            get => this.SymbolIcon.Symbol;
            private set => this.SymbolIcon.Symbol = value;
        }

        public bool IsChecked
        {
            get => this.isChecked;
            set
            {
                this.isChecked = value;
                this.Symbol = value ? this.CheckedSymbol : this.UncheckedSymbol;
            }
        }
        private bool isChecked;

        readonly SymbolIcon SymbolIcon = new SymbolIcon();

        //@Construct
        public CommandCheckedButton()
        {
            base.Content = this.SymbolIcon;
            base.Loaded += (s, e) => this.Symbol = this.IsChecked ? this.CheckedSymbol : this.UncheckedSymbol;
        }
    }
}