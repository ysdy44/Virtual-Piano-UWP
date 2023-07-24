using System.Windows.Input;

namespace Virtual_Piano.Midi.Controls
{
    public interface IPianoPanel
    {
        ICommand Command { get; set; }
        double WhiteSize { get; set; }
        double BlackSize { get; set; }

        int ItemSize { get; set; }
        KeyLabel Label { get; set; }

        IPianoButton this[Note item] { get; }

        void OnClick(Note note);

        void Clear(int index);
        void Add(int index);
        void Clear(Note note);
        void Add(Note note);
    }
}