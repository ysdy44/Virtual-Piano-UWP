namespace Virtual_Piano.Notes.Controls
{
    public interface IGuitarButton : IClickButton
    {
        Note CommandParameter { get; set; }
        GuitarString Strings { get; set; }
    }
}