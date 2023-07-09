using System.Windows.Input;

namespace Virtual_Piano.Notes.Controls
{
    public interface IChordPanel
    {
        ICommand Command { get; set; }

        Chord Chord { get; set; }

        void OnClick(Octave octave);
    }
}