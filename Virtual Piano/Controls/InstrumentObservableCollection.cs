using Virtual_Piano.Midi;
using Virtual_Piano.Strings;

namespace Virtual_Piano.Controls
{
    public class InstrumentObservableCollection : Midi.Core.InstrumentObservableCollection
    {
        public override string GetString(MidiProgram program)
        {
            return $"{(int)program} {program.GetString()}";
        }
    }
}