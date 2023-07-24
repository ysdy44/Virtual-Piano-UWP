using Virtual_Piano.Midi;

namespace Virtual_Piano.Controls
{
    public class InstrumentObservableCollection : Midi.Controls.InstrumentObservableCollection
    {
        public override string GetString(MidiProgram program)
        {
            return $"{(int)program} {App.Resource.GetString($"{program}")}";
        }
    }
}