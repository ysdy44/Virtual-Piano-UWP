namespace Virtual_Piano.Notes.Controls
{
    public class InstrumentItemCategory
    {
        public MidiProgramGroup Group;
        public string Text => this.ToString();
        public override string ToString() => MidiProgramFactory.Emoji[this.Group];
    }
}