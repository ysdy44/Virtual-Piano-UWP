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
}