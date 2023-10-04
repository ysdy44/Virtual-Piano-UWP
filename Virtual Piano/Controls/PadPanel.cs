using Virtual_Piano.Midi;
using Virtual_Piano.Strings;

namespace Virtual_Piano.Controls
{
    public sealed class PadPanel : Midi.Instruments.PadPanel
    {
        public override string GetString(MidiPercussionNote note)
        {
            return note.GetString();
        }
    }
}