namespace Virtual_Piano.Midi.Controls
{
    public interface IDrumButton : IClickButton
    {
        MidiPercussionNote CommandParameter { get; set; }
    }
}