using Virtual_Piano.Midi.Core;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Instruments
{
    public sealed partial class PianoBottomPanel : PianoPanel
    {
        public PianoBottomPanel() : base(PianoDirection.Bottom)
        {
            this.InitializeComponent();
        }
    }
}