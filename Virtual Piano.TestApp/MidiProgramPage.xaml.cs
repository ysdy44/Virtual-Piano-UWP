﻿using System;
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
                    yield return new InstrumentItemGrouping(item2.Value)
                    {
                        Key = item2.Key,
                        Text = $"{item2.Key}"
                    };
                }
            }
        }
    }

    public sealed partial class MidiProgramPage : Page
    {
        private int[] ItemsSource => System.Linq.Enumerable.Range(0, 128).ToArray();

        readonly byte Channel = 0;

        byte Note;
        MidiSynthesizer MidiSynthesizer;

        ~MidiProgramPage() => this.MidiSynthesizer?.Dispose();
        public MidiProgramPage()
        {
            this.InitializeComponent();
            this.ListView.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is MidiProgram item)
                {
                    byte n = (byte)item;
                    this.MidiSynthesizer?.SendMessage(new MidiProgramChangeMessage(this.Channel, n));
                }
            };
            this.GridView.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is int n)
                {
                    this.MidiSynthesizer?.SendMessage(new MidiNoteOffMessage(this.Channel, this.Note, 127));
                    this.MidiSynthesizer?.SendMessage(new MidiNoteOnMessage(this.Channel, (byte)n, 127));
                    this.Note = (byte)n;
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