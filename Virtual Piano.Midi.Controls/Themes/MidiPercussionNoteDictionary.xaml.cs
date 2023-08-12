using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Controls
{
    public sealed partial class MidiPercussionNoteDictionary : ResourceDictionary
    {
        public Geometry this[MidiPercussionNote note] => this.FindGeometry($"{note}");
        public MidiPercussionNoteDictionary()
        {
            this.InitializeComponent();
        }
    }
}