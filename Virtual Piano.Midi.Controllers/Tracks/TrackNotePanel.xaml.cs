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
        private double HeightConverter(bool? value) => value is true ? this.Layout.ExtentHeightFoot : this.Layout.ExtentHeight;
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

                for (int i = 0; i < this.TextCanvas.Children.Count; i++)
                {
                    TextBlock item = this.TextCanvas.Children[i] as TextBlock;
                    item.Text = $"{value + i}";
                }
            }
        }

        // UI
        public object ItemsSource { get => this.ItemsControl.ItemsSource; set => this.ItemsControl.ItemsSource = value; }
        public UIElement Pane { get => this.PaneBorder.Child; set => this.PaneBorder.Child = value; }
        public UIElement FootPane { get => this.FootPaneBorder.Child; set => this.FootPaneBorder.Child = value; }
        public PointCollection FootPoints { get => this.Polyline.Points; set => this.Polyline.Points = value; }
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
            var ex2 = this.ScrollProperties.SnapScrollerX(-this.Layout.Pane);
            var ey = this.ScrollProperties.SnapScrollerY();
            var ey2 = this.ScrollProperties.SnapScrollerY(-this.Layout.Head);
            var sx = this.ScrollProperties.SnapScrollerX(TrackLayout.Step, 0);

            this.Polygon.GetVisual().AnimationY(ey2);
            this.Line.GetVisual().AnimationY(ey);
            this.TimelineThumb.GetVisual().AnimationXY(ex, ey2);

            this.PaneBorder.GetVisual().AnimationX(ex2);
            this.FootPaneGrid.GetVisual().AnimationX(ex2);
            this.HeadBorder.GetVisual().AnimationXY(ex2, ey2);

            this.LineCanvas.GetVisual().AnimationXY(sx, ey);
            this.BackgroundCanvas.GetVisual().AnimationX(ex);

            this.TextCanvas.GetVisual().AnimationXY(sx, ey2);
            this.PointCanvas.GetVisual().AnimationXY(sx, ey2);

            // BackgroundCanvas
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
                        this.BackgroundCanvas.Children.Add(rect);
                        break;
                    default:
                        break;
                }
            }

            // LineCanvas
            this.LineCanvas.Width = 16 * TrackLayout.Step;
            for (int i = 0; i < 16; i++)
            {
                double x = i * TrackLayout.Step;
                this.LineCanvas.Children.Add(new Line
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
                    this.LineCanvas.Children.Add(new Line
                    {
                        StrokeThickness = 1,
                        Y1 = 0,
                        X1 = x2,
                        X2 = x2,
                        //Y2 = ?
                    });
                }
            }

            // PointCanvas
            this.PointCanvas.Width = 16 * TrackLayout.Step;
            for (int i = 0; i < 16; i++)
            {
                double x = i * TrackLayout.Step;
                this.PointCanvas.Children.Add(new Line
                {
                    Y1 = 0,
                    X1 = x,
                    X2 = x,
                    Y2 = this.Layout.Head
                });

                for (int j = 0; j < TrackLayout.StepCount; j++)
                {
                    double x2 = x + j * TrackLayout.StepSpacing;
                    this.PointCanvas.Children.Add(new Line
                    {
                        X1 = x2,
                        Y1 = this.Layout.Head - 9,
                        X2 = x2,
                        Y2 = this.Layout.Head
                    });

                    for (int k = 0; k < TrackLayout.StepCount; k++)
                    {
                        double x3 = x2 + k * TrackLayout.StepSpacing2;
                        this.PointCanvas.Children.Add(new Line
                        {
                            X1 = x3,
                            Y1 = this.Layout.Head - 5,
                            X2 = x3,
                            Y2 = this.Layout.Head
                        });
                    }
                }
            }

            // TextCanvas
            for (int i = 0; i < 16; i++)
            {
                TextBlock item = new TextBlock
                {
                    Text = $"{i}"
                };
                Canvas.SetLeft(item, i * TrackLayout.Step);
                this.TextCanvas.Children.Add(item);
            }

            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                int w = (int)e.NewSize.Width - this.Layout.Pane;
                if (this.ViewportWidth != w)
                {
                    this.ViewportWidth = w;

                    this.TimelineThumb.Width = w;
                    this.BackgroundCanvas.Width = w;
                    foreach (FrameworkElement item in this.BackgroundCanvas.Children.Cast<FrameworkElement>())
                    {
                        item.Width = w;
                    }
                }

                int h = (int)e.NewSize.Height - this.Layout.Head;
                if (this.ViewportHeight != h)
                {
                    this.ViewportHeight = h;

                    this.Line.Y2 = h;
                    this.LineCanvas.Height = h;
                    foreach (Line item in this.LineCanvas.Children.Cast<Line>())
                    {
                        item.Y2 = h;
                    }

                    var sy = this.ScrollProperties.SnapScrollerY(h - this.Layout.Foot);
                    this.FootBorder.GetVisual().AnimationY(sy);
                    this.FootPaneGrid.GetVisual().AnimationY(sy);
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
            this.ItemsControl.Width = extentWidth;
            this.Canvas.Width = extentWidth + this.Layout.Pane;
            this.FootBorder.Width = extentWidth + this.Layout.Pane;
            this.FootItemsControl.Width = extentWidth;
        }

        public void ChangePosition(int time)
        {
            this.Time = System.Math.Max(0, time);
            this.Position = this.Time / TrackLayout.Scaling;
            this.Timeline = this.Position - this.HorizontalOffset + this.Layout.Pane;

            // UI
            this.Line.X1 = this.Timeline;
            this.Line.X2 = this.Timeline;
            this.Polygon.X1 = this.Timeline;
            this.Polygon.X2 = this.Timeline;

            if (this.Timeline < this.HorizontalOffset + this.Layout.Pane)
            {
                this.ScrollViewer.ChangeView(this.Position / 2, null, null, true);
            }
            else if (this.Timeline >= this.HorizontalOffset + this.ViewportWidth)
            {
                this.ScrollViewer.ChangeView(this.Position / 2, null, null, true);
            }
        }

        public int UpdateTimeline(int time)
        {
            this.Timeline = System.Math.Max(0, time);
            this.Position = this.Timeline + this.HorizontalOffset;
            this.Time = this.Position * TrackLayout.Scaling;

            // UI
            this.Line.X1 = this.Timeline;
            this.Line.X2 = this.Timeline;
            this.Polygon.X1 = this.Timeline;
            this.Polygon.X2 = this.Timeline;

            return this.Time;
        }
    }
}