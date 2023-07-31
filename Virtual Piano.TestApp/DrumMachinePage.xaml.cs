using System;
using System.Collections.Generic;
using System.Linq;
using Virtual_Piano.Midi;
using Windows.Devices.Midi;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano.TestApp
{
    public sealed class DrumMachine : List<ListViewItem>
    {
        //@Static
        public static readonly KitSet[] Drum = new KitSet[]
        {
             KitSet.Open,
             KitSet.Close,
             KitSet.Clap,
             KitSet.Kick,
        };
        public static readonly byte[] Music = new byte[]
        {
            0,0,1,0,  0,0,0,0,  0,0,0,0,  0,0,0,0,
            1,1,0,0,  1,0,1,0,  1,0,1,0,  1,0,1,0,
            0,0,0,0,  1,0,0,0,  0,0,0,0,  1,0,0,0,
            1,0,0,0,  0,0,0,1,  0,0,1,0,  0,0,0,1,
        };

        public DrumMachine()
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    int i = 16 * y + x;
                    base.Add(new ListViewItem
                    {
                        Content = DrumMachine.Drum[y],
                    });
                }
            }
        }

        public void Initialize()
        {
            for (int i = 0; i < 4 * 16; i++)
            {
                if (this[i] is ListViewItem item)
                {
                    item.IsSelected = DrumMachine.Music[i] is 1;
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
    }

    public sealed partial class DrumMachinePage : Page
    {
        readonly int[] Indexs = System.Linq.Enumerable.Range(0, 16).ToArray();
        readonly DrumMachine ItemsSource = new DrumMachine();
        readonly DispatcherTimer Timer = new DispatcherTimer();

        MidiSynthesizer Synthesizer;
        ~DrumMachinePage() => this.Synthesizer?.Dispose();
        public DrumMachinePage()
        {
            this.InitializeComponent();
            this.Timer.Interval = TimeSpan.FromMilliseconds(100);
            this.Run.Text = $"{100}";
            this.Slider.Value = 100 / 20;
            this.Slider.ValueChanged += (s, e) =>
            {
                int value = (int)e.NewValue * 20;
                this.Run.Text = $"{value}";
                this.Timer.Interval = TimeSpan.FromMilliseconds(value);
            };

            base.Loaded += (s, e) => this.ItemsSource.Initialize();
            this.ResetButton.Click += (s, e) => this.ItemsSource.Initialize();
            this.ClearButton.Click += (s, e) => this.ItemsSource.Deselect();
            this.StopButton.Click += (s, e) =>
            {
                this.ListBox.SelectedIndex = 0;
                this.Timer.Stop();
            };
            this.PlayButton.Click += (s, e) =>
            {
                this.ListBox.SelectedIndex = 0;
                this.Timer.Start();
            };

            this.Timer.Tick += (s, e) =>
            {
                int x = this.ListBox.SelectedIndex;

                for (int y = 0; y < 4; y++)
                {
                    MidiPercussionNote note = (MidiPercussionNote)DrumMachine.Drum[y];
                    this.Synthesizer.NoteOff(note);

                    int i = 16 * y + x;
                    if (this.ItemsSource[i] is ListViewItem item)
                    {
                        if (item.IsSelected)
                        {
                            this.Synthesizer.NoteOn(note);
                        }
                    }
                }

                x++;
                if (x < 16)
                {
                    this.ListBox.SelectedIndex = x;
                }
                else
                {
                    x = 0;
                    this.ListBox.SelectedIndex = 0;
                }
            };
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e) => this.Synthesizer?.Dispose();
        protected async override void OnNavigatedTo(NavigationEventArgs e) => this.Synthesizer = await MidiSynthesizer.CreateAsync();
    }
}