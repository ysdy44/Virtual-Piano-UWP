using Virtual_Piano.Notes;

namespace Virtual_Piano.Controls
{
    public class InstrumentObservableCollection : Notes.Controls.InstrumentObservableCollection
    {
        public override string GetString(MidiProgram program)
        {
            return $"{(int)program} {App.Resource.GetString($"{program}")}";
        }
    }
}