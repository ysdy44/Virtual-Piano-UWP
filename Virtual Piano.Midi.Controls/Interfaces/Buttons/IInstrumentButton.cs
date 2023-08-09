namespace Virtual_Piano.Midi.Controls
{
    public interface IInstrumentButton : IClickButton
    {
        MidiProgram CommandParameter { get; set; }
    }
}