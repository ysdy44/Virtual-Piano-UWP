namespace Virtual_Piano.Midi.Controls
{
    public interface IGuitarButton : IClickButton
    {
        Note CommandParameter { get; set; }
        GuitarString Strings { get; set; }
    }
}