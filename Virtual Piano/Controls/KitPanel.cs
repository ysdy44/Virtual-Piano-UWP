using Virtual_Piano.Midi;

namespace Virtual_Piano.Controls
{
    public sealed class KitPanel : Midi.Instruments.KitPanel
    {
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
}