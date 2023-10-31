namespace Virtual_Piano.Midi.Core
{
    public interface IDrumButton : IClickButton
    {
        MidiPercussionNote CommandParameter { get; set; }
    }
}