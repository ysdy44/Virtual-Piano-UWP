using Virtual_Piano.Notes;

namespace Virtual_Piano.Controls
{
    public sealed class InstrumentGroupPanel : Notes.Controls.InstrumentGroupPanel
    {
        public override string GetString(MidiProgram program)
        {
            return App.Resource.GetString(program.ToString());
        }
    }
}