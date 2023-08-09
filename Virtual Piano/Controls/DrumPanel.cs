using Virtual_Piano.Midi;

namespace Virtual_Piano.Controls
{
    public class DrumPanel : Midi.Controls.DrumPanel
    {
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
}