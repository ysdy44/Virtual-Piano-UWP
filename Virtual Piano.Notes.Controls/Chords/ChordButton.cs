namespace Virtual_Piano.Notes.Controls
{
    public sealed class ChordButton : ClickButton, IChordButton
    {
        //@Command
        public Octave CommandParameter { get; set; }

        protected override void OnClick()
        {
            if (base.Parent is IChordPanel item)
            {
                item.OnClick(this.CommandParameter);
            }
        }
    }
}