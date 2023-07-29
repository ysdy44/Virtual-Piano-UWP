using System;
using Windows.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Midi.Controls
{
    public sealed partial class TickGauge : UserControl
    {
        //@Const
        const int S1 = 90;
        const int S2 = 80;
        const int S3 = 74;

        double X;
        double Y;

        private int index;
        public int Index
        {
            get => this.index;
            set
            {
                if (this.index == value) return;
                this.index = value;
                this.Update(value);
            }
        }

        public TickGauge()
        {
            this.InitializeComponent();
            this.Update(this.Index);

            for (int i = Radial.MinTick; i <= Radial.MaxTick; i++)
            {
                double angle = Radial.ToRadians(i);
                double cos = System.Math.Cos(angle);
                double sin = System.Math.Sin(angle);
                this.Canvas.Children.Add(new Line
                {
                    X1 = cos * S2 + S2,
                    Y1 = sin * S2 + S2,
                    X2 = cos * S3 + S2,
                    Y2 = sin * S3 + S2,
                });
            }

            // Wheel
            this.Thumb.PointerWheelChanged += (s, e) =>
            {
                PointerPoint pp = e.GetCurrentPoint(this.Thumb);
                PointerPointProperties prop = pp.Properties;

                double delta = prop.MouseWheelDelta;
                int index = this.Index + System.Math.Clamp((int)-delta, -1, 1);
                this.Index = System.Math.Clamp(index, 0, Radial.Velocity);

                e.Handled = true;
            };

            // Thumb
            this.Thumb.DragStarted += (s, e) =>
            {
                this.X = e.HorizontalOffset;
                this.Y = e.VerticalOffset;
                this.Index = Radial.ToIndex(this.Index, this.X, this.Y, false, S1);
            };
            this.Thumb.DragDelta += (s, e) =>
            {
                this.X += e.HorizontalChange;
                this.Y += e.VerticalChange;
                this.Index = Radial.ToIndex(this.Index, this.X, this.Y, true, S1);
            };
            this.Thumb.DragCompleted += (s, e) =>
            {
            };
        }

        private void Update(int value)
        {
            double a = Radial.ToRadians(Radial.ToTick(value));
            this.Update($"{value}", System.Math.Cos(a) * S2 + S1, Math.Sin(a) * S2 + S1);
        }
        private void Update(string value, double x, double y)
        {
            this.TextBlock.Text = value;
            this.Line.X1 = x;
            this.Line.Y1 = y;
        }
    }
}