namespace Virtual_Piano.Midi.Core
{
    public interface IGuitarButton : IClickButton
    {
        MidiNote CommandParameter { get; set; }
        GuitarString Strings { get; set; }
    }
}