using Virtual_Piano.Midi;

namespace Virtual_Piano.Controls
{
    public sealed class PadPanel : Midi.Instruments.PadPanel
    {
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
}