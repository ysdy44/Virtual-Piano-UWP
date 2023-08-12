using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Core
{
    public sealed partial class MidiPercussionNoteCategoryDictionary : ResourceDictionary
    {
        public Brush this[MidiPercussionNoteCategory category] => this.FindBrush($"{category}");
        public MidiPercussionNoteCategoryDictionary()
        {
            this.InitializeComponent();
        }
    }
}