using Virtual_Piano.Midi;

namespace Virtual_Piano.Controls
{
    public sealed class KitFlyout : Midi.Controls.KitFlyout
    {
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
}