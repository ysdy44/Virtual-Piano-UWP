using Virtual_Piano.Notes;

namespace Virtual_Piano.Controls
{
    public class DrumSplitView : Notes.Controls.DrumSplitView
    {
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString(note.ToString());
        }
        public override string GetString(MidiPercussionNoteCategory category)
        {
            return App.Resource.GetString(category.ToString());
        }
    }
}