namespace Virtual_Piano.Elements
{
    public  class CommandToggleButton : CommandCheckedButton
    {
        //@Command
        public object UncheckedCommandParameter { get; set; }
        public object CheckedCommandParameter { get; set; }

        //@Construct
        public CommandToggleButton()
        {
            base.Click += (s, e) =>
            {
                if (this.IsChecked)
                {
                    this.IsChecked = false;
                    base.Command?.Execute(this.UncheckedCommandParameter); // Command
                }
                else
                {
                    this.IsChecked = true;
                    base.Command?.Execute(this.CheckedCommandParameter); // Command
                }
            };
        }
    }
}