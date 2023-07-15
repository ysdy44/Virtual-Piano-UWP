using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Notes.Controls
{
    public sealed partial class GuitarPanel : Canvas, IGuitarPanel
    {
        //@Const
        const string Headstock = "Headstock";

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

            this.Initialize(Note.E6, 0, GuitarString.S1);
            this.Initialize(Note.B5, 40, GuitarString.S2);
            this.Initialize(Note.G5, 80, GuitarString.S3);
            this.Initialize(Note.D5, 120, GuitarString.S4);
            this.Initialize(Note.A4, 160, GuitarString.S5);
            this.Initialize(Note.E4, 200, GuitarString.S6);

            foreach (int item in this.Guitar.Inlay1())
            {
                Ellipse ellipse = new Ellipse();
                Canvas.SetLeft(ellipse, item - 6);
                Canvas.SetTop(ellipse, 120 - 5);
                this.EllipseCanvas.Children.Add(ellipse);
            }

            foreach (int item in this.Guitar.Inlay2())
            {
                Ellipse ellipse1 = new Ellipse();
                Canvas.SetLeft(ellipse1, item - 6);
                Canvas.SetTop(ellipse1, 120 - 5 - 40);
                this.EllipseCanvas.Children.Add(ellipse1);

                Ellipse ellipse2 = new Ellipse();
                Canvas.SetLeft(ellipse2, item - 6);
                Canvas.SetTop(ellipse2, 120 - 5 + 40);
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

        private void Initialize(Note note, int y, GuitarString strings)
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
                Height = 40,
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
                            Height = 40,
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
                            Height = 40,
                        });
                        break;
                    default:
                        break;
                }
                note++;
            }
        }

        public void OnClick(Note note, GuitarString strings)
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