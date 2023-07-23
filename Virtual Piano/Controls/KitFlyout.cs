using Virtual_Piano.Notes;

namespace Virtual_Piano.Controls
{
    public sealed class KitFlyout : Notes.Controls.KitFlyout
    {
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
}