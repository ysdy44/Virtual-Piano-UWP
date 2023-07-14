using System.Windows.Input;

namespace Virtual_Piano.Notes.Controls
{
    public interface INotePanel
    {
        ICommand Command { get; set; }

        void OnClick(Note note);
    }
}