using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Windows.Input;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Controls
{
    public partial class DrumPanel : WrapPanel, IDrumPanel
    {
        //@Command
        public ICommand Command { get; set; }

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
                        CommandParameter = item3,
                        TabIndex = (byte)item3,
                        Foreground = base.Resources[$"{item2.Key}"] as Brush,
                        Content = $"{this.GetString(item3)}"
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