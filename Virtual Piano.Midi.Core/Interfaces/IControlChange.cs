namespace Virtual_Piano.Midi.Core
{
    public interface IControlChange : IControl
    {
        MidiControlController Controller { get; }
    }
}