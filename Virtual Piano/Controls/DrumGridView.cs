using Virtual_Piano.Notes;

namespace Virtual_Piano.Controls
{
    public class DrumGridView : Notes.Controls.DrumGridView
    {
        public DrumGridView() : base(false) { }
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
    public class DrumCategoryGridView : Notes.Controls.DrumGridView
    {
        public DrumCategoryGridView() : base(true) { }
        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
}