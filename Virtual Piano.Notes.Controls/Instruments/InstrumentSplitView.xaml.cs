using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

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

                foreach (InstrumentItemCategory item in e.RemovedItems)
                {
                    this.InstrumentGroupPanel.Remove(item.Group);
                }

                foreach (InstrumentItemCategory item in e.AddedItems)
                {
                    this.InstrumentGroupPanel.Add(item.Group);
                }
            };
        }

        private IEnumerable<InstrumentItemCategory> ItemsSource()
        {
            foreach (var item1 in MidiProgramFactory.Instance)
            {
                foreach (var item2 in item1.Value)
                {
                    yield return new InstrumentItemCategory
                    {
                        Group = item2.Key,
                        Text = this.GetString(item2.Key)
                    };
                }
            }
        }

        public virtual string GetString(MidiProgramGroup group)
        {
            return group.ToString();
        }
    }
}