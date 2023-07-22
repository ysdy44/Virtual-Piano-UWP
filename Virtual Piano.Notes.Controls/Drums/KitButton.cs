namespace Virtual_Piano.Notes.Controls
{
    public sealed class KitButton : ClickButton, IKitButton
    {
        //@Command
        public KitSet CommandParameter { get; set; }

        protected override void OnClick()
        {
            if (base.Parent is IKitPanel item)
            {
                item.OnClick(this.CommandParameter);
            }
        }
    }
}