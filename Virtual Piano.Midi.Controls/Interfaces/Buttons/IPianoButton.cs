namespace Virtual_Piano.Midi.Controls
{
    public interface IPianoButton : IClickButton
    {
        MidiNote CommandParameter { get; set; }
        ToneType Type { get; set; }
    }
}