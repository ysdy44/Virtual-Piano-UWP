using System.Collections.Generic;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Controls
{
    public partial class DrumPanel : Canvas, IKitPanel
    {
        //@Command
        public ICommand Command { get; set; }


        private readonly IList<KitSet> ItemsSource = new List<KitSet>
        {
            KitSet.Open,
            KitSet.Ride,
            KitSet.Sandham,
            KitSet.Crash,

            KitSet.Close,
            KitSet.LowTom,
            KitSet.MidTom,
            KitSet.FloorTom,

            KitSet.Ring,
            KitSet.Clap,
            KitSet.Snare,
            KitSet.Kick,
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
                        if (base.Children[i] is IKitButton item)
                        {
                            item.X = x * size.Width;
                            item.Y = y * size.Height;
                            item.Width = size.Width;
                            item.Height = size.Height;
                        }
                    }
                }
            };

            foreach (KitSet set in this.ItemsSource)
            {
                MidiPercussionNote item = (MidiPercussionNote)set;
                base.Children.Add(new KitButton
                {
                    Content = $"{this.GetString(item)}",
                    CommandParameter = set,
                    TabIndex = (byte)item,
                    Foreground = base.Resources[$"{this.GetCategory(item)}"] as Brush,
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

        public void OnClick(KitSet set) => this.Command?.Execute((MidiPercussionNote)set); // Command
        public virtual string GetString(MidiPercussionNote note)
        {
            return note.ToString();
        }
    }
}