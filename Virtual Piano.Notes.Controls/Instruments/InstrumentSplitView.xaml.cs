using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Notes.Controls
{
    public partial class InstrumentSplitView : SplitView
    {
        public IInstrumentGroupPanel InstrumentGroupPanel { get; set; }

        //@Construct
        public InstrumentSplitView()
        {
            this.InitializeComponent();
            this.ListView.ItemsSource = this.ItemsSource().ToArray();
            this.ListView.SelectedIndex = 0; // Initialize
            this.ListView.SelectionChanged += (s, e) =>
            {
                if (this.InstrumentGroupPanel is null) return;

                foreach (ListViewItem item in e.RemovedItems)
                {
                    if (item.Content is InstrumentItemCategory category)
                    {
                        this.InstrumentGroupPanel.Remove(category.Group);
                    }
                }

                foreach (ListViewItem item in e.AddedItems)
                {
                    if (item.Content is InstrumentItemCategory category)
                    {
                        this.InstrumentGroupPanel.Add(category.Group);
                    }
                }
            };
        }

        private IEnumerable<ListViewItem> ItemsSource()
        {
            foreach (var item1 in MidiProgramFactory.Instance)
            {
                foreach (var item2 in item1.Value)
                {
                    MidiProgramGroup group = item2.Key;
                    ListViewItem item = new ListViewItem
                    {
                        Content = new InstrumentItemCategory
                        {
                            Group = item2.Key,
                        }
                    };
                    ToolTipService.SetToolTip(item, this.GetString(group));
                    yield return item;
                }
            }
        }

        public virtual string GetString(MidiProgramGroup group)
        {
            return group.ToString();
        }
    }
}