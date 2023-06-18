namespace Virtual_Piano.Notes.Controls
{
    public interface IChordButton : IClickButton
    {
        Octave CommandParameter { get; set; }
    }
}