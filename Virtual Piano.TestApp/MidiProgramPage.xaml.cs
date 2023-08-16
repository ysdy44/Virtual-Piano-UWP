using System;
using System.Collections.Generic;
using System.Linq;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Core;
using Windows.Devices.Midi;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano.TestApp
{
    public sealed class InstrumentGroupingCollection : List<InstrumentItemGrouping>
    {
        public InstrumentGroupingCollection() : base(GetItemsSource()) { }

        private static IEnumerable<InstrumentItemGrouping> GetItemsSource()
        {
            foreach (var item in MidiProgramFactory.Instance)
            {
                foreach (var item2 in item.Value)
                {
                    yield return new InstrumentItemGrouping(item2.Value.Select(GetItem))
                    {
                        Key = item2.Key,
                        Text = $"{item2.Key}"
                    };
                }
            }
        }

        private static InstrumentItem GetItem(MidiProgram item) => new InstrumentItem
        {
            Key = item,
            Text = $"{item}"
        };
    }

    public sealed partial class MidiProgramPage : Page
    {
        private int[] ItemsSource => System.Linq.Enumerable.Range(0, 128).ToArray();

        readonly byte Channel = 0;

        byte Note;
        public MidiSynthesizer Synthesizer { get; set; }

        public MidiProgramPage()
        {
            this.InitializeComponent();
            this.ListView.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is MidiProgram item)
                {
                    byte n = (byte)item;
                    this.Synthesizer?.SendMessage(new MidiProgramChangeMessage(this.Channel, n));
                }
            };
            this.GridView.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is int n)
                {
                    this.Synthesizer?.SendMessage(new MidiNoteOffMessage(this.Channel, this.Note, 127));
                    this.Synthesizer?.SendMessage(new MidiNoteOnMessage(this.Channel, (byte)n, 127));
                    this.Note = (byte)n;
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