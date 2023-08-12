using Virtual_Piano.Midi.Core;

namespace Virtual_Piano.Midi.Controls
{
    public sealed class GuitarButton : ClickButton, IGuitarButton
    {
        //@Command
        public MidiNote CommandParameter { get; set; }
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