using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Core
{
    public sealed partial class MidiInstrumentDictionary : ResourceDictionary
    {
        public Geometry this[MidiInstrument instrument] => this.FindGeometry($"{instrument}");
        public MidiInstrumentDictionary()
        {
            this.InitializeComponent();
        }
    }
}