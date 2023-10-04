using Virtual_Piano.Midi;
using Virtual_Piano.Strings;

namespace Virtual_Piano.Controls
{
    public class DrumPanel : Midi.Instruments.DrumPanel
    {
        public override string GetString(MidiPercussionNote note)
        {
            return note.GetString();
        }
    }
}