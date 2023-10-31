namespace Virtual_Piano.Midi.Core
{
    public interface IInstrumentButton : IClickButton
    {
        MidiProgram CommandParameter { get; set; }
    }
}