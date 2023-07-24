using Virtual_Piano.Midi;

namespace Virtual_Piano.Controls
{
    public class DrumGridView : Midi.Controls.DrumGridView
    {
        public DrumGridView() : base(false) { }
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
    public class DrumCategoryGridView : Midi.Controls.DrumGridView
    {
        public DrumCategoryGridView() : base(true) { }
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
}