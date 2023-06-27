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

    public sealed class ItemCanvas : Canvas
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
        double X;
        double Y;
        public double Position { get; private set; }

        // Container
        public double ViewportWidth { get; private set; }
        public double ViewportHeight { get; private set; }

        public const int ExtentHeight = NoteExtensions.NoteCount * TrackPanel.Spacing;

        // Content
        public double HorizontalOffset { get => this.ScrollViewer.HorizontalOffset; set => this.ScrollViewer.ChangeView(value, null, null, true); }
        public double VerticalOffset { get => this.ScrollViewer.VerticalOffset; set => this.ScrollViewer.ChangeView(null, value, null, true); }
        public Point Offset { get => new Point(this.ScrollViewer.HorizontalOffset, this.ScrollViewer.VerticalOffset); set => this.ScrollViewer.ChangeView(value.X, value.Y, null, true); }

        // UI
        public UIElementCollection Children => this.ItemCanvas.Children;
        public UIElement Pane { get => this.PaneBorder.Child; set => this.PaneBorder.Child = value; }
        public double Length { get => this.ItemCanvas.Width; set => this.ItemCanvas.Width = value; }

        public TrackPanel()
        {
            this.InitializeComponent();
            this.CenterGrid.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                this.ViewportWidth = e.NewSize.Width;
                this.ViewportHeight = e.NewSize.Height;

                if (e.NewSize.Height != e.PreviousSize.Height)
                {
                    this.Line.Y2 = e.NewSize.Height;
                }
            };

            this.ScrollViewer.ViewChanging += (s, e) =>
            {
                double y = e.FinalView.VerticalOffset;
                double x = e.FinalView.HorizontalOffset;

                this.YTransform.Y = -y % (TrackPanel.Spacing + TrackPanel.Spacing);
                this.YTransform2.Y = -y;

                this.XTransform.X =
                this.XTransform2.X =
                this.XTransform3.X = -x % TrackPanel.Step;

                this.TextCanvas.Offset = ((int)x) / TrackPanel.Step;

                this.X = this.Position - x;
                // UI
                this.Storyboard.Stop(); // Storyboard
                Canvas.SetLeft(this.Line, this.X);
                Canvas.SetLeft(this.Polygon, this.X);
            };
            this.ScrollViewer.PointerWheelChanged += (s, e) =>
            {
                PointerPoint pp = e.GetCurrentPoint(this);
                int delta = pp.Properties.MouseWheelDelta;
                if (delta == 0) return;

                switch (e.KeyModifiers)
                {
                    case VirtualKeyModifiers.Control:
                        double offset = this.ScrollViewer.HorizontalOffset;
                        offset -= delta;
                        offset -= delta;
                        this.ScrollViewer.ChangeView(offset, null, null);
                        break;
                    default:
                        break;
                }
            };

            this.LeftGrid.PointerWheelChanged += (s, e) =>
            {
                PointerPoint pp = e.GetCurrentPoint(this);
                int delta = pp.Properties.MouseWheelDelta;
                if (delta == 0) return;
                else if (delta > 0) this.VerticalOffset -= TrackPanel.Spacing;
                else this.VerticalOffset += TrackPanel.Spacing;
            };

            this.Thumb.DragStarted += (s, e) =>
            {
                this.X = e.HorizontalOffset;
                // UI
                Canvas.SetLeft(this.Line, this.X);
                Canvas.SetLeft(this.Polygon, this.X);
            };
            this.Thumb.DragDelta += (s, e) =>
            {
                this.X += e.HorizontalChange;
                // UI
                Canvas.SetLeft(this.Line, this.X);
                Canvas.SetLeft(this.Polygon, this.X);
            };
            this.Thumb.DragCompleted += (s, e) =>
            {
                this.Position = this.X + this.HorizontalOffset;
            };

            this.HorizontalOffset = -TrackPanel.Spacing * NoteExtensions.NoteCount / 2;
        }

        public void ChangePosition(double value) => this.ChangePosition(value, this.HorizontalOffset);
        public void ChangePosition(double value, double horizontalOffset)
        {
            this.Position = value;

            this.X = this.Position - horizontalOffset;
            if (this.X < 0)
            {
                this.HorizontalOffset = this.Position;
            }
            else if (this.X > this.ViewportWidth)
            {
                this.HorizontalOffset = this.Position;
            }
            else
            {
                // UI
                this.LineAnimation.To = this.X;
                this.PolygonAnimation.To = this.X;
                this.Storyboard.Begin(); // Storyboard
            }
        }
    }
}