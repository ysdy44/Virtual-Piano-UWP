using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Notes.Controls
{
    public partial class InstrumentSplitView : SplitView
    {
        public IInstrumentPanel InstrumentPanel { get; set; }

        public InstrumentSplitView()
        {
            this.InitializeComponent();
            this.ListView.ItemsSource = this.ItemsSource().ToArray();
            this.ListView.SelectionChanged += (s, e) =>
            {
                if (this.InstrumentPanel is null) return;

                foreach (InstrumentItemCategory item in e.RemovedItems)
                {
                    this.InstrumentPanel.Remove(item.Group);
                }

                foreach (InstrumentItemCategory item in e.AddedItems)
                {
                    this.InstrumentPanel.Add(item.Group);
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