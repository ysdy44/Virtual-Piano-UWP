using Virtual_Piano.Midi;
using Virtual_Piano.Strings;

namespace Virtual_Piano.Controls
{
    public sealed class InstrumentCollectionPanel : Midi.Instruments.InstrumentCollectionPanel
    {
        public override string GetString(MidiProgram program)
        {
            return $"{(int)program} {program.GetString()}";
        }
    }
}