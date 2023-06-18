using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Notes.Controls
{
    public partial class DrumGroupGridView : UserControl
    {
        //@Command
        public ICommand Command { get; set; }

        public DrumItemCategory[] ItemsSource { get; }
        public IList<MidiPercussionNoteCategory> Collection { get; } = new List<MidiPercussionNoteCategory>();

        public IDictionary<MidiPercussionNoteCategory, DrumItem[]> Dictionary { get; } = new Dictionary<MidiPercussionNoteCategory, DrumItem[]>();
        public IList<DrumItem> ObservableCollection { get; } = new ObservableCollection<DrumItem>();

        public DrumGroupGridView()
        {
            this.InitializeComponent();
            this.ItemsSource = MidiPercussionNoteFactory.Instance.Select(c => new DrumItemCategory
            {
                Category = c.Key,
                Text = this.GetString(c.Key),
            }).ToArray();

            foreach (var item in MidiPercussionNoteFactory.Instance)
            {
                this.Dictionary[item.Key] = item.Value.Select(c => new DrumItem
                {
                    Note = c,
                    Text = this.GetString(c),
                }).ToArray();
            }

            this.ListView.SelectionChanged += (s, e) =>
            {
                foreach (DrumItemCategory item in e.RemovedItems)
                {
                    this.Collection.Remove(item.Category);
                    foreach (var item2 in this.Dictionary[item.Category])
                    {
                        this.ObservableCollection.Remove(item2);
                    }
                }
                foreach (DrumItemCategory item in e.AddedItems)
                {
                    this.Collection.Add(item.Category);
                    foreach (var item2 in this.Dictionary[item.Category])
                    {
                        this.ObservableCollection.Add(item2);
                    }
                }
            };
            this.GridView.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is DrumItem item)
                {
                    this.Command?.Execute(item.Note); // Command
                }
            };
        }

        public virtual string GetString(MidiPercussionNote note)
        {
            return note.ToString();
        }
        public virtual string GetString(MidiPercussionNoteCategory category)
        {
            return category.ToString();
        }
    }
}