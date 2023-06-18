using System.Windows.Input;

namespace Virtual_Piano.Notes.Controls
{
    public interface IDrumPanel
    {
        MidiPercussionNote this[int index] { get; set; }

        ICommand Command { get; set; }

        void OnClick(MidiPercussionNote note);
        string GetString(MidiPercussionNote note);

        void Clear(int index);
        void Add(int index);
        void Clear(MidiPercussionNote note);
        void Add(MidiPercussionNote note);
    }
}