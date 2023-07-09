using Virtual_Piano.Notes;

namespace Virtual_Piano.Controls
{
    public sealed class InstrumentListView : Notes.Controls.InstrumentListView
    {
        public override string GetString(MidiProgram program)
        {
            return App.Resource.GetString(program.ToString());
        }
    }
}