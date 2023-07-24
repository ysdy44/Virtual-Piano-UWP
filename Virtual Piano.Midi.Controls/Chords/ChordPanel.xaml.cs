using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Controls
{
    public sealed partial class ChordPanel : Canvas, IChordPanel
    {
        public static readonly Octave Octave = Octave.Number8;
        public static readonly Octave[] Octaves = new Octave[]
        {
            Octave.Number8,
            Octave.Number7,
            Octave.Number6,

            // C
            Octave.Number5,

            Octave.Number4,
            Octave.Number3,
            Octave.Number2,
        };

        //@Command
        public ICommand Command { get; set; }
        public Chord Chord
        {
            get => this.Chords.Source;
            set
            {
                this.Chords = new Chords(value);
                if (base.Children.FirstOrDefault() is ChordButton chordButton)
                {
                    chordButton.Content = value;
                }
            }
        }
        Chords Chords = new Chords(Chord.C);

        public ChordPanel()
        {
            this.InitializeComponent();
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                if (e.NewSize.Width != e.PreviousSize.Width)
                {
                    foreach (IClickButton item in base.Children.Cast<IClickButton>())
                    {
                        item.Width = e.NewSize.Width;
                    }
                }

                if (e.NewSize.Height != e.PreviousSize.Height)
                {
                    const double spacing = 1;
                    int count = base.Children.Count;

                    double length = e.NewSize.Height - spacing * count + spacing;
                    double height = length / count;

                    for (int i = 0; i < count; i++)
                    {
                        IClickButton item = base.Children[i] as IClickButton;
                        item.Y = i * (height + spacing);
                        item.Height = height;
                    }
                }
            };

            foreach (Octave item in ChordPanel.Octaves)
            {
                base.Children.Add(new ChordButton
                {
                    Content = item == ChordPanel.Octave ? (object)this.Chord : null,
                    Background = this.Resources[$"{item}"] as SolidColorBrush,
                    CommandParameter = item
                });
            }
        }

        public async void OnClick(Octave octave)
        {
            this.Command?.Execute(this.Chords.Play(octave)); // Command
            foreach (Note item in this.Chords.Plays(octave))
            {
                await Task.Delay(5);
                this.Command?.Execute(item); // Command
            }
        }
    }
}