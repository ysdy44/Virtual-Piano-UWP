using Virtual_Piano.Midi.Core;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Instruments
{
    public sealed partial class PianoTopPanel : PianoPanel
    {
        public PianoTopPanel() : base(PianoDirection.Top)
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
        public override Brush GetBrush(MidiOctave octave) => this.Resources[$"{octave}"] as SolidColorBrush;
        public override Style GetStyle(ToneType type) => this.Resources[$"{type}"] as Style;
    }
}