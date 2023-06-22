using System.Linq;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Notes.Controls
{
    public enum NoteDirection
    {
        Left,
        Top,
        Right,
        Bottom
    }

    public abstract partial class NotePanel : Canvas, INotePanel
    {
        //@Command
        public ICommand Command { get; set; }

        public double WhiteSize
        {
            get
            {
                switch (this.Direction)
                {
                    case NoteDirection.Left: case NoteDirection.Right: return base.MaxWidth;
                    case NoteDirection.Top: case NoteDirection.Bottom: return base.MaxHeight;
                    default: return 0;
                }
            }
            set
            {
                switch (this.Direction)
                {
                    case NoteDirection.Left: case NoteDirection.Right: base.MaxWidth = value; break;
                    case NoteDirection.Top: case NoteDirection.Bottom: base.MaxHeight = value; break;
                    default: break;
                }
            }
        }

        public double BlackSize
        {
            get
            {
                switch (this.Direction)
                {
                    case NoteDirection.Left: case NoteDirection.Right: return base.MinWidth;
                    case NoteDirection.Top: case NoteDirection.Bottom: return base.MinHeight;
                    default: return 0;
                }
            }
            set
            {
                switch (this.Direction)
                {
                    case NoteDirection.Left: case NoteDirection.Right: base.MinWidth = value; break;
                    case NoteDirection.Top: case NoteDirection.Bottom: base.MinHeight = value; break;
                    default: break;
                }
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

                foreach (INoteButton item in base.Children.Cast<INoteButton>())
                {
                    item.Content = item.CommandParameter.ToLabel(value);
                }
            }
        }

        public INoteButton this[Note item] => base.Children[(int)item] as INoteButton;

        readonly NoteDirection Direction;
        public NotePanel(NoteDirection direction) => this.Direction = direction;

        public void OnClick(Note note) => this.Command?.Execute(note); // Command

        public abstract Brush GetBrush(Octave octave);
        public abstract Style GetStyle(ToneType type);
        public int GetIndex(Note note)
        {
            switch (this.Direction)
            {
                case NoteDirection.Left: case NoteDirection.Bottom: return NoteExtensions.NoteCount - (int)note - 1;
                case NoteDirection.Top: case NoteDirection.Right: return (int)note;
                default: return 0;
            }
        }

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

    partial class NotePanel
    {
        public Rect GetRect(int count, ToneType type)
        {
            switch (type)
            {
                case ToneType.White:
                    switch (this.Direction)
                    {
                        case NoteDirection.Left:
                            return new Rect
                            {
                                X = 0,
                                Y = (NoteExtensions.NoteWhiteCount - count - 1) * this.NoteSize.WhiteSize,
                                Width = double.NaN,
                                Height = this.NoteSize.WhiteSize
                            };
                        case NoteDirection.Top:
                            return new Rect
                            {
                                X = count * this.NoteSize.WhiteSize,
                                Y = 0,
                                Width = this.NoteSize.WhiteSize,
                                Height = double.NaN,
                            };
                        case NoteDirection.Right:
                            return new Rect
                            {
                                X = 0,
                                Y = count * this.NoteSize.WhiteSize,
                                Width = double.NaN,
                                Height = this.NoteSize.WhiteSize
                            };
                        case NoteDirection.Bottom:
                            return new Rect
                            {
                                X = (NoteExtensions.NoteWhiteCount - count - 1) * this.NoteSize.WhiteSize,
                                Y = 0,
                                Width = this.NoteSize.WhiteSize,
                                Height = double.NaN,
                            };
                        default: return Rect.Empty;
                    }
                case ToneType.Black:
                    switch (this.Direction)
                    {
                        case NoteDirection.Left:
                            return new Rect
                            {
                                X = 0,
                                Y = (NoteExtensions.NoteWhiteCount - count + 1) * this.NoteSize.WhiteSize - this.NoteSize.BlackSizeHalf,
                                Width = double.NaN,
                                Height = this.NoteSize.BlackSize
                            };
                        case NoteDirection.Top:
                            return new Rect
                            {
                                X = (count + 1) * this.NoteSize.WhiteSize - this.NoteSize.BlackSizeHalf,
                                Y = 0,
                                Width = this.NoteSize.BlackSize,
                                Height = double.NaN,
                            };
                        case NoteDirection.Right:
                            return new Rect
                            {
                                X = 0,
                                Y = count * this.NoteSize.WhiteSize,
                                Width = double.NaN,
                                Height = this.NoteSize.BlackSize
                            };
                        case NoteDirection.Bottom:
                            return new Rect
                            {
                                X = (NoteExtensions.NoteWhiteCount - count + 1) * this.NoteSize.WhiteSize - this.NoteSize.BlackSizeHalf,
                                Y = 0,
                                Width = this.NoteSize.BlackSize,
                                Height = double.NaN,
                            };
                        default: return Rect.Empty;
                    }
                default: return Rect.Empty;
            }
        }

        public void Initialize()
        {
            int count = 0;
            foreach (Note note in System.Enum.GetValues(typeof(Note)).Cast<Note>())
            {
                switch (note.ToTone().ToType())
                {
                    case ToneType.White:
                        Rect white = this.GetRect(count, ToneType.White);
                        base.Children.Add(new NoteButton
                        {
                            Type = ToneType.White,
                            CommandParameter = note,
                            Content = note.ToLabel(this.Label),
                            TabIndex = this.GetIndex(note),
                            Foreground = this.GetBrush(note.ToOctave()),
                            Style = this.GetStyle(ToneType.White),
                            X = white.X,
                            Y = white.Y,
                            Width = white.Width,
                            Height = white.Height,
                        });
                        count++;
                        break;
                    case ToneType.Black:
                        Rect black = this.GetRect(count, ToneType.Black);
                        base.Children.Add(new NoteButton
                        {
                            Type = ToneType.Black,
                            Style = this.GetStyle(ToneType.Black),
                            Content = note.ToLabel(this.Label),
                            TabIndex = this.GetIndex(note),
                            Foreground = this.GetBrush(note.ToOctave()),
                            CommandParameter = note,
                            X = black.X,
                            Y = black.Y,
                            Width = black.Width,
                            Height = black.Height,
                        });
                        break;
                    default:
                        break;
                }
            }

            switch (this.Direction)
            {
                case NoteDirection.Left: case NoteDirection.Right: base.Height = NoteExtensions.NoteWhiteCount * this.NoteSize.WhiteSize; break;
                case NoteDirection.Top: case NoteDirection.Bottom: base.Width = NoteExtensions.NoteWhiteCount * this.NoteSize.WhiteSize; break;
                default: break;
            }
        }
    }

    partial class NotePanel
    {
        private NoteSize NoteSize = new NoteSize(20);

        public int ItemSize
        {
            get => this.NoteSize.ItemSize;
            set
            {
                if (this.NoteSize.ItemSize == value) return;
                this.NoteSize = new NoteSize(value);

                int count = 0;
                switch (this.Direction)
                {
                    case NoteDirection.Left:
                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            Note note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.Y = (NoteExtensions.NoteWhiteCount - count - 1) * this.NoteSize.WhiteSize;
                                    item.Height = this.NoteSize.WhiteSize;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.Y = (NoteExtensions.NoteWhiteCount - count + 1) * this.NoteSize.WhiteSize - this.NoteSize.BlackSizeHalf;
                                    item.Height = this.NoteSize.BlackSize;
                                    break;
                                default:
                                    break;
                            }
                        }

                        base.Height = NoteExtensions.NoteWhiteCount * this.NoteSize.WhiteSize;
                        break;
                    case NoteDirection.Top:
                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            Note note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.X = count * this.NoteSize.WhiteSize;
                                    item.Width = this.NoteSize.WhiteSize;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.X = (count + 1) * this.NoteSize.WhiteSize - this.NoteSize.BlackSizeHalf;
                                    item.Width = this.NoteSize.BlackSize;
                                    break;
                                default:
                                    break;
                            }
                        }

                        base.Width = NoteExtensions.NoteWhiteCount * this.NoteSize.WhiteSize;
                        break;
                    case NoteDirection.Right:
                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            Note note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.Y = count * this.NoteSize.WhiteSize;
                                    item.Height = this.NoteSize.WhiteSize;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.Y = (count + 1) * this.NoteSize.WhiteSize - this.NoteSize.BlackSizeHalf;
                                    item.Height = this.NoteSize.BlackSize;
                                    break;
                                default:
                                    break;
                            }
                        }

                        base.Height = NoteExtensions.NoteWhiteCount * this.NoteSize.WhiteSize;
                        break;
                    case NoteDirection.Bottom:
                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            Note note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.X = (NoteExtensions.NoteWhiteCount - count - 1) * this.NoteSize.WhiteSize;
                                    item.Width = this.NoteSize.WhiteSize;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.X = (NoteExtensions.NoteWhiteCount - count + 1) * this.NoteSize.WhiteSize - this.NoteSize.BlackSizeHalf;
                                    item.Width = this.NoteSize.BlackSize;
                                    break;
                                default:
                                    break;
                            }
                        }

                        base.Width = NoteExtensions.NoteWhiteCount * this.NoteSize.WhiteSize;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    partial class NotePanel
    {
        public void Resize(Size newSize, Size previousSize)
        {
            switch (this.Direction)
            {
                case NoteDirection.Left:
                    if (newSize.Width != previousSize.Width)
                    {
                        int count = 0;
                        double size = newSize.Width;
                        double size2 = size * this.BlackSize / this.WhiteSize;

                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            Note note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.X = 0;
                                    item.Width = size;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.X = 0;
                                    item.Width = size2;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                case NoteDirection.Top:
                    if (newSize.Height != previousSize.Height)
                    {
                        int count = 0;
                        double size = newSize.Height;
                        double size2 = size * this.BlackSize / this.WhiteSize;

                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            Note note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.Y = 0;
                                    item.Height = size;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.Y = 0;
                                    item.Height = size2;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                case NoteDirection.Right:
                    if (newSize.Width != previousSize.Width)
                    {
                        int count = 0;
                        double size = newSize.Width;
                        double size2 = size * this.BlackSize / this.WhiteSize;
                        double position = size - size2;

                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            Note note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.X = 0;
                                    item.Width = size;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.X = position;
                                    item.Width = size2;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                case NoteDirection.Bottom:
                    if (newSize.Height != previousSize.Height)
                    {
                        int count = 0;
                        double size = newSize.Height;
                        double size2 = size * this.BlackSize / this.WhiteSize;
                        double position = size - size2;

                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            Note note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.Y = 0;
                                    item.Height = size;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.Y = position;
                                    item.Height = size2;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public sealed partial class NoteTopPanel : NotePanel
    {
        public NoteTopPanel() : base(NoteDirection.Top)
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