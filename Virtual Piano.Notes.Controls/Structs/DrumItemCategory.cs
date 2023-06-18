namespace Virtual_Piano.Notes.Controls
{
    public struct DrumItemCategory
    {
        public MidiPercussionNoteCategory Category;
        public string Text;
        public override string ToString() => this.Text;
    }
}