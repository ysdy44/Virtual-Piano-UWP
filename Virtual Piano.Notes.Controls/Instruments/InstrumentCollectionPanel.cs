using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Notes.Controls
{
    public partial class InstrumentCollectionPanel : InstrumentPanel, IInstrumentPanel
    {
        //@Command
        public ICommand Command { get; set; }

        private readonly IDictionary<MidiProgram, InstrumentButton> Dictionary = new Dictionary<MidiProgram, InstrumentButton>();


        private ObservableCollection<MidiProgram> observableCollection;
        public ObservableCollection<MidiProgram> ObservableCollection
        {
            get => this.observableCollection;
            set
            {
                if (this.observableCollection != null)
                {
                    this.observableCollection.CollectionChanged -= this.CollectionChanged;
                    foreach (MidiProgram item in this.observableCollection)
                    {
                        this.Remove(item);
                    }
                }
                this.observableCollection = value;
                if (this.observableCollection != null)
                {
                    this.observableCollection.CollectionChanged += this.CollectionChanged;
                    foreach (MidiProgram item in this.observableCollection)
                    {
                        this.Add(item);
                    }
                }
            }
        }

        //@Construct
        public InstrumentCollectionPanel()
        {
            this.InitializeComponent();
            foreach (var item1 in MidiProgramFactory.Instance)
            {
                foreach (var item2 in item1.Value)
                {
                    MidiProgramGroup group = item2.Key;
                    foreach (MidiProgram item in item2.Value)
                    {
                        this.Dictionary.Add(item, new InstrumentButton
                        {
                            CommandParameter = item,
                            TabIndex = (byte)item,
                            Foreground = base.Resources[$"{group}"] as Brush,
                            Content = $"{this.GetString(item)}"
                        });
                    }
                }
            }
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (MidiProgram item in e.NewItems)
                    {
                        this.Add(item);
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (MidiProgram item in e.OldItems)
                    {
                        this.Remove(item);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    base.Children.Clear();
                    break;

                default:
                    break;
            };
        }

        public void OnClick(MidiProgram program) => this.Command?.Execute(program); // Command
        public virtual string GetString(MidiProgram program)
        {
            return program.ToString();
        }

        public void Remove(MidiProgram program)
        {
            base.Children.Remove(this.Dictionary[program]);
        }

        public void Add(MidiProgram program)
        {
            base.Children.Add(this.Dictionary[program]);
        }
    }
}