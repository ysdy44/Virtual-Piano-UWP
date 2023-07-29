using Virtual_Piano.Midi;

namespace Virtual_Piano.Controls
{
    public sealed class KitPanel : Midi.Controls.KitPanel
    {
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
}