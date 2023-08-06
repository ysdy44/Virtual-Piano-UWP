using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Virtual_Piano.Midi;
using Windows.Devices.Midi;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano.TestApp
{
    public sealed class DrumMachine : List<ListViewItem>
    {
        public int Index { get; private set; } = 0;
        public int Length { get; private set; } = 32;

        public ListViewItem this[int n, int i]
        {
            get => this[n + i * 4];
            set => this[n + i * 4] = value;
        }

        public DrumMachine()
        {
            for (int i = 0; i < 32; i++)
            {
                for (int n = 0; n < 4; n++)
                {
                    base.Add(new ListViewItem
                    {
                        Content = i
                    });
                }
            }
        }

        public void Initialize(byte[] bytes)
        {
            this.Index = 0;
            this.Length = bytes.Length / 4;

            for (int i = 0; i < this.Length; i++)
            {
                for (int n = 0; n < 4; n++)
                {
                    var index = n + i * 4;
                    if (this[index] is ListViewItem item)
                    {
                        item.IsEnabled = true;
                        item.IsSelected = bytes[index] is 1;
                    }
                }
            }

            for (int i = this.Length; i < 32; i++)
            {
                for (int n = 0; n < 4; n++)
                {
                    if (this[n, i] is ListViewItem item)
                    {
                        item.IsEnabled = false;
                        item.IsSelected = false;
                    }
                }
            }
        }

        public void Deselect()
        {
            foreach (ListViewItem item in this)
            {
                item.IsSelected = false;
            }
        }

        public void Next()
        {
            this.Index++;
            if (this.Index < this.Length) return;
            this.Index = 0;
        }
    }

    public sealed partial class DrumMachinePage : Page, ICommand
    {
        readonly KitSet[] Drum = new KitSet[] { KitSet.Open, KitSet.Close, KitSet.Clap, KitSet.Kick, };
        readonly ListViewItem[] HeadItemsSource;
        readonly DrumMachine ItemsSource = new DrumMachine();
        readonly DispatcherTimer Timer = new DispatcherTimer();

        MidiSynthesizer Synthesizer;
        ~DrumMachinePage() => this.Synthesizer?.Dispose();
        public DrumMachinePage()
        {
            this.InitializeComponent();
            this.HeadItemsSource = Drum.Select(c => new ListViewItem
            {
                Content = c
            }).ToArray();

            Tempo tempo = new Tempo(100);
            this.Run.Text = $"{tempo.Bpm}";
            this.Timer.Interval = (tempo.Delay);
            this.Slider.Value = tempo.Bpm;
            this.Slider.ValueChanged += (s, e) =>
            {
                Tempo t = new Tempo((int)e.NewValue);
                this.Run.Text = $"{t.Bpm}";
                this.Timer.Interval = (t.Delay);
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
            this.StopButton.Click += (s, e) => this.Timer.Stop();
            this.PlayButton.Click += (s, e) => this.Timer.Start();

            this.Timer.Tick += (s, e) =>
            {
                int i = this.ItemsSource.Index;

                for (int n = 0; n < 4; n++)
                {
                    MidiPercussionNote note = (MidiPercussionNote)this.Drum[n];
                    this.Synthesizer.NoteOff(note);

                    if (this.ItemsSource[n, i] is ListViewItem item)
                    {
                        if (item.IsSelected)
                        {
                            this.Synthesizer.NoteOn(note);
                        }
                    }
                }

                this.ItemsSource.Next();
                this.ProgressBar.Value = this.ItemsSource.Index;
            };
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e) => this.Synthesizer?.Dispose();
        protected async override void OnNavigatedTo(NavigationEventArgs e) => this.Synthesizer = await MidiSynthesizer.CreateAsync();

        //@Delegate
        public event EventHandler CanExecuteChanged;

        //@Command
        public ICommand Command => this;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            if (parameter is int item)
            {
                this.Initialize(item);
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