using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Virtual_Piano.Midi;
using Windows.Devices.Midi;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano.TestApp
{
    public sealed partial class DrumMachinePage : Page, ICommand
    {
        readonly KitSet[] Drum = new KitSet[] { KitSet.Open, KitSet.Close, KitSet.Clap, KitSet.Kick, };

        MidiSynthesizer MidiSynthesizer;

        ~DrumMachinePage() => this.MidiSynthesizer?.Dispose();
        public DrumMachinePage()
        {
            this.InitializeComponent();
            Tempo tempo = this.ItemsSource.Tempo;
            this.Run.Text = $"{tempo.Bpm}";
            this.Slider.Value = tempo.Bpm;
            this.Slider.ValueChanged += (s, e) =>
            {
                Tempo t = new Tempo((int)e.NewValue);
                this.Run.Text = $"{t.Bpm}";
                this.ItemsSource.Tempo = t;
            };

            base.Loaded += (s, e) => this.Initialize(0);
            for (int i = 0; i < this.Music.Length; i++)
            {
                this.CommandBar.SecondaryCommands.Add(new AppBarButton
                {
                    CommandParameter = i,
                    Command = this,
                    Label = $"Reset {i}",
                    Icon = new SymbolIcon
                    {
                        Symbol = Symbol.Refresh
                    }
                });
            }

            this.ClearButton.Click += (s, e) => this.ItemsSource.Deselect();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.MidiSynthesizer?.Dispose();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.MidiSynthesizer?.Dispose();
            this.MidiSynthesizer = await MidiSynthesizer.CreateAsync();
        }

        //@Delegate
        public event EventHandler CanExecuteChanged;

        //@Command
        public ICommand Command => this;
        public bool CanExecute(object parameter) => true;
        public async void Execute(object parameter)
        {
            if (parameter is int item)
            {
                this.Initialize(item);
            }
            else if (parameter is MidiPercussionNote item2)
            {
                this.MidiSynthesizer.NoteOn(item2);
                await Task.Delay(2000);
                this.MidiSynthesizer.NoteOff(item2);
            }
        }

        private void Initialize(int index)
        {
            this.ItemsSource.Initialize(this.Music[index]);
            this.ProgressBar.Value = this.ItemsSource.Index;
            this.ProgressBar.Maximum = this.ItemsSource.Length;
        }

        public readonly byte[][] Music = new byte[][]
        {
            new byte[]
            {
                0,1,0,1,
                0,1,0,0,
                1,0,0,0,
                0,0,0,0,

                0,1,1,0,
                0,0,0,0,
                0,1,0,0,
                0,0,0,1,

                0,1,0,0,
                0,0,0,0,
                0,1,0,1,
                0,0,0,0,

                0,1,1,0,
                0,0,0,0,
                0,1,0,0,
                0,0,0,1,
            },
            new byte[]
            {
                0,0,0,0,
                1,1,1,1,
                0,1,0,1,
                1,0,0,0,

                0,0,0,0,
                1,0,0,0,
                0,0,0,0,
                0,0,0,0,

                1,0,0,0,
                0,1,1,1,
                0,0,0,0,
                0,0,1,0,

                0,0,0,0,
                0,0,0,0,
                0,0,0,0,
                0,1,0,1,
            },
            new byte[]
            {
                0,0,0,1,
                0,0,0,0,
                0,0,0,0,
                0,0,0,0,

                0,0,1,1,
                0,0,0,0,
                0,0,0,0,
                0,0,0,0,

                0,0,0,1,
                0,0,0,0,
                0,0,0,0,
                0,0,0,0,

                0,0,1,1,
                0,0,0,0,
                0,0,0,0,
                1,0,0,0,
            },
            new byte[]
            {
                0,1,0,1,
                0,1,0,0,
                0,1,0,0,
                0,1,0,0,

                0,1,1,1,
                0,1,0,0,
                0,1,0,0,
                0,1,0,0,

                0,1,0,1,
                0,1,0,0,
                0,1,0,0,
                0,1,0,0,

                0,1,1,1,
                0,1,0,0,
                0,1,0,0,
                0,1,0,1,



                0,1,0,1,
                0,1,0,0,
                1,0,0,0,
                0,1,0,0,

                0,1,1,0,
                0,1,0,0,
                1,0,0,0,
                0,1,0,1,

                0,1,0,0,
                0,1,0,1,
                1,0,0,1,
                0,1,0,0,

                0,1,1,0,
                0,1,0,1,
                1,0,0,0,
                0,1,1,0,
            },
        };
    }
}