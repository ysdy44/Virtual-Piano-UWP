using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Controls
{
    public partial class DrumSplitView : SplitView
    {
        public IDrumPanel DrumPanel { get; set; }

        private readonly IDictionary<MidiPercussionNoteCategory, bool> Dictionary = MidiPercussionNoteFactory.Instance.ToDictionary(c => c.Key, d => true);

        public DrumSplitView()
        {
            this.InitializeComponent();
            this.ListView.ItemsSource = MidiPercussionNoteFactory.Instance.Select(c => new DrumItemCategory
            {
                Category = c.Key,
                Text = this.GetString(c.Key),
            }).ToArray();
            this.ListView.SelectAll();
            this.ListView.SelectionChanged += (s, e) =>
            {
                foreach (DrumItemCategory item in e.RemovedItems)
                {
                    this.Dictionary[item.Category] = false;
                    if (this.DrumPanel is null) continue;
                    foreach (MidiPercussionNote item2 in MidiPercussionNoteFactory.Instance[item.Category])
                    {
                        this.DrumPanel[item2].Visibility = Visibility.Collapsed;
                    }
                }

                foreach (DrumItemCategory item in e.AddedItems)
                {
                    this.Dictionary[item.Category] = true;
                    if (this.DrumPanel is null) continue;
                    foreach (MidiPercussionNote item2 in MidiPercussionNoteFactory.Instance[item.Category])
                    {
                        this.DrumPanel[item2].Visibility = Visibility.Visible;
                    }
                }
            };
        }

        public virtual string GetString(MidiPercussionNoteCategory category)
        {
            return category.ToString();
        }
    }
}