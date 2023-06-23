using System;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Controls
{
    public sealed class BackgroundCanvas : Canvas
    {
        //@Delegate
        public event EventHandler<double> MoveX;
        public event EventHandler<double> MoveY;

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

    public sealed class ItemCanvas : Canvas
    {
        double X;
        double Y;
        UIElement Source;

        public ItemCanvas()
        {
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
                int i = ((int)this.Y + 16) / 21 * 21;
                Canvas.SetTop(this.Source, i);
            };
            base.ManipulationCompleted += (s, e) =>
            {
            };
        }
    }

    [ContentProperty(Name = nameof(Child))]
    public sealed partial class TrackPanel : UserControl
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public UIElement Child
        {
            get => this.ChildBorder.Child;
            set => this.ChildBorder.Child = value;
        }

        public TrackPanel()
        {
            this.InitializeComponent();

            this.RootGrid.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                this.RectangleGeometry.Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height);
            };
            this.RootGrid.PointerWheelChanged += (s, e) =>
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
                        this.UpdateX(delta);
                        break;
                    default:
                        if (pp.Properties.IsHorizontalMouseWheel)
                            this.UpdateX(-delta);
                        else
                            this.UpdateY(delta / 2);
                        break;
                }
            };

            this.Rectangle.ManipulationStarted += (s, e) =>
            {
            };
            this.Rectangle.ManipulationDelta += (s, e) =>
            {
                this.UpdateX(e.Delta.Translation.X);
                this.UpdateY(e.Delta.Translation.Y);
            };
            this.Rectangle.ManipulationCompleted += (s, e) =>
            {
            };
            this.UpdateY(-21 * 64);
        }

        private void UpdateX(double x)
        {
            this.X = System.Math.Min(0, this.X + x);
            this.XTransform.X = 100 + this.X % 100;
            this.TranslateTransform2.X = this.X;
        }

        private void UpdateY(double y)
        {
            const double MaxY = -21 * 128;
            this.Y = System.Math.Clamp(this.Y + y, MaxY + this.Rectangle.ActualHeight, 0);

            this.YTransform2.Y = this.Y;
            this.YTransform.Y = this.Y % (21 + 21);
            this.TranslateTransform2.Y = this.Y;
        }
    }
}