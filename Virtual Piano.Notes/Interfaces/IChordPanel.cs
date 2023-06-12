using System.Windows.Input;

namespace Virtual_Piano.Notes.Controls
{
    public interface IChordPanel
    {
        ICommand Command { get; set; }

        Chord Chord { get; set; }

        IChordButton this[Octave item] { get; }

        void OnClick(Octave octave);
    }
}