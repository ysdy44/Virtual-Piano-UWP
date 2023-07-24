using Virtual_Piano.Midi;

namespace Virtual_Piano.Controls
{
    public class DrumSplitView : Midi.Controls.DrumSplitView
    {
        public override string GetString(MidiPercussionNoteCategory category)
        {
            return App.Resource.GetString($"{category}");
        }
    }
}