using Virtual_Piano.Notes;

namespace Virtual_Piano.Controls
{
    public class InstrumentCarousel : Notes.Controls.InstrumentCarousel
    {
        public override string GetString(MidiProgram note)
        {
            return App.Resource.GetString(note.ToString());
        }
        public override string GetString(MidiProgramGroup group)
        {
            return App.Resource.GetString(group.ToString());
        }
    }
}