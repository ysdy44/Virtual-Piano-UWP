﻿using System;
using System.Collections.Generic;
using System.Linq;
using Virtual_Piano.Notes;
using Windows.Devices.Midi;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano.TestApp
{
    internal sealed class ProgramGrouping : List<MidiProgram>, IList<MidiProgram>, IGrouping<MidiProgramGroup, MidiProgram>
    {
        public MidiProgramGroup Key { get; }
        public ProgramGrouping(MidiProgramGroup key, IEnumerable<MidiProgram> collection) : base(collection) => this.Key = key;
        public override string ToString() => this.Key.ToString();
    }

    public sealed partial class MidiProgramPage : Page
    {
        private ProgramGrouping[] Groupings => MidiProgramPage.GetItemsSource().ToArray();
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

        private static IEnumerable<ProgramGrouping> GetItemsSource()
        {
            foreach (var item in MidiProgramFactory.Instance)
            {
                foreach (var item2 in item.Value)
                {
                    yield return new ProgramGrouping(item2.Key, item2.Value);
                }
            }
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