﻿using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Controls
{
    public partial class DrumGridView : WrapPanel, IDrumPanel
    {
        //@Command
        public ICommand Command { get; set; }

        public IDrumButton this[MidiPercussionNote note] =>
            base.Children[this.ItemsSource.IndexOf(note)] as IDrumButton;

        public MidiPercussionNote this[int index]
        {
            get => this.ItemsSource[index];
            set
            {
                if (this.ItemsSource[index] == value) return;
                this.ItemsSource[index] = value;

                if (base.Children[index] is IDrumButton item)
                {
                    item.CommandParameter = value;
                    item.TabIndex = (byte)value;
                    item.Foreground = base.Resources[$"{this.GetKey(value)}"] as Brush;
                    item.Content = this.GetString(value);
                }
            }
        }

        private readonly IList<MidiPercussionNote> ItemsSource;

        //@Construct
        public DrumGridView()
        {
            this.InitializeComponent();
            this.ItemsSource = new List<MidiPercussionNote>(this.GetKeyItemsSource());

            foreach (MidiPercussionNote item in this.ItemsSource)
            {
                base.Children.Add(new DrumButton
                {
                    CommandParameter = item,
                    TabIndex = (byte)item,
                    Foreground = base.Resources[$"{this.GetKey(item)}"] as Brush,
                    Content = $"{this.GetString(item)}"
                });
            }
        }

        private IEnumerable<MidiPercussionNote> GetKeyItemsSource()
        {
            foreach (var item2 in MidiPercussionNoteFactory.Instance)
            {
                foreach (var item3 in item2.Value)
                {
                    yield return item3;
                }
            }
        }

        private MidiPercussionNoteCategory GetKey(MidiPercussionNote item)
        {
            foreach (var item2 in MidiPercussionNoteFactory.Instance)
            {
                foreach (var item3 in item2.Value)
                {
                    if (item3 == item)
                    {
                        return item2.Key;
                    }
                }
            }

            return default;
        }

        public void OnClick(MidiPercussionNote note) => this.Command?.Execute(note); // Command

        public virtual string GetString(MidiPercussionNote note)
        {
            return note.ToString();
        }

        public void Clear(int index)
        {
            if (base.Children[index] is IDrumButton item)
            {
                item.Clear();
            }
        }

        public void Add(int index)
        {
            if (base.Children[index] is IDrumButton item)
            {
                item.Add();
            }
        }

        public void Clear(MidiPercussionNote note)
        {
            int i = this.ItemsSource.IndexOf(note);
            if (base.Children[i] is IDrumButton item)
            {
                item.Clear();
            }
        }

        public void Add(MidiPercussionNote note)
        {
            int i = this.ItemsSource.IndexOf(note);
            if (base.Children[i] is IDrumButton item)
            {
                item.Add();
            }
        }
    }
}