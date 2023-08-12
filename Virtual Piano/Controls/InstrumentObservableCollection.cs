using Virtual_Piano.Midi;

namespace Virtual_Piano.Controls
{
    public class InstrumentObservableCollection : Midi.Instruments.InstrumentObservableCollection
    {
        public override string GetString(MidiProgram program)
        {
            return $"{(int)program} {App.Resource.GetString($"{program}")}";
        }
    }
}