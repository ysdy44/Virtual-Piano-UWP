namespace Virtual_Piano.Notes.Controls
{
    public sealed class DrumButton : ClickButton, IDrumButton
    {
        //@Command
        public MidiPercussionNote CommandParameter { get; set; }

        protected override void OnClick()
        {
            if (base.Parent is DrumPanel item)
            {
                item.OnClick(this.CommandParameter);
            }
        }
    }
}