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
}