using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Notes.Controls
{
    public class InstrumentGroupPanel : InstrumentPanel, IInstrumentGroupPanel
    {
        //@Command
        public ICommand Command { get; set; }

        private readonly IDictionary<MidiProgramGroup, IEnumerable<InstrumentButton>> Dictionary = new Dictionary<MidiProgramGroup, IEnumerable<InstrumentButton>>();

        //@Construct
        public InstrumentGroupPanel()
        {
            this.InitializeComponent();
            foreach (var item1 in MidiProgramFactory.Instance)
            {
                foreach (var item2 in item1.Value)
                {
                    this.Dictionary.Add(item2.Key, this.ItemsSource(item2.Key, item2.Value).ToArray());
                }
            }
            // Initialize
            foreach (var item2 in this.Dictionary)
            {
                foreach (var item3 in item2.Value)
                {
                    this.Children.Add(item3);
                }
                return;
            }
        }

        public IEnumerable<InstrumentButton> ItemsSource(MidiProgramGroup group, IEnumerable<MidiProgram> programs)
        {
            foreach (MidiProgram item in programs)
            {
                yield return new InstrumentButton
                {
                    CommandParameter = item,
                    TabIndex = (byte)item,
                    Foreground = base.Resources[$"{group}"] as Brush,
                    Content = $"{this.GetString(item)}"
                };
            }
        }

        public void OnClick(MidiProgram program) => this.Command?.Execute(program); // Command
        public virtual string GetString(MidiProgram program)
        {
            return program.ToString();
        }

        public void Remove(MidiProgramGroup group)
        {
            foreach (var item2 in this.Dictionary[group])
            {
                base.Children.Remove(item2);
            }
        }

        public void Add(MidiProgramGroup group)
        {
            foreach (var item2 in this.Dictionary[group])
            {
                base.Children.Add(item2);
            }
        }
    }
}