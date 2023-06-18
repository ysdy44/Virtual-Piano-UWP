using Virtual_Piano.Notes;

namespace Virtual_Piano.Controls
{
    public class DrumGridView : Notes.Controls.DrumGridView
    {
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString(note.ToString());
        }
    }
}