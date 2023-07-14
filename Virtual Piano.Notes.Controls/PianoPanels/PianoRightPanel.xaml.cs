using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Notes.Controls
{
    public sealed partial class PianoRightPanel : PianoPanel
    {
        public PianoRightPanel() : base(PianoDirection.Right)
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
        public override Brush GetBrush(Octave octave) => null;
        public override Style GetStyle(ToneType type) => this.Resources[$"{type}"] as Style;
    }
}