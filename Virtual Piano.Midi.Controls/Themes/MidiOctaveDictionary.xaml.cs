using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Controls
{
    public sealed partial class MidiOctaveDictionary : ResourceDictionary
    {
        public Brush this[MidiOctave octave] => this.FindBrush($"{octave}");
        public MidiOctaveDictionary()
        {
            this.InitializeComponent();
        }
    }
}