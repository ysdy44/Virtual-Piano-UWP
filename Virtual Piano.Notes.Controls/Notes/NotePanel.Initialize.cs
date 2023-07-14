using System.Linq;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Notes.Controls
{
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
                                Y = (NoteExtensions.NoteWhiteCount - count - 1) * this.PianoSize.WhiteSize,
                                Width = double.NaN,
                                Height = this.PianoSize.WhiteSize
                            };
                        case NoteDirection.Top:
                            return new Rect
                            {
                                X = count * this.PianoSize.WhiteSize,
                                Y = 0,
                                Width = this.PianoSize.WhiteSize,
                                Height = double.NaN,
                            };
                        case NoteDirection.Right:
                            return new Rect
                            {
                                X = 0,
                                Y = count * this.PianoSize.WhiteSize,
                                Width = double.NaN,
                                Height = this.PianoSize.WhiteSize
                            };
                        case NoteDirection.Bottom:
                            return new Rect
                            {
                                X = (NoteExtensions.NoteWhiteCount - count - 1) * this.PianoSize.WhiteSize,
                                Y = 0,
                                Width = this.PianoSize.WhiteSize,
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
                                Y = (NoteExtensions.NoteWhiteCount - count + 1) * this.PianoSize.WhiteSize - this.PianoSize.BlackSizeHalf,
                                Width = double.NaN,
                                Height = this.PianoSize.BlackSize
                            };
                        case NoteDirection.Top:
                            return new Rect
                            {
                                X = (count + 1) * this.PianoSize.WhiteSize - this.PianoSize.BlackSizeHalf,
                                Y = 0,
                                Width = this.PianoSize.BlackSize,
                                Height = double.NaN,
                            };
                        case NoteDirection.Right:
                            return new Rect
                            {
                                X = 0,
                                Y = count * this.PianoSize.WhiteSize,
                                Width = double.NaN,
                                Height = this.PianoSize.BlackSize
                            };
                        case NoteDirection.Bottom:
                            return new Rect
                            {
                                X = (NoteExtensions.NoteWhiteCount - count + 1) * this.PianoSize.WhiteSize - this.PianoSize.BlackSizeHalf,
                                Y = 0,
                                Width = this.PianoSize.BlackSize,
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
                        base.Children.Add(new PianoButton
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
                        base.Children.Add(new PianoButton
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
                case NoteDirection.Left: case NoteDirection.Right: base.Height = NoteExtensions.NoteWhiteCount * this.PianoSize.WhiteSize; break;
                case NoteDirection.Top: case NoteDirection.Bottom: base.Width = NoteExtensions.NoteWhiteCount * this.PianoSize.WhiteSize; break;
                default: break;
            }
        }
    }
}