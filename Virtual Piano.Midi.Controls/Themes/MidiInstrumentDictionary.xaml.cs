using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Controls
{
    public sealed partial class InstrumentDictionary : ResourceDictionary
    {
        public Geometry this[MidiInstrument instrument] => this.FindGeometry($"{instrument}");
        public InstrumentDictionary()
        {
            this.InitializeComponent();
        }
    }
}