namespace Virtual_Piano.Midi.Controls
{
    public sealed class ChordButton : ClickButton, IClickButton
    {
        //@Command
        public MidiOctave CommandParameter { get; set; }

        protected override void OnClick()
        {
            if (base.Parent is IChordPanel item)
            {
                item.OnClick(this.CommandParameter);
            }
        }
    }
}