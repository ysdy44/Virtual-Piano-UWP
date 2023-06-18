using System.Linq;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Notes.Controls
{
    public sealed partial class NotePanel : Canvas, INotePanel
    {
        //@Command
        public ICommand Command { get; set; }
        public double WhiteHeight { get => base.MaxHeight; set => base.MaxHeight = value; }
        public double BlackHeight { get => base.MinHeight; set => base.MinHeight = value; }

        private NoteSize NoteSize = new NoteSize(28);
        public int ItemWidth
        {
            get => this.NoteSize.ItemWidth;
            set
            {
                if (this.NoteSize.ItemWidth == value) return;
                this.NoteSize = new NoteSize(value);

                //       if (base.IsLoaded is false) return;

                int count = 0;
                foreach (INoteButton item in base.Children.Cast<INoteButton>())
                {
                    switch (item.Type)
                    {
                        case ToneType.White:
                            count++;
                            item.X = item.TabIndex * this.NoteSize.WhiteWidth;
                            item.Width = this.NoteSize.WhiteWidth;
                            break;
                        case ToneType.Black:
                            item.X = item.TabIndex * this.NoteSize.WhiteWidth - this.NoteSize.BlackWidthHalf;
                            item.Width = this.NoteSize.BlackWidth;
                            break;
                        default:
                            break;
                    }
                }
                base.Width = (count - 1) * this.NoteSize.WhiteWidth;
            }
        }

        private KeyLabel label;
        public KeyLabel Label
        {
            get => this.label;
            set
            {
                if (this.label == value) return;
                this.label = value;

                //     if (base.IsLoaded is false) return;

                switch (value)
                {
                    case KeyLabel.Off:
                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            item.Tag = null;
                        }
                        break;
                    case KeyLabel.CDE:
                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            item.Tag = item.CommandParameter.ToCDE();
                        }
                        break;
                    case KeyLabel.DoReMi:
                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            item.Tag = item.CommandParameter.ToTone().ToString();
                        }
                        break;
                    default:
                        break;
                }
                foreach (INoteButton item in base.Children.Cast<INoteButton>())
                {
                    switch (item.Type)
                    {
                        case ToneType.White:
                            item.X = item.TabIndex * this.NoteSize.WhiteWidth;
                            break;
                        case ToneType.Black:
                            item.X = item.TabIndex * this.NoteSize.WhiteWidth - this.NoteSize.BlackWidthHalf;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public INoteButton this[Note item] => base.Children[(int)item] as INoteButton;

        public NotePanel()
        {
            this.InitializeComponent();
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                if (e.NewSize.Height == e.PreviousSize.Height) return;

                double height = e.NewSize.Height * this.BlackHeight / this.WhiteHeight;
                foreach (INoteButton item in base.Children.Cast<INoteButton>())
                {
                    switch (item.Type)
                    {
                        case ToneType.White:
                            item.Height = e.NewSize.Height;
                            break;
                        case ToneType.Black:
                            item.Height = height;
                            break;
                        default:
                            break;
                    }
                }
            };

            int count = 0;
            foreach (Note note in System.Enum.GetValues(typeof(Note)).Cast<Note>())
            {
                Octave octave = note.ToOctave();
                Brush brush = this.Resources[$"{octave}Brush"] as SolidColorBrush;
                int index = (int)octave * NoteExtensions.WhiteCount;

                Tone tone = note.ToTone();
                switch (tone.ToType())
                {
                    case ToneType.White:
                        count++;
                        int white = index + tone.ToIndex();
                        base.Children.Add(new NoteButton
                        {
                            Tag = this.Label == KeyLabel.Off ? null : this.Label == KeyLabel.DoReMi ? tone.ToString() : octave.ToCDE(tone),
                            TabIndex = white,
                            Foreground = brush,
                            Type = ToneType.White,
                            CommandParameter = note,
                            Style = this.Resources[$"{ToneType.White}Style"] as Style,
                            Width = this.NoteSize.WhiteWidth,
                        });
                        break;
                    case ToneType.Black:
                        int black = index + 2 + tone.ToIndex();
                        base.Children.Add(new NoteButton
                        {
                            Tag = this.Label == KeyLabel.Off ? null : this.Label == KeyLabel.DoReMi ? tone.ToString() : octave.ToCDE(tone),
                            TabIndex = black,
                            Foreground = brush,
                            CommandParameter = note,
                            Type = ToneType.Black,
                            Style = this.Resources[$"{ToneType.Black}Style"] as Style,
                            X = black * this.NoteSize.WhiteWidth - this.NoteSize.BlackWidthHalf,
                            Width = this.NoteSize.BlackWidth,
                        });
                        break;
                    default:
                        break;
                }
            }
            base.Width = (count - 1) * this.NoteSize.WhiteWidth;
        }

        public void OnClick(Note note) => this.Command?.Execute(note); // Command
    }
}