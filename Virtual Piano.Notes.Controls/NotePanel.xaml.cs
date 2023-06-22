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
        public double WhiteSize { get => base.MaxHeight; set => base.MaxHeight = value; }
        public double BlackSize { get => base.MinHeight; set => base.MinHeight = value; }

        private NoteSize NoteSize = new NoteSize(28);
        public int ItemSize
        {
            get => this.NoteSize.ItemSize;
            set
            {
                if (this.NoteSize.ItemSize == value) return;
                this.NoteSize = new NoteSize(value);

                //       if (base.IsLoaded is false) return;

                int count = 0;
                foreach (INoteButton item in base.Children.Cast<INoteButton>())
                {
                    switch (item.Type)
                    {
                        case ToneType.White:
                            count++;
                            item.X = item.TabIndex * this.NoteSize.WhiteSize;
                            item.Width = this.NoteSize.WhiteSize;
                            break;
                        case ToneType.Black:
                            item.X = item.TabIndex * this.NoteSize.WhiteSize - this.NoteSize.BlackSizeHalf;
                            item.Width = this.NoteSize.BlackSize;
                            break;
                        default:
                            break;
                    }
                }
                base.Width = (count - 1) * this.NoteSize.WhiteSize;
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
                            item.Content = null;
                        }
                        break;
                    case KeyLabel.CDE:
                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            item.Content = item.CommandParameter.ToCDE();
                        }
                        break;
                    case KeyLabel.DoReMi:
                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            item.Content = item.CommandParameter.ToTone().ToString();
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
                            item.X = item.TabIndex * this.NoteSize.WhiteSize;
                            break;
                        case ToneType.Black:
                            item.X = item.TabIndex * this.NoteSize.WhiteSize - this.NoteSize.BlackSizeHalf;
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

                double height = e.NewSize.Height * this.BlackSize / this.WhiteSize;
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
                            Content = this.Label == KeyLabel.Off ? null : this.Label == KeyLabel.DoReMi ? tone.ToString() : octave.ToCDE(tone),
                            TabIndex = white,
                            Foreground = brush,
                            Type = ToneType.White,
                            CommandParameter = note,
                            Style = this.Resources[$"{ToneType.White}Style"] as Style,
                            Width = this.NoteSize.WhiteSize,
                        });
                        break;
                    case ToneType.Black:
                        int black = index + 2 + tone.ToIndex();
                        base.Children.Add(new NoteButton
                        {
                            Content = this.Label == KeyLabel.Off ? null : this.Label == KeyLabel.DoReMi ? tone.ToString() : octave.ToCDE(tone),
                            TabIndex = black,
                            Foreground = brush,
                            CommandParameter = note,
                            Type = ToneType.Black,
                            Style = this.Resources[$"{ToneType.Black}Style"] as Style,
                            X = black * this.NoteSize.WhiteSize - this.NoteSize.BlackSizeHalf,
                            Width = this.NoteSize.BlackSize,
                        });
                        break;
                    default:
                        break;
                }
            }
            base.Width = (count - 1) * this.NoteSize.WhiteSize;
        }

        public void OnClick(Note note) => this.Command?.Execute(note); // Command

        public void Clear(int index)
        {
            if (base.Children[index] is INoteButton item)
            {
                item.Clear();
            }
        }
        public void Add(int index)
        {
            if (base.Children[index] is INoteButton item)
            {
                item.Add();
            }
        }

        public void Clear(Note note)
        {
            int i = (int)note;
            if (base.Children[i] is INoteButton item)
            {
                item.Clear();
            }
        }
        public void Add(Note note)
        {
            int i = (int)note;
            if (base.Children[i] is INoteButton item)
            {
                item.Add();
            }
        }
    }
}