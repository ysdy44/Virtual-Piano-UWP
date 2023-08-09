namespace Virtual_Piano.Midi.Controls
{
    public interface IChordButton : IClickButton
    {
        MidiOctave CommandParameter { get; set; }
    }
}