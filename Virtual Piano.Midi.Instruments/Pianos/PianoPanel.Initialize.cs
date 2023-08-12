using System.Linq;
using Virtual_Piano.Midi.Core;
using Windows.Foundation;

namespace Virtual_Piano.Midi.Instruments
{
    partial class PianoPanel
    {
        public Rect GetRect(int count, ToneType type)
        {
            switch (type)
            {
                case ToneType.White:
                    switch (this.Direction)
                    {
                        case PianoDirection.Left:
                            return new Rect
                            {
                                X = 0,
                                Y = this.PianoSize.ReverseToWhite(count),
                                Width = double.NaN,
                                Height = this.PianoSize.WhiteSize
                            };
                        case PianoDirection.Top:
                            return new Rect
                            {
                                X = this.PianoSize.ToWhite(count),
                                Y = 0,
                                Width = this.PianoSize.WhiteSize,
                                Height = double.NaN,
                            };
                        case PianoDirection.Right:
                            return new Rect
                            {
                                X = 0,
                                Y = this.PianoSize.ToWhite(count),
                                Width = double.NaN,
                                Height = this.PianoSize.WhiteSize
                            };
                        case PianoDirection.Bottom:
                            return new Rect
                            {
                                X = this.PianoSize.ReverseToWhite(count),
                                Y = 0,
                                Width = this.PianoSize.WhiteSize,
                                Height = double.NaN,
                            };
                        default: return Rect.Empty;
                    }
                case ToneType.Black:
                    switch (this.Direction)
                    {
                        case PianoDirection.Left:
                            return new Rect
                            {
                                X = 0,
                                Y = this.PianoSize.ReverseToBlack(count),
                                Width = double.NaN,
                                Height = this.PianoSize.BlackSize
                            };
                        case PianoDirection.Top:
                            return new Rect
                            {
                                X = this.PianoSize.ToBlack(count),
                                Y = 0,
                                Width = this.PianoSize.BlackSize,
                                Height = double.NaN,
                            };
                        case PianoDirection.Right:
                            return new Rect
                            {
                                X = 0,
                                Y = this.PianoSize.ToWhite(count),
                                Width = double.NaN,
                                Height = this.PianoSize.BlackSize
                            };
                        case PianoDirection.Bottom:
                            return new Rect
                            {
                                X = this.PianoSize.ReverseToBlack(count),
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
            foreach (MidiNote note in System.Enum.GetValues(typeof(MidiNote)).Cast<MidiNote>())
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
                case PianoDirection.Left: case PianoDirection.Right: base.Height = this.PianoSize.Length; break;
                case PianoDirection.Top: case PianoDirection.Bottom: base.Width = this.PianoSize.Length; break;
                default: break;
            }
        }
    }
}