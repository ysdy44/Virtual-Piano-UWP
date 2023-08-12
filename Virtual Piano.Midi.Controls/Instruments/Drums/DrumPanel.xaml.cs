using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Controls
{
    public partial class DrumPanel : WrapPanel, IDrumPanel
    {
        //@Command
        public ICommand Command { get; set; }

        readonly MidiPercussionNoteCategoryDictionary MidiPercussionNoteCategoryDictionary = new MidiPercussionNoteCategoryDictionary();
        readonly MidiPercussionNoteDictionary MidiPercussionNoteDictionary = new MidiPercussionNoteDictionary();

        //@Construct
        public DrumPanel()
        {
            this.InitializeComponent();
            foreach (var item2 in MidiPercussionNoteFactory.Instance)
            {
                foreach (var item3 in item2.Value)
                {
                    base.Children.Add(new DrumButton
                    {
                        Content = $"{this.GetString(item3)}",
                        CommandParameter = item3,
                        TabIndex = (byte)item3,
                        Foreground = this.MidiPercussionNoteCategoryDictionary[item2.Key],
                        Tag = new PathIcon
                        {
                            Data = this.MidiPercussionNoteDictionary[item3]
                        }
                    });
                }
            }
        }

        public void OnClick(MidiPercussionNote note) => this.Command?.Execute(note); // Command
        public virtual string GetString(MidiPercussionNote note)
        {
            return note.ToString();
        }
    }
}