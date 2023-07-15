using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Elements
{
    public sealed class CommandToggleButton : Button
    {
        //@Command
        public object UncheckedCommandParameter { get; set; }
        public object CheckedCommandParameter { get; set; }

        public Symbol CheckedSymbol { get; set; }
        public Symbol UncheckedSymbol { get; set; }

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

        public Symbol Symbol
        {
            get => this.SymbolIcon.Symbol;
            private set => this.SymbolIcon.Symbol = value;
        }

        readonly SymbolIcon SymbolIcon = new SymbolIcon();

        //@Construct
        public CommandToggleButton()
        {
            base.Content = this.SymbolIcon;
            base.Loaded += (s, e) => this.Symbol = this.IsChecked ? this.CheckedSymbol : this.UncheckedSymbol;
            base.Click += (s, e) =>
            {
                if (this.isChecked)
                {
                    this.isChecked = false;
                    this.Symbol = this.UncheckedSymbol;
                    base.Command?.Execute(this.UncheckedCommandParameter); // Command
                }
                else
                {
                    this.isChecked = true;
                    this.SymbolIcon.Symbol = this.CheckedSymbol;
                    base.Command?.Execute(this.CheckedCommandParameter); // Command
                }
            };
        }
    }
}