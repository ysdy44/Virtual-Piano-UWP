using System.Linq;
using Virtual_Piano.Midi.Core;

namespace Virtual_Piano.Midi.Instruments
{
    partial class PianoPanel
    {
        private PianoSize PianoSize = new PianoSize(21);

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
                        foreach (IPianoButton item in base.Children.Cast<IPianoButton>())
                        {
                            MidiNote note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.Y = this.PianoSize.ReverseToWhite(count);
                                    item.Height = this.PianoSize.WhiteSize;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.Y = this.PianoSize.ReverseToBlack(count);
                                    item.Height = this.PianoSize.BlackSize;
                                    break;
                                default:
                                    break;
                            }
                        }

                        base.Height = this.PianoSize.Length;
                        break;
                    case PianoDirection.Top:
                        foreach (IPianoButton item in base.Children.Cast<IPianoButton>())
                        {
                            MidiNote note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.X = this.PianoSize.ToWhite(count);
                                    item.Width = this.PianoSize.WhiteSize;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.X = this.PianoSize.ToBlack(count);
                                    item.Width = this.PianoSize.BlackSize;
                                    break;
                                default:
                                    break;
                            }
                        }

                        base.Width = this.PianoSize.Length;
                        break;
                    case PianoDirection.Right:
                        foreach (IPianoButton item in base.Children.Cast<IPianoButton>())
                        {
                            MidiNote note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.Y = this.PianoSize.ToWhite(count);
                                    item.Height = this.PianoSize.WhiteSize;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.Y = this.PianoSize.ToBlack(count);
                                    item.Height = this.PianoSize.BlackSize;
                                    break;
                                default:
                                    break;
                            }
                        }

                        base.Height = this.PianoSize.Length;
                        break;
                    case PianoDirection.Bottom:
                        foreach (IPianoButton item in base.Children.Cast<IPianoButton>())
                        {
                            MidiNote note = item.CommandParameter;
                            switch (item.Type)
                            {
                                case ToneType.White:
                                    item.X = this.PianoSize.ReverseToWhite(count);
                                    item.Width = this.PianoSize.WhiteSize;
                                    count++;
                                    break;
                                case ToneType.Black:
                                    item.X = this.PianoSize.ReverseToBlack(count);
                                    item.Width = this.PianoSize.BlackSize;
                                    break;
                                default:
                                    break;
                            }
                        }

                        base.Width = this.PianoSize.Length;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}