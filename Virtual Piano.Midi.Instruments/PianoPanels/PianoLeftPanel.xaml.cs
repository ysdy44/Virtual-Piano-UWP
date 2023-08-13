using Virtual_Piano.Midi.Core;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Instruments
{
    public sealed partial class PianoLeftPanel : PianoPanel
    {
        public PianoLeftPanel() : base(PianoDirection.Left)
        {
            this.InitializeComponent();
        }
    }
}