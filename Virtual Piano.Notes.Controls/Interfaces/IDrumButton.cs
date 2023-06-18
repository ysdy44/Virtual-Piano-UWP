namespace Virtual_Piano.Notes.Controls
{
    public interface IDrumButton: IClickButton
    {
        MidiPercussionNote CommandParameter { get; set; }
    }
}