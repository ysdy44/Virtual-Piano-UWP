namespace Virtual_Piano.Midi.Core
{
    public interface IPianoButton : IClickButton
    {
        MidiNote CommandParameter { get; set; }
        ToneType Type { get; }
    }
}