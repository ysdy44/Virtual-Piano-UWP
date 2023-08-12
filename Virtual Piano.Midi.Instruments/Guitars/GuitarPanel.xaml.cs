using System.Linq;
using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Midi.Instruments
{
    public sealed partial class GuitarPanel : Canvas, IGuitarPanel
    {
        //@Const
        const string Headstock = "Headstock";
        const int Space = 240 / 6;
        const int Center = 240 / 2;

        //@Command
        public ICommand Command { get; set; }

        readonly Guitar Guitar = new Guitar(1.4);

        bool DisableC1;
        bool DisableC2;
        bool DisableC3;
        bool DisableC4;
        bool DisableC5;
        bool DisableC6;

        //@Construct
        public GuitarPanel()
        {
            this.InitializeComponent();

            this.Storyboard1.Completed += (s, e) => this.DisableC1 = false;
            this.Storyboard2.Completed += (s, e) => this.DisableC2 = false;
            this.Storyboard3.Completed += (s, e) => this.DisableC3 = false;
            this.Storyboard4.Completed += (s, e) => this.DisableC4 = false;
            this.Storyboard5.Completed += (s, e) => this.DisableC5 = false;
            this.Storyboard6.Completed += (s, e) => this.DisableC6 = false;

            this.Initialize(MidiNote.E6, 0, GuitarString.S1);
            this.Initialize(MidiNote.B5, GuitarPanel.Space, GuitarString.S2);
            this.Initialize(MidiNote.G5, GuitarPanel.Space * 2, GuitarString.S3);
            this.Initialize(MidiNote.D5, GuitarPanel.Space * 3, GuitarString.S4);
            this.Initialize(MidiNote.A4, GuitarPanel.Space * 4, GuitarString.S5);
            this.Initialize(MidiNote.E4, GuitarPanel.Space * 5, GuitarString.S6);

            foreach (int item in this.Guitar.Inlay1())
            {
                Ellipse ellipse = new Ellipse();
                Canvas.SetLeft(ellipse, item - 6);
                Canvas.SetTop(ellipse, GuitarPanel.Center - 5);
                this.EllipseCanvas.Children.Add(ellipse);
            }

            foreach (int item in this.Guitar.Inlay2())
            {
                Ellipse ellipse1 = new Ellipse();
                Canvas.SetLeft(ellipse1, item - 6);
                Canvas.SetTop(ellipse1, GuitarPanel.Center - GuitarPanel.Space - 5);
                this.EllipseCanvas.Children.Add(ellipse1);

                Ellipse ellipse2 = new Ellipse();
                Canvas.SetLeft(ellipse2, item - 6);
                Canvas.SetTop(ellipse2, GuitarPanel.Center + GuitarPanel.Space - 5);
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
                Style = base.Resources[GuitarPanel.Headstock] as Style,
                X = 0,
                Y = y,
                Width = this.Guitar.Fretboards.First().Length,
                Height = GuitarPanel.Space,
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
                            Height = GuitarPanel.Space,
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
                            Height = GuitarPanel.Space,
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
                    if (this.DisableC1) break;
                    this.DisableC1 = true;

                    this.Command?.Execute(note); // Command
                    this.Storyboard1.Begin(); // Storyboard
                    break;
                case GuitarString.S2:
                    if (this.DisableC2) break;
                    this.DisableC2 = true;

                    this.Command?.Execute(note); // Command
                    this.Storyboard2.Begin(); // Storyboard
                    break;
                case GuitarString.S3:
                    if (this.DisableC3) break;
                    this.DisableC3 = true;

                    this.Command?.Execute(note); // Command
                    this.Storyboard3.Begin(); // Storyboard
                    break;
                case GuitarString.S4:
                    if (this.DisableC4) break;
                    this.DisableC4 = true;

                    this.Command?.Execute(note); // Command
                    this.Storyboard4.Begin(); // Storyboard
                    break;
                case GuitarString.S5:
                    if (this.DisableC5) break;
                    this.DisableC5 = true;

                    this.Command?.Execute(note); // Command
                    this.Storyboard5.Begin(); // Storyboard
                    break;
                case GuitarString.S6:
                    if (this.DisableC6) break;
                    this.DisableC6 = true;

                    this.Command?.Execute(note); // Command
                    this.Storyboard6.Begin(); // Storyboard
                    break;
                default:
                    break;
            }
        }
    }
}