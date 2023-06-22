using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Notes.Controls
{
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
}