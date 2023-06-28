using System.Linq;
using Virtual_Piano.Notes;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Controls
{
    public sealed class CropGrid : Grid
    {
        readonly RectangleGeometry RectangleGeometry = new RectangleGeometry();
        public CropGrid()
        {
            base.Clip = this.RectangleGeometry;
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                this.RectangleGeometry.Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height);
            };
        }
    }

    public sealed class BackgroundCanvas : Canvas
    {
        public BackgroundCanvas()
        {
            for (int i = 0; i < 24; i++)
            {
                Rectangle rect = new Rectangle
                {
                    Height = TrackPanel.Spacing,
                };
                Canvas.SetTop(rect, i * 2 * TrackPanel.Spacing);
                base.Children.Add(rect);
            }

            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                if (e.NewSize.Width != e.PreviousSize.Width)
                {
                    foreach (FrameworkElement item in base.Children.Cast<FrameworkElement>())
                    {
                        item.Width = e.NewSize.Width;
                    }
                }
            };
        }
    }

    public sealed class LineCanvas : Canvas
    {
        public LineCanvas()
        {
            for (int i = 0; i < 16; i++)
            {
                double x = i * TrackPanel.Step;
                base.Children.Add(new Line
                {
                    Y1 = 0,
                    X1 = x,
                    X2 = x,
                });

                for (int j = 0; j < TrackPanel.StepCount; j++)
                {
                    double x2 = x + j * TrackPanel.StepSpacing;
                    base.Children.Add(new Line
                    {
                        Y1 = 0,
                        X1 = x2,
                        X2 = x2,
                    });
                }
            }
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                if (e.NewSize.Height != e.PreviousSize.Height)
                {
                    foreach (Line item in base.Children.Cast<Line>())
                    {
                        item.Y2 = e.NewSize.Height;
                    }
                }
            };
        }
    }

    public sealed class PointCanvas : Canvas
    {
        public PointCanvas()
        {
            for (int i = 0; i < 16; i++)
            {
                double x = i * TrackPanel.Step;
                base.Children.Add(new Line
                {
                    Y1 = 0,
                    X1 = x,
                    X2 = x,
                });

                for (int j = 0; j < TrackPanel.StepCount; j++)
                {
                    double x2 = x + j * TrackPanel.StepSpacing;
                    base.Children.Add(new Line
                    {
                        Y1 = 9,
                        X1 = x2,
                        X2 = x2,
                    });

                    for (int k = 0; k < TrackPanel.StepCount; k++)
                    {
                        double x3 = x2 + k * TrackPanel.StepSpacing2;
                        base.Children.Add(new Line
                        {
                            Y1 = 9 + 4,
                            X1 = x3,
                            X2 = x3,
                        });
                    }
                }
            }
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                if (e.NewSize.Height != e.PreviousSize.Height)
                {
                    foreach (Line item in base.Children)
                    {
                        item.Y2 = e.NewSize.Height;
                    }
                }
            };
        }
    }

    public sealed class TextCanvas : Canvas
    {
        private int offset;
        public int Offset
        {
            get => this.offset;
            set
            {
                if (this.offset == value) return;
                this.offset = value;

                for (int i = 0; i < base.Children.Count; i++)
                {
                    TextBlock item = base.Children[i] as TextBlock;
                    item.Text = $"{value + i}";
                }
            }
        }

        public TextCanvas()
        {
            for (int i = 0; i < 16; i++)
            {
                double x = i * TrackPanel.Step;

                TextBlock item = new TextBlock
                {
                    Text = $"{this.Offset + i}"
                };
                Canvas.SetLeft(item, x);
                base.Children.Add(item);
            }
        }
    }

    public sealed class ItemCanvas : ItemsControl
    {
        double X;
        double Y;
        UIElement Source;

        public ItemCanvas()
        {
            base.Width = TrackPanel.ExtentHeight;
            base.Height = TrackPanel.ExtentHeight;
            base.ManipulationStarted += (s, e) =>
            {
                this.Source = e.OriginalSource as UIElement;
                if (this.Source is null) return;

                this.X = Canvas.GetLeft(this.Source);
                this.Y = Canvas.GetTop(this.Source);
            };
            base.ManipulationDelta += (s, e) =>
            {
                // X
                double x = this.X + e.Delta.Translation.X;
                this.X = System.Math.Max(0, x);
                Canvas.SetLeft(this.Source, this.X);

                // Y
                double y = this.Y + e.Delta.Translation.Y;
                this.Y = System.Math.Max(0, y);
                int i = ((int)this.Y + 4) / TrackPanel.Spacing * TrackPanel.Spacing;
                Canvas.SetTop(this.Source, i);
            };
            base.ManipulationCompleted += (s, e) =>
            {
            };
        }
    }

    [ContentProperty(Name = nameof(Pane))]
    public sealed partial class TrackPanel : UserControl
    {
        //@Const
        public const int Scaling = 4;
        public const int Spacing = 21;

        public const int Step = 120;
        public const int StepCount = 4;

        public const int StepSpacing = Step / StepCount;
        public const int StepSpacing2 = StepSpacing / StepCount;

        // Timeline
        public int Time { get; private set; }
        private double Position;
        private double Timeline;

        // Container
        public double ViewportWidth { get; private set; }
        public double ViewportHeight { get; private set; }

        public double ExtentWidth { get; private set; } = TrackPanel.ExtentHeight;
        public const int ExtentHeight = NoteExtensions.NoteCount * TrackPanel.Spacing;

        // Content
        public double ScrollableWidth { get; private set; }
        public double ScrollableHeight { get; private set; }

        private double horizontalOffset = 0;
        public double HorizontalOffset
        {
            get => this.horizontalOffset;
            set
            {
                if (this.ScrollableWidth >= 0) return;
                value = System.Math.Clamp(value, this.ScrollableWidth, 0);

                if (this.horizontalOffset == value) return;
                this.horizontalOffset = value;

                this.TextCanvas.Offset = ((int)-this.horizontalOffset) / TrackPanel.Step;

                this.TranslateTransform.X = this.horizontalOffset;
                this.XTransform.X =
                this.XTransform2.X =
                this.XTransform3.X = this.horizontalOffset % TrackPanel.Step;
            }
        }

        private double verticalOffset = 0;
        public double VerticalOffset
        {
            get => this.verticalOffset;
            set
            {
                if (this.ScrollableHeight >= 0) return;
                value = System.Math.Clamp(value, this.ScrollableHeight, 0);

                if (this.verticalOffset == value) return;
                this.verticalOffset = value;

                this.TranslateTransform.Y = value;
                this.YTransform.Y = value % (TrackPanel.Spacing + TrackPanel.Spacing);
                this.YTransform2.Y = value;
            }
        }

        // UI
        public object ItemsSource { get => this.ItemsControl.ItemsSource; set => this.ItemsControl.ItemsSource = value; }
        public UIElement Pane { get => this.PaneBorder.Child; set => this.PaneBorder.Child = value; }

        public TrackPanel()
        {
            this.InitializeComponent();
            base.PointerWheelChanged += (s, e) =>
            {
                PointerPoint pp = e.GetCurrentPoint(this);
                int delta = pp.Properties.MouseWheelDelta;
                if (delta == 0) return;

                switch (e.KeyModifiers)
                {
                    case VirtualKeyModifiers.Control:
                    case VirtualKeyModifiers.Menu:
                    case VirtualKeyModifiers.Shift:
                    case VirtualKeyModifiers.Windows:
                        this.HorizontalOffset += delta;
                        this.UpdateTimeline();
                        break;
                    default:
                        if (pp.Properties.IsHorizontalMouseWheel)
                        {
                            this.HorizontalOffset -= delta;
                            this.UpdateTimeline();
                        }
                        else
                            this.VerticalOffset += delta / 2;
                        break;
                }
            };

            this.Thumb.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                this.ViewportWidth = e.NewSize.Width;
                this.ViewportHeight = e.NewSize.Height;

                if (e.NewSize.Width != e.PreviousSize.Width)
                {
                    this.ScrollableWidth = e.NewSize.Width - this.ExtentWidth;
                }

                if (e.NewSize.Height != e.PreviousSize.Height)
                {
                    this.Line.Y2 = e.NewSize.Height;
                    this.ScrollableHeight = e.NewSize.Height - TrackPanel.ExtentHeight;
                }
            };

            this.Thumb.DragStarted += (s, e) =>
            {
            };
            this.Thumb.DragDelta += (s, e) =>
            {
                this.HorizontalOffset += e.HorizontalChange;
                this.UpdateTimeline();
                this.VerticalOffset += e.VerticalChange;
            };
            this.Thumb.DragCompleted += (s, e) =>
            {
            };

            this.TimelineThumb.DragStarted += (s, e) =>
            {
                // this.Click(OptionType.Pause);
                this.Timeline = e.HorizontalOffset;

                // UI
                Canvas.SetLeft(this.Line, this.Timeline);
                Canvas.SetLeft(this.Polygon, this.Timeline);
            };
            this.TimelineThumb.DragDelta += (s, e) =>
            {
                this.Timeline += e.HorizontalChange;

                // UI
                Canvas.SetLeft(this.Line, this.Timeline);
                Canvas.SetLeft(this.Polygon, this.Timeline);
            };
            this.TimelineThumb.DragCompleted += (s, e) =>
            {
                // this.Click(OptionType.Play);
                this.Position = this.Timeline - this.HorizontalOffset;
                this.Time = (int)(this.Position * TrackPanel.Scaling);
            };
        }

        public void ChangeDuration(double time)
        {
            this.ExtentWidth = time / TrackPanel.Scaling;
            this.ScrollableWidth = this.Thumb.ActualWidth - this.ExtentWidth;
        }

        public void ChangePosition(double time)
        {
            this.Time = (int)time;
            this.Position = this.Time / TrackPanel.Scaling;
            this.Timeline = this.Position + this.HorizontalOffset;

            if (this.Timeline < 0)
            {
                this.HorizontalOffset = -this.Timeline;
                this.Timeline = 0;

                // UI
                this.Storyboard.Stop(); // Storyboard
                Canvas.SetLeft(this.Line, 0);
                Canvas.SetLeft(this.Polygon, 0);
            }
            else if (this.Timeline > this.ViewportWidth)
            {
                this.HorizontalOffset = -this.Time / TrackPanel.Scaling;
                this.Timeline = 0;

                // UI
                this.Storyboard.Stop(); // Storyboard
                Canvas.SetLeft(this.Line, 0);
                Canvas.SetLeft(this.Polygon, 0);
            }
            else
            {
                // UI
                this.LineAnimation.To = this.Timeline;
                this.PolygonAnimation.To = this.Timeline;
                this.Storyboard.Begin(); // Storyboard
            }
        }

        private void UpdateTimeline()
        {
            this.Timeline = this.Position + this.HorizontalOffset;

            // UI
            this.Storyboard.Stop(); // Storyboard
            Canvas.SetLeft(this.Line, this.Timeline);
            Canvas.SetLeft(this.Polygon, this.Timeline);
        }
    }
}