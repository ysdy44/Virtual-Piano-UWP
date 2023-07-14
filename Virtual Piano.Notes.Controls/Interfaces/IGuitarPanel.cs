using System.Windows.Input;

namespace Virtual_Piano.Notes.Controls
{
    public interface IGuitarPanel
    {
        ICommand Command { get; set; }

        void OnClick(Note note, GuitarString strings);
    }
}