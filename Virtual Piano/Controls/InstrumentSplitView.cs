using Virtual_Piano.Midi;

namespace Virtual_Piano.Controls
{
    public class InstrumentSplitView : Midi.Instruments.InstrumentSplitView
    {
        public override string GetString(MidiProgramGroup group)
        {
            return App.Resource.GetString($"{group}");
        }
    }
}