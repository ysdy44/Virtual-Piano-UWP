namespace Virtual_Piano.Notes.Controls
{
    public struct DrumItem
    {
        public MidiPercussionNote Note;
        public string Text;
        public override string ToString() => this.Text;
    }
}