namespace Virtual_Piano.Midi.Core
{
    public interface IChordButton : IClickButton
    {
        MidiOctave CommandParameter { get; set; }
    }
}