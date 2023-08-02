using System.Windows.Input;

namespace Virtual_Piano.Midi.Controls
{
    public interface INotePanel
    {
        //@Command
        ICommand Command { get; set; }

        void OnClick(MidiNote note);
    }
}