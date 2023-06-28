using System.Linq;
using Virtual_Piano.Notes;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Controls
{
    [ContentProperty(Name = nameof(Pane))]
    public sealed partial class TrackPanel : Canvas
    {
        //@Const
        public const int Scaling = 4;
        public const int Spacing = 21;

        public const int Step = 120;
        public const int StepCount = 4;

        public const int StepSpacing = Step / StepCount;
        public const int StepSpacing2 = StepSpacing / StepCount;

        public double Left => 75;
        public double Top => 18;

        // Container
        private double viewportWidth;
        public double ViewportWidth
        {
            get => this.viewportWidth;
            private set
            {
                if (this.viewportWidth == value) return;
                this.viewportWidth = value;

                // Top
                this.PointCanvas.Width = value;
                this.TextCanvas.Width = value;

                // Center
                this.ScrollableWidth = value - this.ExtentWidth;

                this.Thumb.Width = value;

                this.BackgroundCanvas.Width = value;
                foreach (FrameworkElement item in this.BackgroundCanvas.Children.Cast<FrameworkElement>())
                {
                    item.Width = value;
                }

                this.LineCanvas.Width = value + TrackPanel.Step;
            }
        }

        private double viewportHeight;
        public double ViewportHeight
        {
            get => this.viewportHeight;
            private set
            {
                if (this.viewportHeight == value) return;
                this.viewportHeight = value;

                // Left
                this.PaneBorder.Height = value;

                // Center
                this.ScrollableHeight = value - this.ExtentHeight;

                this.Thumb.Height = value;
                this.BackgroundCanvas.Height = value + TrackPanel.StepSpacing + TrackPanel.StepSpacing;

                this.LineCanvas.Height = value;
                foreach (Line item in this.LineCanvas.Children.Cast<Line>())
                {
                    item.Y2 = value;
                }

                this.Line.Y2 = value;
            }
        }

        public double ExtentWidth { get => this.ItemsControl.Width; private set => this.ItemsControl.Width = value; }
        public double ExtentHeight => NoteExtensions.NoteCount * TrackPanel.Spacing;

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

                this.Offset = ((int)-value) / TrackPanel.Step;

                Canvas.SetLeft(this.ItemsControl, value + this.Left);
                double h = value % TrackPanel.Step + this.Left;
                Canvas.SetLeft(this.LineCanvas, h);
                Canvas.SetLeft(this.PointCanvas, h);
                Canvas.SetLeft(this.TextCanvas, h);
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

                Canvas.SetTop(this.ItemsControl, value + this.Top);
                Canvas.SetTop(this.BackgroundCanvas, value % (TrackPanel.Spacing + TrackPanel.Spacing) + this.Top);
                Canvas.SetTop(this.PaneBorder, value + this.Top);
            }
        }

        private int offset;
        public int Offset
        {
            get => this.offset;
            set
            {
                if (this.offset == value) return;
                this.offset = value;

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
        private double Position;
        private double Timeline;

        double X;
        double Y;
        UIElement Source;

        public TrackPanel()
        {
            this.InitializeComponent();

            // BackgroundCanvas
            for (int i = 0; i < 24; i++)
            {
                Rectangle rect = new Rectangle
                {
                    Height = TrackPanel.Spacing,
                };
                Canvas.SetTop(rect, i * 2 * TrackPanel.Spacing);
                this.BackgroundCanvas.Children.Add(rect);
            }

            // LineCanvas
            for (int i = 0; i < 16; i++)
            {
                double x = i * TrackPanel.Step;
                this.LineCanvas.Children.Add(new Line
                {
                    Y1 = 0,
                    X1 = x,
                    X2 = x,
                    //Y2 = ?
                });

                for (int j = 0; j < TrackPanel.StepCount; j++)
                {
                    double x2 = x + j * TrackPanel.StepSpacing;
                    this.LineCanvas.Children.Add(new Line
                    {
                        Y1 = 0,
                        X1 = x2,
                        X2 = x2,
                        //Y2 = ?
                    });
                }
            }

            // PointCanvas
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

                this.RectangleGeometry.Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height);
                this.ViewportWidth = e.NewSize.Width - this.Left;
                this.ViewportHeight = e.NewSize.Height - this.Top;

                // Timeline
                this.TimelineThumb.Width = e.NewSize.Width;
            };

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

            this.ItemsControl.ManipulationStarted += (s, e) =>
            {
                this.Source = e.OriginalSource as UIElement;
                if (this.Source is null) return;

                this.X = Canvas.GetLeft(this.Source);
                this.Y = Canvas.GetTop(this.Source);
            };
            this.ItemsControl.ManipulationDelta += (s, e) =>
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
            this.ItemsControl.ManipulationCompleted += (s, e) =>
            {
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
                this.Position = this.Timeline - this.Left - this.HorizontalOffset;
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
                Canvas.SetLeft(this.Line, this.Left);
                Canvas.SetLeft(this.Polygon, this.Left);
            }
            else if (this.Timeline > this.ViewportWidth)
            {
                this.HorizontalOffset = -this.Time / TrackPanel.Scaling;
                this.Timeline = 0;

                // UI
                this.Storyboard.Stop(); // Storyboard
                Canvas.SetLeft(this.Line, this.Left);
                Canvas.SetLeft(this.Polygon, this.Left);
            }
            else
            {
                // UI
                this.LineAnimation.To = this.Timeline + this.Left;
                this.PolygonAnimation.To = this.Timeline + this.Left;
                this.Storyboard.Begin(); // Storyboard
            }
        }

        private void UpdateTimeline()
        {
            this.Timeline = this.Position + this.HorizontalOffset;

            // UI
            this.Storyboard.Stop(); // Storyboard
            Canvas.SetLeft(this.Line, this.Timeline + this.Left);
            Canvas.SetLeft(this.Polygon, this.Timeline + this.Left);
        }
    }
}