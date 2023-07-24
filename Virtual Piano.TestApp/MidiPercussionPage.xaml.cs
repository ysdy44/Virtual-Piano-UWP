using System;
using System.Collections.Generic;
using System.Linq;
using Virtual_Piano.Midi;
using Windows.Devices.Midi;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano.TestApp
{
    internal sealed class PercussionGrouping : List<MidiPercussionNote>, IList<MidiPercussionNote>, IGrouping<MidiPercussionNoteCategory, MidiPercussionNote>
    {
        public MidiPercussionNoteCategory Key { get; }
        public PercussionGrouping(MidiPercussionNoteCategory key, IEnumerable<MidiPercussionNote> collection) : base(collection) => this.Key = key;
        public override string ToString() => this.Key.ToString();
    }

    public sealed partial class MidiPercussionPage : Page
    {
        private PercussionGrouping[] Groupings =>
            MidiPercussionNoteFactory.Instance.Select(c =>
            new PercussionGrouping(c.Key, c.Value)).ToArray();

        readonly byte Channel = 9;

        byte Note;
        public MidiSynthesizer Synthesizer { get; set; }

        public MidiPercussionPage()
        {
            this.InitializeComponent();
            this.ListView.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is MidiPercussionNote item)
                {
                    byte n = (byte)item;
                    this.Synthesizer?.SendMessage(new MidiNoteOffMessage(this.Channel, this.Note, 127));
                    this.Synthesizer?.SendMessage(new MidiNoteOnMessage(this.Channel, n, 127));
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