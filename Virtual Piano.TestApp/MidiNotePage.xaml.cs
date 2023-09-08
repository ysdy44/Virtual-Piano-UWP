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

        MidiSynthesizer MidiSynthesizer;

        ~MidiNotePage() => this.MidiSynthesizer?.Dispose();
        public MidiNotePage()
        {
            this.InitializeComponent();
            this.GridView.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is int n)
                {
                    int index = ListView.SelectedIndex;
                    if (index < 0) index = 0;
                    this.MidiSynthesizer?.SendMessage(new MidiNoteOnMessage((byte)index, (byte)n, 127));
                }
            };
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
    }
}