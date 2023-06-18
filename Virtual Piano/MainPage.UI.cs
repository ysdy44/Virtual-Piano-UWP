using Virtual_Piano.Notes;

namespace Virtual_Piano
{
    partial class MainPage
    {
        public void Clear(int item)
        {
            this.DrumPanel.Clear(item);
        }
        public void Add(int item)
        {
            this.DrumPanel.Add(item);
        }

        public void Clear(Note item)
        {
            this.NotePanel[item].Clear();
        }
        public void Add(Note item)
        {
            this.NotePanel[item].Add();
        }

        public void Clear(MidiPercussionNote item)
        {
            this.DrumPanel.Clear(item);
        }
        public void Add(MidiPercussionNote item)
        {
            this.DrumPanel.Add(item);
        }
    }
}