namespace Virtual_Piano.Notes.Controls
{
    public sealed class NoteButton : ClickButton, INoteButton
    {
        //@Command
        public Note CommandParameter { get; set; }
        public ToneType Type { get; set; }

        protected override void OnClick()
        {
            if (base.Parent is INotePanel item)
            {
                item.OnClick(this.CommandParameter);
            }
        }
    }
}