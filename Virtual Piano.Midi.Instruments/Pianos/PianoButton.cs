using Virtual_Piano.Midi.Core;

namespace Virtual_Piano.Midi.Instruments
{
    public sealed class PianoWhiteButton : ClickButton, IPianoButton
    {
        //@Command
        public MidiNote CommandParameter { get; set; }
        public ToneType Type => ToneType.White;

        protected override void OnClick()
        {
            if (base.Parent is IPianoPanel item)
            {
                item.OnClick(this.CommandParameter);
            }
        }
    }
    public sealed class PianoBlackButton : ClickButton, IPianoButton
    {
        //@Command
        public MidiNote CommandParameter { get; set; }
        public ToneType Type => ToneType.Black;

        protected override void OnClick()
        {
            if (base.Parent is IPianoPanel item)
            {
                item.OnClick(this.CommandParameter);
            }
        }
    }
}