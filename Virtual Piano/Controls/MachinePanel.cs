using Virtual_Piano.Midi;

namespace Virtual_Piano.Controls
{
    public sealed class MachinePanel : Midi.Controls.MachinePanel
    {
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
}