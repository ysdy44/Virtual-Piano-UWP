using System.Linq;
using Virtual_Piano.Midi.Core;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using TrackLayout = Virtual_Piano.Midi.Core.TrackNoteLayout;

namespace Virtual_Piano.Midi.Controllers
{
    [ContentProperty(Name = nameof(Pane))]
    public sealed partial class TrackNotePanel : UserControl
    {
        //@Delegate
        public event DragStartedEventHandler DragStarted { remove => this.TimelineThumb.DragStarted -= value; add => this.TimelineThumb.DragStarted += value; }
        public event DragDeltaEventHandler DragDelta { remove => this.TimelineThumb.DragDelta -= value; add => this.TimelineThumb.DragDelta += value; }
        public event DragCompletedEventHandler DragCompleted { remove => this.TimelineThumb.DragCompleted -= value; add => this.TimelineThumb.DragCompleted += value; }

        //@Converter
        private double HeightConverter(bool? value) => value is true ? this.Layout.ExtentHeightFoot : this.Layout.ExtentHeightHead;
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
        public UIElement Pane { get => this.PaneBorder.Child; set => this.PaneBorder.Child = value; }
        public UIElement Foot { get => this.FootBorderBorder.Child; set => this.FootBorderBorder.Child = value; }
        public PointCollection ControllerPoints { get => this.ControllerPolyline.Points; set => this.ControllerPolyline.Points = value; }
        public object FootItemsSource { get => this.FootItemsControl.ItemsSource; set => this.FootItemsControl.ItemsSource = value; }

        // Timeline
        public int Time { get; private set; }
        private int Position;
        private int Timeline;

        readonly TrackLayout Layout = new TrackLayout(75, 22, 150 + 18);
        readonly Windows.UI.Composition.CompositionPropertySet ScrollProperties;

        ~TrackNotePanel() => this.ScrollProperties?.Dispose();
        public TrackNotePanel()
        {
            this.InitializeComponent();

            // Composition
            this.ScrollProperties = this.ScrollViewer.GetScroller();
            var ex = this.ScrollProperties.SnapScrollerX();
            var ey = this.ScrollProperties.SnapScrollerY();
            var sx = this.ScrollProperties.SnapScrollerX(TrackLayout.Step, this.Layout.Pane);

            this.FootBorder.GetVisual().AnimationX(ex);

            this.TimelinePoint.GetVisual().AnimationY(ey);
            this.TimelineLine.GetVisual().AnimationY(ey);

            this.TimelineThumb.GetVisual().AnimationXY(ex, ey);
            this.HeadBorder.GetVisual().AnimationXY(ex, ey);

            this.PaneBorder.GetVisual().AnimationXY(ex, this.Layout.Head);
            this.BodyBackgroundCanvas.GetVisual().AnimationXY(ex, this.Layout.Head);

            this.BodyLineCanvas.GetVisual().AnimationXY(sx, ey);
            this.TimelineTextCanvas.GetVisual().AnimationXY(sx, ey);
            this.TimelinePointCanvas.GetVisual().AnimationXY(sx, ey);

            // BodyBackgroundCanvas
            foreach (MidiNote item in System.Enum.GetValues(typeof(MidiNote)).Cast<MidiNote>())
            {
                switch (item.ToType())
                {
                    case ToneType.White:
                        break;
                    case ToneType.Black:
                        Rectangle rect = new Rectangle
                        {
                            Height = TrackLayout.Spacing
                        };

                        var i = NoteExtensions.NoteCount - (byte)item - 1;
                        var y = i * TrackLayout.Spacing;

                        Canvas.SetTop(rect, y);
                        this.BodyBackgroundCanvas.Children.Add(rect);
                        break;
                    default:
                        break;
                }
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
                        item.Width = w;
                    }
                }

                int h = (int)e.NewSize.Height;
                if (this.ViewportHeight != h)
                {
                    this.ViewportHeight = h;

                    this.TimelineLine.Y2 = h;
                    this.BodyLineCanvas.Height = h;
                    foreach (Line item in this.BodyLineCanvas.Children.Cast<Line>())
                    {
                        item.Y2 = h;
                    }

                    var sy = this.ScrollProperties.SnapScrollerY(h - this.Layout.Foot);
                    this.ControllerBorder.GetVisual().AnimationY(sy);
                    this.FootBorder.GetVisual().AnimationY(sy);
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
                        break;
                    default:
                        break;
                }
            };

            this.ScrollViewer.ViewChanging += (s, e) =>
            {
                this.HorizontalOffset = (int)e.FinalView.HorizontalOffset;
                this.VerticalOffset = (int)e.FinalView.VerticalOffset;
                this.Index = this.HorizontalOffset / TrackLayout.Step;
            };
        }

        public void ChangeDuration(int time)
        {
            int extentWidth = time / TrackLayout.Scaling;
            this.BodyItemsControl.Width = extentWidth;
            this.Canvas.Width = extentWidth + this.Layout.Pane;
            this.ControllerBorder.Width = extentWidth + this.Layout.Pane;
            this.FootItemsControl.Width = extentWidth;
        }

        public void ChangePosition(int timeline)
        {
            this.Timeline = System.Math.Max(this.Layout.Pane, timeline);
            this.Position = this.Timeline + this.HorizontalOffset;
            this.Time = (this.Position - this.Layout.Pane) * TrackLayout.Scaling;

            // UI
            this.TimelineLine.X1 = this.Timeline;
            this.TimelineLine.X2 = this.Timeline;
            this.TimelinePoint.X1 = this.Timeline;
            this.TimelinePoint.X2 = this.Timeline;

            if (this.Timeline < this.HorizontalOffset)
            {
                this.ScrollViewer.ChangeView(this.Position / 2, null, null, true);
            }
            else if (this.Timeline >= this.HorizontalOffset + this.ViewportWidth)
            {
                this.ScrollViewer.ChangeView(this.Position / 2, null, null, true);
            }
        }

        public int UpdateTimeline(int timeline)
        {
            if (timeline > this.Layout.Pane)
            {
                this.Timeline = System.Math.Max(this.Layout.Pane, timeline);
                this.Position = this.Timeline + this.HorizontalOffset;
                this.Time = (this.Position - this.Layout.Pane) * TrackLayout.Scaling;

                // UI
                this.TimelineLine.X1 = this.Timeline;
                this.TimelineLine.X2 = this.Timeline;
                this.TimelinePoint.X1 = this.Timeline;
                this.TimelinePoint.X2 = this.Timeline;
            }
            else
            {
                this.Timeline = this.Layout.Pane;
                this.Position = this.Layout.Pane + this.HorizontalOffset;
                this.Time = this.HorizontalOffset * TrackLayout.Scaling;

                // UI
                this.TimelineLine.X1 = this.Layout.Pane;
                this.TimelineLine.X2 = this.Layout.Pane;
                this.TimelinePoint.X1 = this.Layout.Pane;
                this.TimelinePoint.X2 = this.Layout.Pane;
            }

            return this.Time;
        }
    }
}