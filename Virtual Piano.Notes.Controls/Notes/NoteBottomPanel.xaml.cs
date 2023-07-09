using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Notes.Controls
{
    public sealed partial class NoteBottomPanel : NotePanel
    {
        public NoteBottomPanel() : base(NoteDirection.Bottom)
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
        public override Brush GetBrush(Octave octave) => this.Resources[$"{octave}"] as SolidColorBrush;
        public override Style GetStyle(ToneType type) => this.Resources[$"{type}"] as Style;
    }
}