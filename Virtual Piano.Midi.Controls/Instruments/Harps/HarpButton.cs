using Virtual_Piano.Midi.Core;

namespace Virtual_Piano.Midi.Instruments
{
    public sealed class HarpButton : ClickButton
    {
        //@Command
        public MidiNote CommandParameter { get; set; }

        protected override void OnClick()
        {
            if (base.Parent is IHarpPanel item)
            {
                item.OnClick(this.CommandParameter);
            }
        }
    }
}