using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Instruments
{
    public partial class DrumPanel : Canvas, IDrumPanel
    {
        //@Command
        public ICommand Command { get; set; }

        readonly MidiPercussionNoteCategoryDictionary MidiPercussionNoteCategoryDictionary = new MidiPercussionNoteCategoryDictionary();
        readonly MidiPercussionNoteDictionary MidiPercussionNoteDictionary = new MidiPercussionNoteDictionary();

        private int widthCount = 5;
        public int WidthCount
        {
            get => this.widthCount;
            set
            {
                if (this.widthCount == value) return;
                this.widthCount = value;

                for (int i = 0; i < base.Children.Count; i++)
                {
                    UIElement item = base.Children[i];
                    Canvas.SetLeft(item, (i % value) * 100);
                    Canvas.SetTop(item, (i / value) * 120);
                }

                int count = base.Children.Count;
                base.Width = value * 100;
                base.Height = (count / value) * 120;
            }
        }

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
                        Width = 100,
                        Height = 120,
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

        public void UpdateWidthCount(double width)
        {
            this.WidthCount = (int)(width / 100);
        }
    }
}