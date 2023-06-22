using System.Windows.Input;

namespace Virtual_Piano.Notes.Controls
{
    public interface INotePanel
    {
        ICommand Command { get; set; }
        double WhiteSize { get; set; }
        double BlackSize { get; set; }

        int ItemSize { get; set; }
        KeyLabel Label { get; set; }

        INoteButton this[Note item] { get; }

        void OnClick(Note note);

        void Clear(int index);
        void Add(int index);
        void Clear(Note note);
        void Add(Note note);
    }
}