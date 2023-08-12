using System.Linq;
using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Instruments
{
    public abstract partial class PianoPanel : Canvas, IPianoPanel
    {
        //@Command
        public ICommand Command { get; set; }

        public double WhiteSize
        {
            get
            {
                switch (this.Direction)
                {
                    case PianoDirection.Left: case PianoDirection.Right: return System.Math.Max(base.MinWidth, base.MaxWidth);
                    case PianoDirection.Top: case PianoDirection.Bottom: return System.Math.Max(base.MinHeight, base.MaxHeight);
                    default: return 0;
                }
            }
            set
            {
                switch (this.Direction)
                {
                    case PianoDirection.Left: case PianoDirection.Right: base.MaxWidth = value; break;
                    case PianoDirection.Top: case PianoDirection.Bottom: base.MaxHeight = value; break;
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
                    case PianoDirection.Left: case PianoDirection.Right: return base.MinWidth;
                    case PianoDirection.Top: case PianoDirection.Bottom: return base.MinHeight;
                    default: return 0;
                }
            }
            set
            {
                switch (this.Direction)
                {
                    case PianoDirection.Left: case PianoDirection.Right: base.MinWidth = value; break;
                    case PianoDirection.Top: case PianoDirection.Bottom: base.MinHeight = value; break;
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

                foreach (IPianoButton item in base.Children.Cast<IPianoButton>())
                {
                    item.Content = item.CommandParameter.ToLabel(value);
                }
            }
        }

        readonly PianoDirection Direction;
        public PianoPanel(PianoDirection direction) => this.Direction = direction;

        public void OnClick(MidiNote note) => this.Command?.Execute(note); // Command

        public abstract Brush GetBrush(MidiOctave octave);
        public abstract Style GetStyle(ToneType type);
        public int GetIndex(MidiNote note)
        {
            switch (this.Direction)
            {
                case PianoDirection.Left: case PianoDirection.Bottom: return NoteExtensions.NoteCount - (int)note - 1;
                case PianoDirection.Top: case PianoDirection.Right: return (int)note;
                default: return 0;
            }
        }
    }
}