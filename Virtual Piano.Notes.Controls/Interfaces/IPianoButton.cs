namespace Virtual_Piano.Notes.Controls
{
    public interface IPianoButton : IClickButton
    {
        Note CommandParameter { get; set; }
        ToneType Type { get; set; }
    }
}