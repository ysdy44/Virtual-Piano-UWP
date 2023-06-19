using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Notes.Controls
{
    public partial class DrumGridView : WrapPanel, IDrumPanel
    {
        //@Command
        public ICommand Command { get; set; }

        public IDrumButton this[MidiPercussionNote note] =>
            base.Children[this.ItemSource.IndexOf(note)] as IDrumButton;

        public MidiPercussionNote this[int index]
        {
            get => this.ItemSource[index];
            set
            {
                if (this.ItemSource[index] == value) return;
                this.ItemSource[index] = value;

                if (base.Children[index] is IDrumButton item)
                {
                    item.CommandParameter = value;
                    item.TabIndex = (byte)value;
                    item.Foreground = base.Resources[$"{this.GetCategory(value)}"] as Brush;
                    item.Tag = this.GetString(value);
                }
            }
        }

        private readonly IList<MidiPercussionNote> ItemSource;

        //@Construct
        public DrumGridView(bool isCategory)
        {
            this.InitializeComponent();
            this.ItemSource = new List<MidiPercussionNote>(isCategory ? this.GetCategoryItemSource() : this.GetItemSource());
            
            foreach (MidiPercussionNote item in this.ItemSource)
            {
                base.Children.Add(new DrumButton
                {
                    CommandParameter = item,
                    TabIndex = (byte)item,
                    Foreground = base.Resources[$"{this.GetCategory(item)}"] as Brush,
                    Tag = $"{this.GetString(item)}"
                });
            }
        }

        private IEnumerable<MidiPercussionNote> GetItemSource()
        {
            foreach (MidiPercussionNote item in System.Enum.GetValues(typeof(MidiPercussionNote)).Cast<MidiPercussionNote>())
            {
                yield return item;
            }
        }

        private IEnumerable<MidiPercussionNote> GetCategoryItemSource()
        {
            foreach (var item2 in MidiPercussionNoteFactory.Instance)
            {
                foreach (var item3 in item2.Value)
                {
                    yield return item3;
                }
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
            int i = this.ItemSource.IndexOf(note);
            if (base.Children[i] is IDrumButton item)
            {
                item.Clear();
            }
        }

        public void Add(MidiPercussionNote note)
        {
            int i = this.ItemSource.IndexOf(note);
            if (base.Children[i] is IDrumButton item)
            {
                item.Add();
            }
        }
    }
}