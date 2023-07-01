using System.Linq;
using Virtual_Piano.Elements;
using Virtual_Piano.Notes;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Controls
{
    [ContentProperty(Name = nameof(Pane))]
    public sealed partial class TrackPanel : UserControl
    {
        //@Delegate
        public event DragStartedEventHandler TimelineDragStarted { remove => this.TimelineThumb.DragStarted -= value; add => this.TimelineThumb.DragStarted += value; }
        public event DragDeltaEventHandler TimelineDragDelta { remove => this.TimelineThumb.DragDelta -= value; add => this.TimelineThumb.DragDelta += value; }
        public event DragCompletedEventHandler TimelineDragCompleted { remove => this.TimelineThumb.DragCompleted -= value; add => this.TimelineThumb.DragCompleted += value; }

        //@Const
        public const int Scaling = 4;
        public const int Spacing = 21;

        public const int Step = 120;
        public const int StepCount = 4;

        public const int StepSpacing = Step / StepCount;
        public const int StepSpacing2 = StepSpacing / StepCount;

        public int Left => 75;
        public int Top => 18;

        // Container
        public int ViewportWidth { get; private set; }
        public int ViewportHeight { get; private set; }

        public int ExtentWidth { get; private set; } = NoteExtensions.NoteCount * TrackPanel.Spacing;
        public int ExtentWidthLeft => this.ExtentWidth + this.Left;

        public int ExtentHeight => NoteExtensions.NoteCount * TrackPanel.Spacing;
        public int ExtentHeightTop => this.ExtentHeight + this.Top;

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

        // Timeline
        public int Time { get; private set; }
        private int Position;
        private int Timeline;

        public TrackPanel()
        {
            this.InitializeComponent();

            // Composition
            var m = this.ScrollViewer.GetManipulation();
            var ex = m.ExpressionX();
            var ey = m.ExpressionY();
            var sx = m.ExpressionX(TrackPanel.Step, this.Left);

            this.Polygon.GetVisual().OffsetY(ey);
            this.TimelineThumb.GetVisual().Offset(ex, ey);

            this.PaneBorder.GetVisual().Offset(ex, this.Top);

            this.LineCanvas.GetVisual().Offset(sx, ey);
            this.BackgroundCanvas.GetVisual().Offset(ex, this.Top);

            this.TextCanvas.GetVisual().Offset(sx, ey);
            this.PointCanvas.GetVisual().Offset(sx, ey);

            // BackgroundCanvas
            this.BackgroundCanvas.Height = this.ExtentHeightTop;
            foreach (Note item in System.Enum.GetValues(typeof(Note)).Cast<Note>())
            {
                switch (item.ToType())
                {
                    case ToneType.White:
                        break;
                    case ToneType.Black:
                        Rectangle rect = new Rectangle
                        {
                            Height = TrackPanel.Spacing
                        };

                        var i = NoteExtensions.NoteCount - (byte)item - 1;
                        var y = i * TrackPanel.Spacing;

                        Canvas.SetTop(rect, y);
                        this.BackgroundCanvas.Children.Add(rect);
                        break;
                    default:
                        break;
                }
            }

            // LineCanvas
            this.LineCanvas.Width = 16 * TrackPanel.Step;
            for (int i = 0; i < 16; i++)
            {
                double x = i * TrackPanel.Step;
                this.LineCanvas.Children.Add(new Line
                {
                    Y1 = this.Top,
                    X1 = x,
                    X2 = x,
                    //Y2 = ?
                });

                for (int j = 0; j < TrackPanel.StepCount; j++)
                {
                    double x2 = x + j * TrackPanel.StepSpacing;
                    this.LineCanvas.Children.Add(new Line
                    {
                        Y1 = this.Top,
                        X1 = x2,
                        X2 = x2,
                        //Y2 = ?
                    });
                }
            }

            // PointCanvas
            this.PointCanvas.Width = 16 * TrackPanel.Step;
            for (int i = 0; i < 16; i++)
            {
                double x = i * TrackPanel.Step;
                this.PointCanvas.Children.Add(new Line
                {
                    Y1 = 0,
                    X1 = x,
                    X2 = x,
                    Y2 = this.Top
                });

                for (int j = 0; j < TrackPanel.StepCount; j++)
                {
                    double x2 = x + j * TrackPanel.StepSpacing;
                    this.PointCanvas.Children.Add(new Line
                    {
                        Y1 = 9,
                        X1 = x2,
                        X2 = x2,
                        Y2 = this.Top
                    });

                    for (int k = 0; k < TrackPanel.StepCount; k++)
                    {
                        double x3 = x2 + k * TrackPanel.StepSpacing2;
                        this.PointCanvas.Children.Add(new Line
                        {
                            Y1 = 9 + 4,
                            X1 = x3,
                            X2 = x3,
                            Y2 = this.Top
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
                Canvas.SetLeft(item, i * TrackPanel.Step);
                this.TextCanvas.Children.Add(item);
            }

            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                this.ViewportWidth = (int)e.NewSize.Width;
                this.ViewportHeight = (int)e.NewSize.Height;

                this.LineCanvas.Height = e.NewSize.Height;
                foreach (Line item in this.LineCanvas.Children.Cast<Line>())
                {
                    item.Y2 = e.NewSize.Height;
                }

                this.BackgroundCanvas.Width = e.NewSize.Width;
                foreach (FrameworkElement item in this.BackgroundCanvas.Children.Cast<FrameworkElement>())
                {
                    item.Width = e.NewSize.Width;
                }

                // Timeline
                this.TimelineThumb.Width = e.NewSize.Width;
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
                this.Index = this.HorizontalOffset / TrackPanel.Step;
            };
        }

        public void ChangeDuration(double time)
        {
            this.ExtentWidth = (int)(time / TrackPanel.Scaling);
            this.ItemsControl.Width = this.ExtentWidth;
            this.Canvas.Width = this.ExtentWidthLeft;
        }

        public void ChangePosition(double time)
        {
            this.Time = System.Math.Max(0, (int)time);
            this.Position = this.Time / TrackPanel.Scaling;
            this.Timeline = this.Position - this.HorizontalOffset + this.Left;

            // UI
            this.Line.X1 = this.Timeline;
            this.Line.X2 = this.Timeline;
            this.Polygon.X1 = this.Timeline;
            this.Polygon.X2 = this.Timeline;

            if (this.Timeline < this.HorizontalOffset + this.Left)
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
            this.Timeline = System.Math.Max(this.Left, time);
            this.Position = this.Timeline + this.HorizontalOffset - this.Left;
            this.Time = this.Position * TrackPanel.Scaling;

            // UI
            this.Line.X1 = this.Timeline;
            this.Line.X2 = this.Timeline;
            this.Polygon.X1 = this.Timeline;
            this.Polygon.X2 = this.Timeline;

            return this.Time;
        }
    }
}