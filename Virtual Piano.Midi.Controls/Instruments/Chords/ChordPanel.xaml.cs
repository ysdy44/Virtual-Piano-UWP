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
        public static readonly MidiOctave Octave = MidiOctave.Number8;
        public static readonly MidiOctave[] Octaves = new MidiOctave[]
        {
            MidiOctave.Number8,
            MidiOctave.Number7,
            MidiOctave.Number6,

            // C
            MidiOctave.Number5,

            MidiOctave.Number4,
            MidiOctave.Number3,
            MidiOctave.Number2,
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
                    foreach (IChordButton item in base.Children.Cast<IChordButton>())
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
                        IChordButton item = base.Children[i] as IChordButton;
                        item.Y = i * (height + spacing);
                        item.Height = height;
                    }
                }
            };

            foreach (MidiOctave item in ChordPanel.Octaves)
            {
                base.Children.Add(new ChordButton
                {
                    Content = item == ChordPanel.Octave ? (object)this.Chord : null,
                    Background = this.Resources[$"{item}"] as SolidColorBrush,
                    CommandParameter = item
                });
            }
        }

        public async void OnClick(MidiOctave octave)
        {
            this.Command?.Execute(this.Chords.Play(octave)); // Command
            foreach (MidiNote item in this.Chords.Plays(octave))
            {
                await Task.Delay(5);
                this.Command?.Execute(item); // Command
            }
        }
    }
}