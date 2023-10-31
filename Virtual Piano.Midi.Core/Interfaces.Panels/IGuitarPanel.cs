namespace Virtual_Piano.Midi.Core
{
    public interface IGuitarPanel
    {
        void OnClick(MidiNote note, GuitarString strings);
    }
}