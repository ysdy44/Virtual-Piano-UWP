using Virtual_Piano.Midi.Core;

namespace Virtual_Piano.Midi.Controls
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