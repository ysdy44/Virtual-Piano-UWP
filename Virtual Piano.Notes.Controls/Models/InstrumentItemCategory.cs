namespace Virtual_Piano.Notes.Controls
{
    public class InstrumentItemCategory
    {
        public MidiProgramGroup Group;
        public string Text;
        public override string ToString() => this.Text;
    }
}