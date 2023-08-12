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
            base.Initialize();
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                base.Resize(e.NewSize, e.PreviousSize);
            };
        }
        public override Brush GetBrush(MidiOctave octave) => null;
        public override Style GetStyle(ToneType type) => this.Resources[$"{type}"] as Style;
    }
}