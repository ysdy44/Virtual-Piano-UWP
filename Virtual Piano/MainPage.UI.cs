using Virtual_Piano.Notes;

namespace Virtual_Piano
{
    partial class MainPage
    {
        public void Clear(int item)
        {
            this.DrumPanel[item].Clear();

        }
        public void Add(int item)
        {
            this.DrumPanel[item].Add();
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
            this.DrumPanel[item].Clear();
        }
        public void Add(MidiPercussionNote item)
        {
            this.DrumPanel[item].Add();
        }
    }
}