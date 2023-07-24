using Virtual_Piano.Midi;

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
            this.PianoTopPanel[item].Clear();
        }
        public void Add(Note item)
        {
            this.PianoTopPanel[item].Add();
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