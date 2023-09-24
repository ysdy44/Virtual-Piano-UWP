using System;
using System.Linq;
using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed partial class TrackPanel : UserControl, ICommand, ITrackPanel
    {
        //@Delegate
        public event EventHandler<int> ItemClick;
        public event DragStartedEventHandler DragStarted { remove => this.TimelineThumb.DragStarted -= value; add => this.TimelineThumb.DragStarted += value; }
        public event DragDeltaEventHandler DragDelta { remove => this.TimelineThumb.DragDelta -= value; add => this.TimelineThumb.DragDelta += value; }
        public event DragCompletedEventHandler DragCompleted { remove => this.TimelineThumb.DragCompleted -= value; add => this.TimelineThumb.DragCompleted += value; }

        //@Converter
        private Visibility VisibilityConverter(bool? value) => value is true ? Visibility.Visible : Visibility.Collapsed;

        // Container
        public int ViewportWidth { get; private set; }
        public int ViewportHeight { get; private set; }

        // public int ExtentWidth { get; private set; }
        // public int ExtentHeight { get; private set; }

        // Content
        public int HorizontalOffset { get; private set; }
        public int VerticalOffset { get; private set; }

        private int index;
        public int Index
        {
            get => this.index;
            set
            {
                if (this.index == value) return;
                this.index = value;

                for (int i = 0; i < this.TimelineTextCanvas.Children.Count; i++)
                {
                    TextBlock item = this.TimelineTextCanvas.Children[i] as TextBlock;
                    item.Text = $"{value + i}";
                }
            }
        }

        // UI
        public object ItemsSource { get => this.BodyItemsControl.ItemsSource; set => this.BodyItemsControl.ItemsSource = value; }

        // Timeline
        public int Position { get; private set; }
        private int PositionUI;
        private int TimelineUI;

        readonly TrackLayout Layout = new TrackLayout(175, 18);
        readonly Windows.UI.Composition.CompositionPropertySet ScrollProperties;

        ~TrackPanel() => this.ScrollProperties?.Dispose();
        public TrackPanel()
        {
            this.InitializeComponent();

            // Composition
            this.ScrollProperties = this.ScrollViewer.GetScroller();
            var ex = this.ScrollProperties.SnapScrollerX();
            var ey = this.ScrollProperties.SnapScrollerY();
            var ey3 = this.ScrollProperties.SnapScrollerY(1 - this.Layout.Timerline1);
            var sx = this.ScrollProperties.SnapScrollerX(TrackLayout.Step, this.Layout.Pane);

            this.TimelinePoint.GetVisual().AnimationY(ey);
            this.TimelineLine.GetVisual().AnimationY(ey3);

            this.TimelineThumb.GetVisual().AnimationXY(ex, ey);
            this.HeadBorder.GetVisual().AnimationXY(ex, ey);

            this.PaneStackPanel.GetVisual().AnimationXY(ex, this.Layout.Head);
            this.BodyBackgroundCanvas.GetVisual().AnimationXY(ex, this.Layout.Head);

            this.BodyLineCanvas.GetVisual().AnimationXY(sx, ey);
            this.TimelineTextCanvas.GetVisual().AnimationXY(sx, ey);
            this.TimelinePointCanvas.GetVisual().AnimationXY(sx, ey);

            // PaneListBox
            foreach (MidiChannel item in System.Enum.GetValues(typeof(MidiChannel)).Cast<MidiChannel>())
            {
                int i = (int)item;
                this.PaneStackPanel.Children.Add(new TrackChannel
                {
                    Height = TrackLayout.ItemSize,
                    Label = $"{i}",
                    Text = "Value",
                    Command = this,
                    CommandParameter = i,
                });
            }

            // BodyBackgroundCanvas
            foreach (MidiChannel item in System.Enum.GetValues(typeof(MidiChannel)).Cast<MidiChannel>())
            {
                int i = (int)item;
                int y = i * TrackLayout.ItemSize;
                this.BodyBackgroundCanvas.Children.Add(new Line
                {
                    Y1 = y,
                    Y2 = y,
                });
            }

            // BodyLineCanvas
            this.BodyLineCanvas.Width = 16 * TrackLayout.Step;
            for (int i = 0; i < 16; i++)
            {
                double x = i * TrackLayout.Step;
                this.BodyLineCanvas.Children.Add(new Line
                {
                    StrokeThickness = 2,
                    Y1 = 0,
                    X1 = x,
                    X2 = x,
                    //Y2 = ?
                });

                for (int j = 1; j < TrackLayout.StepCount; j++)
                {
                    double x2 = x + j * TrackLayout.StepSpacing;
                    this.BodyLineCanvas.Children.Add(new Line
                    {
                        StrokeThickness = 1,
                        Y1 = 0,
                        X1 = x2,
                        X2 = x2,
                        //Y2 = ?
                    });
                }
            }

            // TimelinePointCanvas
            this.TimelinePointCanvas.Width = 16 * TrackLayout.Step;
            for (int i = 0; i < 16; i++)
            {
                double x = i * TrackLayout.Step;
                this.TimelinePointCanvas.Children.Add(new Line
                {
                    Y1 = 0,
                    X1 = x,
                    X2 = x,
                    Y2 = this.Layout.Head
                });

                for (int j = 0; j < TrackLayout.StepCount; j++)
                {
                    double x2 = x + j * TrackLayout.StepSpacing;
                    this.TimelinePointCanvas.Children.Add(new Line
                    {
                        X1 = x2,
                        Y1 = this.Layout.Head - 9,
                        X2 = x2,
                        Y2 = this.Layout.Head
                    });

                    for (int k = 0; k < TrackLayout.StepCount; k++)
                    {
                        double x3 = x2 + k * TrackLayout.StepSpacing2;
                        this.TimelinePointCanvas.Children.Add(new Line
                        {
                            X1 = x3,
                            Y1 = this.Layout.Head - 5,
                            X2 = x3,
                            Y2 = this.Layout.Head
                        });
                    }
                }
            }

            // TimelineTextCanvas
            for (int i = 0; i < 16; i++)
            {
                TextBlock item = new TextBlock
                {
                    Text = $"{i}"
                };
                Canvas.SetLeft(item, i * TrackLayout.Step);
                this.TimelineTextCanvas.Children.Add(item);
            }
            this.TimelineTextCanvas.Width = 16 * TrackLayout.Step;

            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                int w = (int)e.NewSize.Width;
                if (this.ViewportWidth != w)
                {
                    this.ViewportWidth = w;

                    this.TimelineThumb.Width = w;
                    this.BodyBackgroundCanvas.Width = w;
                    foreach (FrameworkElement item in this.BodyBackgroundCanvas.Children.Cast<FrameworkElement>())
                    {
                        if (item is Line line)
                        {
                            line.X2 = w;
                        }
                        else
                        {
                            item.Width = w;
                        }
                    }
                }

                int h = (int)e.NewSize.Height;
                if (this.ViewportHeight != h)
                {
                    this.ViewportHeight = h;

                    this.TimelineLine.Y2 = h + this.Layout.Timerline1;
                    this.BodyLineCanvas.Height = h;
                    foreach (Line item in this.BodyLineCanvas.Children.Cast<Line>())
                    {
                        item.Y2 = h;
                    }
                }
            };

            base.PointerWheelChanged += (s, e) =>
            {
                PointerPoint pp = e.GetCurrentPoint(this);
                if (pp.Properties.IsHorizontalMouseWheel) return;

                int delta = pp.Properties.MouseWheelDelta;
                if (delta == 0) return;

                switch (e.KeyModifiers)
                {
                    case VirtualKeyModifiers.Control:
                        double offset = this.HorizontalOffset - delta;
                        this.ScrollViewer.ChangeView(offset, null, null);
                        e.Handled = true;
                        break;
                    default:
                        break;
                }
            };

            this.TimelineThumb.PointerWheelChanged += (s, e) => e.Handled = true;

            this.ScrollViewer.ViewChanging += (s, e) =>
            {
                this.HorizontalOffset = (int)e.FinalView.HorizontalOffset;
                this.VerticalOffset = (int)e.FinalView.VerticalOffset;
                this.Index = this.HorizontalOffset / TrackLayout.Step;
            };
        }

        public void ChangeDuration(long duration)
        {
            double extentWidth = duration / TrackLayout.Scaling;
            this.BodyItemsControl.Width = extentWidth;
            this.Canvas.Width = extentWidth + this.Layout.Pane;
        }

        public void ChangePosition(int position, bool scrollNext, bool scrollPrevious)
        {
            this.Position = System.Math.Max(0, position);
            this.PositionUI = position / TrackLayout.Scaling;
            this.TimelineUI = this.PositionUI + this.Layout.Pane;

            // UI
            this.TimelineLine.X1 = this.TimelineUI;
            this.TimelineLine.X2 = this.TimelineUI;
            this.TimelinePoint.X1 = this.TimelineUI;
            this.TimelinePoint.X2 = this.TimelineUI;

            if (scrollNext && this.TimelineUI >= this.HorizontalOffset + this.ViewportWidth)
            {
                this.ScrollViewer.ChangeView(this.TimelineUI - this.Layout.Pane, null, null, true);
            }
            else if (scrollPrevious && this.TimelineUI < this.HorizontalOffset)
            {
                this.ScrollViewer.ChangeView(this.TimelineUI - this.Layout.Pane, null, null, true);
            }
        }

        public void Stop()
        {
            this.Position = 0;
            this.PositionUI = 0;
            this.TimelineUI = this.Layout.Pane;

            // UI
            this.TimelineLine.X1 = this.Layout.Pane;
            this.TimelineLine.X2 = this.Layout.Pane;
            this.TimelinePoint.X1 = this.Layout.Pane;
            this.TimelinePoint.X2 = this.Layout.Pane;

            this.ScrollViewer.ChangeView(0, null, null, true);
        }

        public void ChangePositionUI(int positionUI)
        {
            if (positionUI > this.Layout.Pane)
            {
                this.Position = this.PositionUI * TrackLayout.Scaling;
                this.PositionUI = this.TimelineUI - this.Layout.Pane;
                this.TimelineUI = System.Math.Max(this.Layout.Pane, positionUI);

                // UI
                this.TimelineLine.X1 = this.TimelineUI;
                this.TimelineLine.X2 = this.TimelineUI;
                this.TimelinePoint.X1 = this.TimelineUI;
                this.TimelinePoint.X2 = this.TimelineUI;
            }
            else
            {
                this.Position = 0;
                this.PositionUI = 0;
                this.TimelineUI = this.Layout.Pane;

                // UI
                this.TimelineLine.X1 = this.Layout.Pane;
                this.TimelineLine.X2 = this.Layout.Pane;
                this.TimelinePoint.X1 = this.Layout.Pane;
                this.TimelinePoint.X2 = this.Layout.Pane;
            }
        }

        //@Delegate
        public event EventHandler CanExecuteChanged;

        //@Command
        public ICommand Command => this;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            if (this.ItemClick is null) return;

            if (parameter is int item)
            {
                this.ItemClick(this, item); // Delegate
            }
        }
    }
}