using System.Collections.Generic;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Controls
{
    public partial class PadPanel : Canvas, IKitPanel
    {
        //@Command
        public ICommand Command { get; set; }

        private readonly IList<KitSet> Pads1x12 = new List<KitSet>
        {
            KitSet.Kick,
            KitSet.Snare,
            KitSet.Clap,
            KitSet.Close,
            KitSet.LowTom,
            KitSet.Open,
            KitSet.MidTom,
            KitSet.Crash,
            KitSet.FloorTom,
            KitSet.Ride,
            KitSet.Ring,
            KitSet.Sandham,
        };

        private readonly IList<KitSet> Pads2x6 = new List<KitSet>
        {
            KitSet.Open,
            KitSet.Crash,
            KitSet.LowTom,
            KitSet.MidTom,
            KitSet.FloorTom,
            KitSet.Ride,

            KitSet.Close,
            KitSet.Sandham,
            KitSet.Clap,
            KitSet.Ring,
            KitSet.Snare,
            KitSet.Kick,
        };

        private readonly IList<KitSet> Pads3x4 = new List<KitSet>
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

        readonly MidiPercussionNoteCategoryDictionary MidiPercussionNoteCategoryDictionary = new MidiPercussionNoteCategoryDictionary();
        readonly MidiPercussionNoteDictionary MidiPercussionNoteDictionary = new MidiPercussionNoteDictionary();

        //@Construct
        public PadPanel()
        {
            this.InitializeComponent();
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                PadLayout size = new PadLayout((int)e.NewSize.Width, (int)e.NewSize.Height);
                switch (size.Mode)
                {
                    case PadLayoutMode.L1x12:
                    case PadLayoutMode.L12x1:
                        for (int y = 0; y < size.Row; y++)
                        {
                            for (int x = 0; x < size.Column; x++)
                            {
                                int i = size.Column * y + x;
                                int index = i;
                                if (base.Children[index] is IKitButton item)
                                {
                                    item.X = x * size.Width;
                                    item.Y = y * size.Height;
                                    item.Width = size.Width;
                                    item.Height = size.Height;
                                }
                            }
                        }
                        break;
                    case PadLayoutMode.L2x6:
                    case PadLayoutMode.L6x2:
                        for (int y = 0; y < size.Row; y++)
                        {
                            for (int x = 0; x < size.Column; x++)
                            {
                                int i = size.Column * y + x;
                                KitSet note = this.Pads2x6[i];
                                int index = this.Pads1x12.IndexOf(note);
                                if (base.Children[index] is IKitButton item)
                                {
                                    item.X = x * size.Width;
                                    item.Y = y * size.Height;
                                    item.Width = size.Width;
                                    item.Height = size.Height;
                                }
                            }
                        }
                        break;
                    case PadLayoutMode.L3x4:
                    case PadLayoutMode.L4x3:
                        for (int y = 0; y < size.Row; y++)
                        {
                            for (int x = 0; x < size.Column; x++)
                            {
                                int i = size.Column * y + x;
                                KitSet note = this.Pads3x4[i];
                                int index = this.Pads1x12.IndexOf(note);
                                if (base.Children[index] is IKitButton item)
                                {
                                    item.X = x * size.Width;
                                    item.Y = y * size.Height;
                                    item.Width = size.Width;
                                    item.Height = size.Height;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            };

            foreach (KitSet set in this.Pads1x12)
            {
                var item = (MidiPercussionNote)set;
                base.Children.Add(new KitButton
                {
                    Content = $"{this.GetString(item)}",
                    CommandParameter = set,
                    TabIndex = (byte)item,
                    Foreground = this.MidiPercussionNoteCategoryDictionary[this.GetCategory(item)],
                    Tag = new PathIcon
                    {
                        Data = this.MidiPercussionNoteDictionary[item]
                    }
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