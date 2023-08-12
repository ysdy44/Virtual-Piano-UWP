using Virtual_Piano.Midi;

namespace Virtual_Piano.Controls
{
    public sealed class KitFlyout : Midi.Instruments.KitFlyout
    {
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
}