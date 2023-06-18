using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Notes.Controls
{
    public partial class DrumGridView : UserControl
    {
        //@Command
        public ICommand Command { get; set; }

        public DrumGridView()
        {
            this.InitializeComponent();
            this.GridView.ItemsSource = this.GetItemSource().ToArray();
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

        private IEnumerable<DrumItem> GetItemSource()
        {
            foreach (MidiPercussionNote item in System.Enum.GetValues(typeof(MidiPercussionNote)))
            {
                yield return new DrumItem
                {
                    Note = item,
                    Text = this.GetString(item)
                };
            }
        }
    }
}