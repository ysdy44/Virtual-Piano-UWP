using System;
using System.Windows.Input;
using Virtual_Piano.Elements;
using Windows.System;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Midi.Controls
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

        readonly static KitSet[] Drum = new KitSet[] { KitSet.Open, KitSet.Close, KitSet.Clap, KitSet.Kick, };
        readonly MachineLayout Layout = new MachineLayout(160, 40, 32, Drum.Length);
        readonly Windows.UI.Composition.CompositionPropertySet ScrollProperties;

        ~MachinePanel() => this.ScrollProperties.Dispose();
        public MachinePanel()
        {
            this.InitializeComponent();
            this.ScrollProperties = this.ScrollViewer.GetScroller();
            var x = this.ScrollProperties.SnapScrollerX(-this.Layout.Pane);
            var y = this.ScrollProperties.SnapScrollerY(-this.Layout.Head);
            this.Pane.GetVisual().AnimationX(x);
            this.Timeline.GetVisual().AnimationY(y);
            this.HeadBorder.GetVisual().AnimationXY(x, y);

            for (int i = 0; i < 32; i++)
            {
                this.Timeline.Children.Add(new Line
                {
                    X1 = i * MachineLayout.Spacing + 10,
                    X2 = i * MachineLayout.Spacing + MachineLayout.Spacing - 10,
                    Y1 = this.Layout.Head / 2,
                    Y2 = this.Layout.Head / 2,
                });

                for (int n = 0; n < MachinePanel.Drum.Length; n++)
                {
                    MidiPercussionNote item = (MidiPercussionNote)MachinePanel.Drum[n];
                    this.ItemsControl.Children.Add(new MachineButton
                    {
                        X = i * MachineLayout.Spacing,
                        Y = n * MachineLayout.Spacing,
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
                    Width = this.Layout.Pane,
                    Height = MachineLayout.Spacing,
                    TabIndex = (int)item,
                    Content = this.GetString(item),
                    CommandParameter = item,
                    Command = this
                };
                Canvas.SetLeft(button, 0);
                Canvas.SetTop(button, n * MachineLayout.Spacing);
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
                        double offset = this.ScrollViewer.HorizontalOffset - delta;
                        this.ScrollViewer.ChangeView(offset, null, null);
                        e.Handled = true;
                        break;
                    default:
                        break;
                }
            };
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
                        item.IsChecked = music[index] is 1;
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

        public void Next()
        {
            this.Index++;
            if (this.Index < this.Length) return;
            this.Index = 0;
        }
    }
}