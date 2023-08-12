using Virtual_Piano.Midi.Core;

namespace Virtual_Piano.Midi.Controls
{
    public interface IGuitarPanel
    {
        void OnClick(MidiNote note, GuitarString strings);
    }
}