using System;
using System.Linq;
using Windows.Devices.Midi;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano.TestApp
{
    public sealed partial class MidiNotePage : Page
    {
        readonly int[] Items = System.Linq.Enumerable.Range(0, 15).ToArray();
        readonly int[] ItemsSource = System.Linq.Enumerable.Range(0, 128).ToArray();

        public MidiSynthesizer Synthesizer { get; set; }

        public MidiNotePage()
        {
            this.InitializeComponent();
            this.GridView.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is int n)
                {
                    int index = ListView.SelectedIndex;
                    if (index < 0) index = 0;
                    this.Synthesizer?.SendMessage(new MidiNoteOnMessage((byte)index, (byte)n, 127));
                }
            };
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.Synthesizer?.Dispose();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Synthesizer?.Dispose();
            this.Synthesizer = await MidiSynthesizer.CreateAsync();
        }
    }
}