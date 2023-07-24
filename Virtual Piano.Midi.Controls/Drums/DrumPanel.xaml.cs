using System.Collections.Generic;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Controls
{
    public partial class DrumPanel : Canvas, IDrumPanel
    {
        //@Command
        public ICommand Command { get; set; }

        public IDrumButton this[MidiPercussionNote note] =>
            this.ItemsSource.Contains(note) ? 
            base.Children[this.ItemsSource.IndexOf(note)] as IDrumButton : null;

        public MidiPercussionNote this[int index]
        {
            get => this.ItemsSource[index];
            set
            {
                if (this.ItemsSource[index] == value) return;
                this.ItemsSource[index] = value;

                if (base.Children[index] is IDrumButton item)
                {
                    item.CommandParameter = value;
                    item.TabIndex = (byte)value;
                    item.Foreground = base.Resources[$"{this.GetCategory(value)}"] as Brush;
                    item.Content = this.GetString(value);
                }
            }
        }

        private readonly IList<MidiPercussionNote> ItemsSource = new List<MidiPercussionNote>
        {
             MidiPercussionNote.OpenHiHat, MidiPercussionNote.RideCymbal1, MidiPercussionNote.Shaker, MidiPercussionNote.CrashCymbal1,
             MidiPercussionNote.ClosedHiHat, MidiPercussionNote.LowTom, MidiPercussionNote.LowMidTom, MidiPercussionNote.HighTom,
             MidiPercussionNote.Cowbell, MidiPercussionNote.HandClap, MidiPercussionNote.AcousticSnare, MidiPercussionNote.AcousticBassDrum,
        };

        //@Construct
        public DrumPanel()
        {
            this.InitializeComponent();
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                DrumLayout size = new DrumLayout((int)e.NewSize.Width, (int)e.NewSize.Height);
                for (int y = 0; y < size.Row; y++)
                {
                    for (int x = 0; x < size.Column; x++)
                    {
                        int i = size.Column * y + x;
                        if (base.Children[i] is IDrumButton item)
                        {
                            item.X = x * size.Width;
                            item.Y = y * size.Height;
                            item.Width = size.Width;
                            item.Height = size.Height;
                        }
                    }
                }
            };

            foreach (MidiPercussionNote item in this.ItemsSource)
            {
                base.Children.Add(new DrumButton
                {
                    Content = $"{this.GetString(item)}",
                    CommandParameter = item,
                    TabIndex = (byte)item,
                    Foreground = base.Resources[$"{this.GetCategory(item)}"] as Brush
                });
            }
        }

        private MidiPercussionNoteCategory GetCategory(MidiPercussionNote item)
        {
            foreach (var item2 in MidiPercussionNoteFactory.Instance)
            {
                foreach (var item3 in item2.Value)
                {
                    if (item3 == item)
                    {
                        return item2.Key;
                    }
                }
            }

            return default;
        }

        public void OnClick(MidiPercussionNote note) => this.Command?.Execute(note); // Command

        public virtual string GetString(MidiPercussionNote note)
        {
            return note.ToString();
        }

        public void Clear(int index)
        {
            if (base.Children[index] is IDrumButton item)
            {
                item.Clear();
            }
        }

        public void Add(int index)
        {
            if (base.Children[index] is IDrumButton item)
            {
                item.Add();
            }
        }

        public void Clear(MidiPercussionNote note)
        {
            int i = this.ItemsSource.IndexOf(note);
            if (base.Children[i] is IDrumButton item)
            {
                item.Clear();
            }
        }

        public void Add(MidiPercussionNote note)
        {
            int i = this.ItemsSource.IndexOf(note);
            if (base.Children[i] is IDrumButton item)
            {
                item.Add();
            }
        }
    }
}