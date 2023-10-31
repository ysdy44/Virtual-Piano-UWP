namespace Virtual_Piano.Midi.Core
{
    public interface IChordPanel
    {
        Chord Chord { get; set; }
        void OnClick(MidiOctave octave);
    }
}