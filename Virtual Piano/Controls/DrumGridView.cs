using Virtual_Piano.Midi;

namespace Virtual_Piano.Controls
{
    public class DrumGridView : Midi.Controls.DrumGridView
    {
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
}