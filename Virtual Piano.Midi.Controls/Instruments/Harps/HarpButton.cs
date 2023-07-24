namespace Virtual_Piano.Midi.Controls
{
    public sealed class HarpButton : ClickButton
    {
        //@Command
        public MidiNote CommandParameter { get; set; }

        protected override void OnClick()
        {
            if (base.Parent is INotePanel item)
            {
                item.OnClick(this.CommandParameter);
            }
        }
    }
}