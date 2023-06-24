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
                var rect = new Rectangle();
                Canvas.SetTop(rect, i * 2 * 21);
                base.Children.Add(rect);
            }

            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                if (e.NewSize.Width != e.PreviousSize.Width)
                {
                    foreach (FrameworkElement item in base.Children)
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
                double x = i * 100;
                base.Children.Add(new Line
                {
                    Y1 = 0,
                    X1 = x,
                    X2 = x,
                });
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

    public sealed class PointCanvas : Canvas
    {
        public PointCanvas()
        {
            for (int i = 0; i < 16; i++)
            {
                double x = i * 100;
                base.Children.Add(new Line
                {
                    Y1 = 0,
                    X1 = x,
                    X2 = x,
                });

                for (int j = 0; j < 5; j++)
                {
                    double x2 = x + j * 20;
                    base.Children.Add(new Line
                    {
                        Y1 = 10,
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
                double x = i * 100;

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
            base.Width =
            base.Height = NoteExtensions.NoteCount * 21;
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
                int i = ((int)this.Y + 4) / 21 * 21;
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
        double X;
        double Y;
        public double Position { get; private set; }
        public double ViewportWidth { get; private set; }
        public double ViewportHeight { get; private set; }

        public double HorizontalOffset { get => this.ScrollViewer.HorizontalOffset; set => this.ScrollViewer.ChangeView(value, null, null, true); }
        public double VerticalOffset { get => this.ScrollViewer.VerticalOffset; set => this.ScrollViewer.ChangeView(null, value, null, true); }
        public Point Offset { get => new Point(this.ScrollViewer.HorizontalOffset, this.ScrollViewer.VerticalOffset); set => this.ScrollViewer.ChangeView(value.X, value.Y, null, true); }

        public UIElementCollection Children => this.ItemCanvas.Children;
        public UIElement Pane { get => this.PaneBorder.Child; set => this.PaneBorder.Child = value; }
        public double Length { get => this.ItemCanvas.Width; set => this.ItemCanvas.Width = value; }

        public TrackPanel()
        {
            this.InitializeComponent();
            this.Canvas.SizeChanged += (s, e) =>
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
                var y = e.FinalView.VerticalOffset;
                var x = e.FinalView.HorizontalOffset;

                this.YTransform.Y = -y % (21 + 21);
                this.YTransform2.Y = -y;

                this.XTransform.X =
                this.XTransform2.X =
                this.XTransform3.X = -x % 100;

                this.TextCanvas.Offset = ((int)x) / 100;
                this.ChangePosition(this.Position, x);
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

            this.HorizontalOffset = -21 * 64;
        }

        public void ChangePosition(double value) => this.ChangePosition(value, this.HorizontalOffset);
        public void ChangePosition(double value, double horizontalOffset)
        {
            this.Position = value;

            double x = value - horizontalOffset;
            if (x < this.ViewportWidth)
                Canvas.SetLeft(this.Line, x);
            else
                this.HorizontalOffset = horizontalOffset + this.ViewportWidth;
        }
    }
}