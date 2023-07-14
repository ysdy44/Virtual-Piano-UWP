namespace Virtual_Piano.Notes.Controls
{
    public sealed class GuitarButton : ClickButton, IGuitarButton
    {
        //@Command
        public Note CommandParameter { get; set; }
        public GuitarString Strings { get; set; }

        protected override void OnClick()
        {
            if (base.Parent is IGuitarPanel item)
            {
                item.OnClick(this.CommandParameter, this.Strings);
            }
        }
    }
}