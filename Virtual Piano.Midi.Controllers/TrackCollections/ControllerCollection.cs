using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Virtual_Piano.Midi.Core;
using Windows.Devices.Midi;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed class ControllerCollection : IList<MidiMessage>
    {
        public readonly IList<MidiMessage> Items;
        public readonly ObservableCollection<ContentControl> ItemsSource;
        public readonly PointCollection Points;

        public int Count => this.Items.Count;
        public bool IsReadOnly => this.Items.IsReadOnly;

        public MidiMessage this[int index]
        {
            get => this.Items[index];
            set => this.Items[index] = value; // Pin
        }

        public ControllerCollection(long duration = 2048, byte defaultValue = Radial.Velocity)
        {
            int lastY = Radial.Velocity - defaultValue;
            this.Items = new List<MidiMessage>();
            this.ItemsSource = new ObservableCollection<ContentControl>();
            this.Points = new PointCollection
            {
                new Point(0, Radial.Velocity), // StartPoint
                new Point(0, lastY), // Last
                new Point(duration / TrackNoteLayout.Scaling, lastY), // Point
                new Point(duration / TrackNoteLayout.Scaling, Radial.Velocity), // EndPoint
            };
        }
        public ControllerCollection(IEnumerable<MidiMessage> items, long duration, byte defaultValue = Radial.Velocity)
        {
            int lastY = Radial.Velocity - defaultValue;
            this.Items = new List<MidiMessage>(items);
            this.ItemsSource = new ObservableCollection<ContentControl>();
            this.Points = new PointCollection
            {
                new Point(0, Radial.Velocity), // StartPoint
                new Point(0, lastY), // Last
            };

            foreach (MidiMessage item in items)
            {
                int x = item.AbsoluteTime / TrackNoteLayout.Scaling;
                int y = Radial.Velocity - item.ControllerValue;

                ContentControl control = new ContentControl
                {
                    Width = 66,
                    Height = 66
                };
                Canvas.SetLeft(control, x - (66 / 2));
                Canvas.SetTop(control, y - (66 / 2));
                this.ItemsSource.Add(control);

                this.Points.Add(new Point(x, lastY)); // Point
                this.Points.Add(new Point(x, y)); // Last

                lastY = y;
            }

            this.Points.Add(new Point(duration / TrackNoteLayout.Scaling, lastY)); // Point
            this.Points.Add(new Point(duration / TrackNoteLayout.Scaling, Radial.Velocity)); // EndPoint
        }
        public ControllerCollection(IEnumerable<ContentControl> items, int duration, byte defaultValue = Radial.Velocity)
        {
            int lastY = Radial.Velocity - defaultValue;
            this.Items = new List<MidiMessage>();
            this.ItemsSource = new ObservableCollection<ContentControl>();
            this.Points = new PointCollection
            {
                new Point(0, Radial.Velocity), // StartPoint
                new Point(0, lastY), // Last
            };

            foreach (ContentControl item in items)
            {
                if (item is null) continue;

                if (item.Content is MidiMessage message)
                {
                    this.Items.Add(message);

                    int x = message.AbsoluteTime / TrackNoteLayout.Scaling;
                    int y = Radial.Velocity - message.ControllerValue;

                    ContentControl control = new ContentControl
                    {
                        Width = 66,
                        Height = 66
                    };
                    Canvas.SetLeft(control, x - 5);
                    Canvas.SetTop(control, y - 5);
                    this.ItemsSource.Add(control);

                    this.Points.Add(new Point(x, lastY)); // Point
                    this.Points.Add(new Point(x, y)); // Last

                    lastY = y;
                }
            }

            this.Points.Add(new Point(duration / TrackNoteLayout.Scaling, lastY)); // Point
            this.Points.Add(new Point(duration / TrackNoteLayout.Scaling, Radial.Velocity)); // EndPoint
        }

        public void Add(MidiMessage item) => this.Items.Add(item); // Pin
        public void Clear() => this.Items.Clear(); // Pin
        public void Insert(int index, MidiMessage item) => this.Items.Insert(index, item); // Pin
        public bool Remove(MidiMessage item) => this.Items.Remove(item); // Pin
        public void RemoveAt(int index) => this.Items.RemoveAt(index); // Pin

        public int IndexOf(MidiMessage item) => this.Items.IndexOf(item);
        public bool Contains(MidiMessage item) => this.Items.Contains(item);
        public void CopyTo(MidiMessage[] array, int arrayIndex) => this.Items.CopyTo(array, arrayIndex);

        IEnumerator IEnumerable.GetEnumerator() => this.Items.GetEnumerator();
        public IEnumerator<MidiMessage> GetEnumerator() => this.Items.GetEnumerator();
    }
}