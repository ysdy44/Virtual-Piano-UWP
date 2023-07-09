using Virtual_Piano.Notes;

namespace Virtual_Piano.Controls
{
    public class InstrumentSplitView : Notes.Controls.InstrumentSplitView
    {
        public override string GetString(MidiProgramGroup group)
        {
            return App.Resource.GetString(group.ToString());
        }
    }
}