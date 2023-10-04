using Virtual_Piano.Midi;
using Virtual_Piano.Strings;

namespace Virtual_Piano.Controls
{
    public class InstrumentSplitView : Midi.Instruments.InstrumentSplitView
    {
        public override string GetString(MidiProgramGroup group)
        {
            return group.GetString();
        }
    }
}