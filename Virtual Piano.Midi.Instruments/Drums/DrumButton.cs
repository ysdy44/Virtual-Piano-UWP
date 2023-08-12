using Virtual_Piano.Midi.Core;

namespace Virtual_Piano.Midi.Instruments
{
    public sealed class DrumButton : ClickButton, IDrumButton
    {
        //@Command
        public MidiPercussionNote CommandParameter { get; set; }

        protected override void OnClick()
        {
            if (base.Parent is IDrumPanel item)
            {
                item.OnClick(this.CommandParameter);
            }
        }
    }
}