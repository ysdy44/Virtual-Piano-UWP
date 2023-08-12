namespace Virtual_Piano.Midi.Controls
{
    public class InstrumentItemCategory
    {
        public MidiProgramGroup Group;
        public string Text => ToString();
        public override string ToString() => MidiProgramFactory.Emoji[Group];
    }
}