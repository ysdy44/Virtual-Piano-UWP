using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed partial class TrackItem : UserControl
    {
        const int Count2 = NoteExtensions.NoteCount / 2;
        const int Count4 = Count2 / 2;

        public string Text { get => this.TextBlock.Text; set => this.TextBlock.Text = value; }

        public TrackItem(MidiTrack info)
        {
            this.InitializeComponent();

            foreach (MidiMessage item in info.Notes)
            {
                int x = item.AbsoluteTime / TrackNoteLayout.Scaling;
                int y = item.Note.ToInedx();
                int w = System.Math.Max(4, item.Duration / TrackNoteLayout.Scaling);

                this.Canvas.Children.Add(new Line
                {
                    X1 = x,
                    Y1 = y - Count4,
                    X2 = x + w,
                    Y2 = y - Count4
                });
            }
        }
    }
}