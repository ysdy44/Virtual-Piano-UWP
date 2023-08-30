using System.Linq;
using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

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

        public bool this[MidiNote note]
        {
            get
            {
                if (this.Children[(int)note] is IPianoButton item)
                {
                    return item.IsEnabled;
                }
                return false;
            }
            set
            {
                if (this.Children[(int)note] is IPianoButton item)
                {
                    item.IsEnabled = value;
                }
            }
        }

        readonly MidiOctaveDictionary OctaveDictionary = new MidiOctaveDictionary();
        readonly PianoDirection Direction;

        public PianoPanel(PianoDirection direction)
        {
            this.Direction = direction;
            this.Initialize();
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                this.Resize(e.NewSize, e.PreviousSize);
            };
        }

        public void OnClick(MidiNote note) => this.Command?.Execute(note); // Command

        public int GetIndex(MidiNote note)
        {
            switch (this.Direction)
            {
                case PianoDirection.Left: case PianoDirection.Bottom: return NoteExtensions.NoteCount - (int)note - 1;
                case PianoDirection.Top: case PianoDirection.Right: return (int)note;
                default: return 0;
            }
        }

        public void Clear(MidiNote note)
        {
            if (this.Children[(int)note] is IPianoButton item)
            {
                item.Clear();
            }
        }
        public void Add(MidiNote note)
        {
            if (this.Children[(int)note] is IPianoButton item)
            {
                item.Add();
            }
        }
    }
}