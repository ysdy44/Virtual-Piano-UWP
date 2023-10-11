using System;
using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.System;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Midi.Controllers
{
    public partial class MachinePanel : UserControl, ICommand
    {
        //@Command
        public ICommand Command { get; set; }

        public int Index { get; private set; } = 0;
        public int Length { get; private set; } = 32;

        public UIElement this[int index]
        {
            get => this.ItemsControl.Children[index];
            set => this.ItemsControl.Children[index] = value;
        }
        public UIElement this[int n, int i]
        {
            get => this[n + i * Drum.Length];
            set => this[n + i * Drum.Length] = value;
        }

        Style Black => base.Resources[$"{ToneType.Black}"] as Style;
        Style White => base.Resources[$"{ToneType.White}"] as Style;

        readonly Style AccentLineStyle;
        readonly Style LineStyle;

        readonly MidiPercussionNoteCategoryDictionary PercussionNoteCategoryDictionary = new MidiPercussionNoteCategoryDictionary();
        readonly MidiPercussionNoteDictionary PercussionNoteDictionary = new MidiPercussionNoteDictionary();

        readonly static KitSet[] Drum = new KitSet[] { KitSet.Open, KitSet.Close, KitSet.Clap, KitSet.Kick };
        readonly MachineLayout Layout = new MachineLayout(50, 60, 24, 16, 32, Drum.Length);
        readonly Windows.UI.Composition.CompositionPropertySet ScrollProperties;

        readonly DispatcherTimer Timer = new DispatcherTimer();

        private Tempo tempo = new Tempo(new Ticks(new TimeSignature(4, 4), 480), 60);
        public int Tempo
        {
            get => this.tempo.Bpm;
            set
            {
                Tempo tempo = new Tempo(new Ticks(new TimeSignature(4, 4), 480), value);

                this.Timer.Interval = tempo.MillisecondsPerBeat;
                this.tempo = tempo;
            }
        }

        //@Construct
        ~MachinePanel() => this.ScrollProperties.Dispose();
        public MachinePanel()
        {
            this.InitializeComponent();
            this.ScrollProperties = this.ScrollViewer.GetScroller();
            var x = this.ScrollProperties.SnapScrollerX();
            this.Pane.GetVisual().StartX(x);
            this.HeadBorder.GetVisual().StartX(x);

            for (int i = 0; i < 32 / MachinePanel.Drum.Length; i++)
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = $"{1 + i}"
                };
                Canvas.SetLeft(textBlock, this.Layout.Pane + i * MachinePanel.Drum.Length * this.Layout.Spacing + 10);
                this.TimelineTextCanvas.Children.Add(textBlock);
            }

            this.AccentLineStyle = base.Resources[nameof(AccentLineStyle)] as Style;
            this.LineStyle = base.Resources[nameof(LineStyle)] as Style;
            for (int i = 0; i < 32; i++)
            {
                this.TimelineLineCanvas.Children.Add(new Line
                {
                    X1 = this.Layout.Pane + i * this.Layout.Spacing + 10,
                    X2 = this.Layout.Pane + i * this.Layout.Spacing + this.Layout.Spacing - 10,
                    Y1 = this.Layout.Timeline2 / 2,
                    Y2 = this.Layout.Timeline2 / 2,
                    Style = this.LineStyle
                });
            }

            for (int i = 0; i < 32; i++)
            {
                for (int n = 0; n < MachinePanel.Drum.Length; n++)
                {
                    MidiPercussionNote item = (MidiPercussionNote)MachinePanel.Drum[n];
                    this.ItemsControl.Children.Add(new MachineButton
                    {
                        X = this.Layout.Pane + i * this.Layout.Spacing,
                        Y = this.Layout.Head + n * this.Layout.Spacing,
                        Width = this.Layout.Spacing,
                        Height = this.Layout.Spacing,
                        Style = i / 4 % 2 == 0 ? this.Black : this.White,
                        CommandParameter = item,
                        Command = this
                    });
                }
            }

            for (int n = 0; n < MachinePanel.Drum.Length; n++)
            {
                MidiPercussionNote item = (MidiPercussionNote)MachinePanel.Drum[n];
                Button button = new Button
                {
                    Content = $"{this.GetString(item)}",
                    CommandParameter = item,
                    Command = this,
                    TabIndex = (byte)item,
                    Foreground = this.PercussionNoteCategoryDictionary[this.GetCategory(item)],
                    Width = this.Layout.Pane,
                    Height = this.Layout.Spacing,
                    Tag = new PathIcon
                    {
                        Data = this.PercussionNoteDictionary[item]
                    }
                };
                Canvas.SetLeft(button, 0);
                Canvas.SetTop(button, this.Layout.Head + n * this.Layout.Spacing);
                this.Pane.Children.Add(button);
            }

            base.PointerWheelChanged += (s, e) =>
            {
                PointerPoint pp = e.GetCurrentPoint(this);
                if (pp.Properties.IsHorizontalMouseWheel) return;

                int delta = pp.Properties.MouseWheelDelta;
                if (delta == 0) return;

                switch (e.KeyModifiers)
                {
                    case VirtualKeyModifiers.Control:
                        break;
                    case VirtualKeyModifiers.Shift:
                        double offset = this.ScrollViewer.HorizontalOffset - delta;
                        this.ScrollViewer.ChangeView(offset, null, null);
                        e.Handled = true;
                        break;
                    default:
                        break;
                }
            };

            this.StopButton.Visibility = Visibility.Collapsed;
            this.PlayButton.Visibility = Visibility.Visible;
            this.StopButton.Click += (s, e) => this.Stop();
            this.PlayButton.Click += (s, e) => this.Play();

            this.Timer.Interval = this.tempo.MillisecondsPerBeat;
            this.Timer.Tick += (s, e) =>
            {
                int i = this.Index;

                for (int n = 0; n < 4; n++)
                {
                    MidiPercussionNote note = (MidiPercussionNote)MachinePanel.Drum[n];

                    if (this[n, i] is UIElement item)
                    {
                        if (item.AllowDrop)
                        {
                            this.Execute(note);
                        }
                    }
                }

                this.Next();
            };
        }

        private MidiPercussionNoteCategory GetCategory(MidiPercussionNote item)
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

        //@Delegate
        public event EventHandler CanExecuteChanged;

        //@Command
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            this.Command?.Execute(parameter);
        }

        public virtual string GetString(MidiPercussionNote note)
        {
            return note.ToString();
        }

        public void Initialize(byte[] music)
        {
            this.Index = 0;
            this.Length = music.Length / MachinePanel.Drum.Length;

            for (int i = 0; i < this.Length; i++)
            {
                for (int n = 0; n < MachinePanel.Drum.Length; n++)
                {
                    int index = n + i * MachinePanel.Drum.Length;
                    if (this[index] is IMachineButton item)
                    {
                        item.IsEnabled = true;
                        item.IsChecked = music[index] != default;
                    }
                }
            }

            for (int i = this.Length; i < 32; i++)
            {
                for (int n = 0; n < MachinePanel.Drum.Length; n++)
                {
                    if (this[n, i] is IMachineButton item)
                    {
                        item.IsEnabled = false;
                        item.IsChecked = false;
                    }
                }
            }
        }

        public void Deselect()
        {
            foreach (IMachineButton item in this.ItemsControl.Children)
            {
                item.IsChecked = false;
            }
        }

        private void Stop()
        {
            this.StopButton.Visibility = Visibility.Collapsed;
            this.PlayButton.Visibility = Visibility.Visible;
            this.Timer.Stop();

            if (this.TimelineLineCanvas.Children[this.Index] is Line element)
            {
                element.Style = this.LineStyle;
            }
            this.Index = 0;
        }

        private void Play()
        {
            this.PlayButton.Visibility = Visibility.Collapsed;
            this.StopButton.Visibility = Visibility.Visible;
            this.Timer.Start();

            if (this.TimelineLineCanvas.Children[this.Index] is Line element)
            {
                element.Style = this.AccentLineStyle;
            }
            this.Index = 0;
        }

        public void Next()
        {
            if (this.TimelineLineCanvas.Children[this.Index] is Line element1)
            {
                element1.Style = this.LineStyle;
            }

            this.Index++;
            if (this.Index >= this.Length)
            {
                this.Index = 0;
            }

            if (this.TimelineLineCanvas.Children[this.Index] is Line element2)
            {
                element2.Style = this.AccentLineStyle;
            }
        }
    }
}