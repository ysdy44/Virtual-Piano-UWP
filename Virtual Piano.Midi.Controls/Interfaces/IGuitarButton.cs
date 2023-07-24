namespace Virtual_Piano.Midi.Controls
{
    public interface IGuitarButton : IClickButton
    {
        MidiNote CommandParameter { get; set; }
        GuitarString Strings { get; set; }
    }
}