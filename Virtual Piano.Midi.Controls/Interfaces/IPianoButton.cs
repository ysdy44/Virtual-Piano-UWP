namespace Virtual_Piano.Midi.Controls
{
    public interface IPianoButton : IClickButton
    {
        Note CommandParameter { get; set; }
        ToneType Type { get; set; }
    }
}