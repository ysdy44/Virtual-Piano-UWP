namespace Virtual_Piano.Midi.Controls
{
    public sealed class InstrumentButton : ClickButton, IInstrumentButton
    {
        //@Command
        public MidiProgram CommandParameter { get; set; }

        protected override void OnClick()
        {
            if (base.Parent is IInstrumentPanel item)
            {
                item.OnClick(this.CommandParameter);
            }
        }
    }
}