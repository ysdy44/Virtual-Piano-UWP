using Virtual_Piano.Notes;

namespace Virtual_Piano.Controls
{
    public sealed class InstrumentCollectionPanel : Notes.Controls.InstrumentCollectionPanel
    {
        public override string GetString(MidiProgram program)
        {
            return $"{(int)program} {App.Resource.GetString($"{program}")}";
        }
    }
}