using System.Linq;
using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Midi.Controls
{
    public sealed partial class BaseGuitarPanel : Canvas, IGuitarPanel
    {
        //@Const
        const string Headstock = "Headstock";
        const int Count = 4;
        const int Space = 240 / Count;
        const int Center = 240 / 2;

        //@Command
        public ICommand Command { get; set; }

        readonly Guitar Guitar = new Guitar(1.4);

        bool DisableS1;
        bool DisableS2;
        bool DisableS3;
        bool DisableS4;

        //@Construct
        public BaseGuitarPanel()
        {
            this.InitializeComponent();

            this.Storyboard1.Completed += (s, e) => this.DisableS1 = false;
            this.Storyboard2.Completed += (s, e) => this.DisableS2 = false;
            this.Storyboard3.Completed += (s, e) => this.DisableS3 = false;
            this.Storyboard4.Completed += (s, e) => this.DisableS4 = false;

            this.Initialize(MidiNote.G4, 0, GuitarString.S1);
            this.Initialize(MidiNote.D3, BaseGuitarPanel.Space, GuitarString.S2);
            this.Initialize(MidiNote.A3, BaseGuitarPanel.Space * 2, GuitarString.S3);
            this.Initialize(MidiNote.E3, BaseGuitarPanel.Space * 3, GuitarString.S4);

            foreach (int item in this.Guitar.Inlay1())
            {
                Ellipse ellipse = new Ellipse();
                Canvas.SetLeft(ellipse, item - 6);
                Canvas.SetTop(ellipse, BaseGuitarPanel.Center - 5);
                this.EllipseCanvas.Children.Add(ellipse);
            }

            foreach (int item in this.Guitar.Inlay2())
            {
                Ellipse ellipse1 = new Ellipse();
                Canvas.SetLeft(ellipse1, item - 6);
                Canvas.SetTop(ellipse1, BaseGuitarPanel.Center - BaseGuitarPanel.Space - 5);
                this.EllipseCanvas.Children.Add(ellipse1);

                Ellipse ellipse2 = new Ellipse();
                Canvas.SetLeft(ellipse2, item - 6);
                Canvas.SetTop(ellipse2, BaseGuitarPanel.Center + BaseGuitarPanel.Space - 5);
                this.EllipseCanvas.Children.Add(ellipse2);
            }

            foreach (ItemIndexRange item in this.Guitar.Fretboards)
            {
                this.EllipseCanvas.Children.Add(new Line
                {
                    X1 = item.LastIndex,
                    X2 = item.LastIndex
                });
            }
        }

        private void Initialize(MidiNote note, int y, GuitarString strings)
        {
            base.Children.Add(new GuitarButton
            {
                Strings = strings,
                CommandParameter = note,
                Content = note.ToCDE(),
                Style = base.Resources[BaseGuitarPanel.Headstock] as Style,
                X = 0,
                Y = y,
                Width = this.Guitar.Fretboards.First().Length,
                Height = BaseGuitarPanel.Space,
            });
            note++;

            for (int i = 1; i < Guitar.Count; i++)
            {
                ItemIndexRange item = this.Guitar.Fretboards[i];
                switch (note.ToType())
                {
                    case ToneType.White:
                        base.Children.Add(new GuitarButton
                        {
                            Strings = strings,
                            CommandParameter = note,
                            Content = note.ToCDE(),
                            Style = base.Resources[$"{ToneType.White}"] as Style,
                            X = item.FirstIndex,
                            Y = y,
                            Width = item.Length,
                            Height = BaseGuitarPanel.Space,
                        });
                        break;
                    case ToneType.Black:
                        base.Children.Add(new GuitarButton
                        {
                            Strings = strings,
                            CommandParameter = note,
                            Content = note.ToCDE(),
                            Style = base.Resources[$"{ToneType.Black}"] as Style,
                            X = item.FirstIndex,
                            Y = y,
                            Width = item.Length,
                            Height = BaseGuitarPanel.Space,
                        });
                        break;
                    default:
                        break;
                }
                note++;
            }
        }

        public void OnClick(MidiNote note, GuitarString strings)
        {
            switch (strings)
            {
                case GuitarString.S1:
                    if (this.DisableS1) break;
                    this.DisableS1 = true;

                    this.Command?.Execute(note); // Command
                    this.Storyboard1.Begin(); // Storyboard
                    break;
                case GuitarString.S2:
                    if (this.DisableS2) break;
                    this.DisableS2 = true;

                    this.Command?.Execute(note); // Command
                    this.Storyboard2.Begin(); // Storyboard
                    break;
                case GuitarString.S3:
                    if (this.DisableS3) break;
                    this.DisableS3 = true;

                    this.Command?.Execute(note); // Command
                    this.Storyboard3.Begin(); // Storyboard
                    break;
                case GuitarString.S4:
                    if (this.DisableS4) break;
                    this.DisableS4 = true;

                    this.Command?.Execute(note); // Command
                    this.Storyboard4.Begin(); // Storyboard
                    break;
                default:
                    break;
            }
        }
    }
}