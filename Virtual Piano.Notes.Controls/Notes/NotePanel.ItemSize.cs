using System.Linq;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Notes.Controls
{
    partial class PianoPanel
    {
        private PianoSize PianoSize = new PianoSize(20);

        public int ItemSize
        {
            get => this.PianoSize.ItemSize;
            set
            {
                if (this.PianoSize.ItemSize == value) return;
                this.PianoSize = new PianoSize(value);

                int count = 0;
                switch (this.Direction)
                {
                    case PianoDirection.Left:
                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            Note note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.Y = (NoteExtensions.NoteWhiteCount - count - 1) * this.PianoSize.WhiteSize;
                                    item.Height = this.PianoSize.WhiteSize;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.Y = (NoteExtensions.NoteWhiteCount - count + 1) * this.PianoSize.WhiteSize - this.PianoSize.BlackSizeHalf;
                                    item.Height = this.PianoSize.BlackSize;
                                    break;
                                default:
                                    break;
                            }
                        }

                        base.Height = NoteExtensions.NoteWhiteCount * this.PianoSize.WhiteSize;
                        break;
                    case PianoDirection.Top:
                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            Note note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.X = count * this.PianoSize.WhiteSize;
                                    item.Width = this.PianoSize.WhiteSize;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.X = (count + 1) * this.PianoSize.WhiteSize - this.PianoSize.BlackSizeHalf;
                                    item.Width = this.PianoSize.BlackSize;
                                    break;
                                default:
                                    break;
                            }
                        }

                        base.Width = NoteExtensions.NoteWhiteCount * this.PianoSize.WhiteSize;
                        break;
                    case PianoDirection.Right:
                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            Note note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.Y = count * this.PianoSize.WhiteSize;
                                    item.Height = this.PianoSize.WhiteSize;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.Y = (count + 1) * this.PianoSize.WhiteSize - this.PianoSize.BlackSizeHalf;
                                    item.Height = this.PianoSize.BlackSize;
                                    break;
                                default:
                                    break;
                            }
                        }

                        base.Height = NoteExtensions.NoteWhiteCount * this.PianoSize.WhiteSize;
                        break;
                    case PianoDirection.Bottom:
                        foreach (INoteButton item in base.Children.Cast<INoteButton>())
                        {
                            Note note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.X = (NoteExtensions.NoteWhiteCount - count - 1) * this.PianoSize.WhiteSize;
                                    item.Width = this.PianoSize.WhiteSize;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.X = (NoteExtensions.NoteWhiteCount - count + 1) * this.PianoSize.WhiteSize - this.PianoSize.BlackSizeHalf;
                                    item.Width = this.PianoSize.BlackSize;
                                    break;
                                default:
                                    break;
                            }
                        }

                        base.Width = NoteExtensions.NoteWhiteCount * this.PianoSize.WhiteSize;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}