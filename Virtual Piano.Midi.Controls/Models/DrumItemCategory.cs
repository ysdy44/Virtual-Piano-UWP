namespace Virtual_Piano.Midi.Core
{
    public class DrumItemCategory
    {
        public MidiPercussionNoteCategory Category;
        public string Text;
        public override string ToString() => this.Text;
    }
}