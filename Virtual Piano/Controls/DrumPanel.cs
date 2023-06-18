using Virtual_Piano.Notes;

namespace Virtual_Piano.Controls
{
    public sealed class DrumPanel : Notes.Controls.DrumPanel
    {
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString(note.ToString());
        }
    }
}