namespace Virtual_Piano.Notes.Controls
{
    public sealed class PianoButton : ClickButton, IPianoButton
    {
        //@Command
        public Note CommandParameter { get; set; }
        public ToneType Type { get; set; }

        protected override void OnClick()
        {
            if (base.Parent is IPianoPanel item)
            {
                item.OnClick(this.CommandParameter);
            }
        }
    }
}