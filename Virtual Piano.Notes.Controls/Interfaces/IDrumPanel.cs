using System.Windows.Input;

namespace Virtual_Piano.Notes.Controls
{
    public interface IDrumPanel
    {
        IDrumButton this[int item] { get; }
        IDrumButton this[MidiPercussionNote item] { get; }

        ICommand Command { get; set; }

        void OnClick(MidiPercussionNote note);
    }
}