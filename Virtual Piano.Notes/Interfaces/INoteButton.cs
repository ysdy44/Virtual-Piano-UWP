namespace Virtual_Piano.Notes.Controls
{
    public interface INoteButton : IClickButton
    {
        Note CommandParameter { get; set; }
        ToneType Type { get; set; }
    }
}