using System.Windows.Input;

namespace Virtual_Piano.Midi.Controls
{
    public interface IInstrumentPanel
    {
        ICommand Command { get; set; }

        void OnClick(MidiProgram program);
        string GetString(MidiProgram note);
    }
}