using System;
using Windows.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Notes.Controls
{
    public sealed partial class VelocityGauge : UserControl
    {
        //@const
        const int Velocity = 127;
        const int VelocityHalf = Velocity / 2;

        const int MinTick = 2;
        const int MaxTick = 22;
        const int LengthTick = MaxTick - MinTick;

        const double Min = (MinTick + 6) * 15 * Math.PI / 180;
        const double Max = (MaxTick + 6) * 15 * Math.PI / 180;
        const double Length = Max - Min;

        static double ToRadians(double tick) => (tick + 6) * 15 * Math.PI / 180;
        static int ToIndex(double angle) => (int)((angle - Min) / Length * Velocity);
        static double ToTick(double index) => MinTick + 1d * LengthTick * index / Velocity;

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

                double a = ToRadians(ToTick(value));
                this.TextBlock.Text = $"{value}";
                this.Line.X1 = Math.Cos(a) * 20 + 22;
                this.Line.Y1 = Math.Sin(a) * 20 + 22;
            }
        }

        public VelocityGauge()
        {
            this.InitializeComponent();

            double a = ToRadians(ToTick(this.Index));
            this.TextBlock.Text = $"{this.Index}";
            this.Line.X1 = Math.Cos(a) * 20 + 22;
            this.Line.Y1 = Math.Sin(a) * 20 + 22;

            for (int i = MinTick; i <= MaxTick; i++)
            {
                double angle = ToRadians(i);
                double cos = Math.Cos(angle);
                double sin = Math.Sin(angle);
                this.Canvas.Children.Add(new Line
                {
                    X1 = cos * 20 + 20,
                    Y1 = sin * 20 + 20,
                    X2 = cos * 16 + 20,
                    Y2 = sin * 16 + 20,
                });
            }

            // Wheel
            base.PointerWheelChanged += (s, e) =>
            {
                PointerPoint pp = e.GetCurrentPoint(this);
                PointerPointProperties prop = pp.Properties;

                double delta = prop.MouseWheelDelta;
                int index = this.Index + System.Math.Clamp((int)-delta, -1, 1);
                this.Index = Math.Clamp(index, 0, Velocity);
            };

            // Thumb
            this.Thumb.DragStarted += (s, e) =>
            {
                X = e.HorizontalOffset;
                Y = e.VerticalOffset;

                double angle = Math.Atan2(this.Y - 22, this.X - 22);
                if (angle < Math.PI / 2 || angle > Math.PI) angle += Math.PI + Math.PI;
                angle = Math.Clamp(angle, Min, Max);

                int index = ToIndex(angle);
                this.Index = Math.Clamp(index, 0, Velocity);
            };
            this.Thumb.DragDelta += (s, e) =>
            {
                X += e.HorizontalChange;
                Y += e.VerticalChange;

                double angle = Math.Atan2(this.Y - 22, this.X - 22);
                if (angle < Math.PI / 2 || angle > Math.PI) angle += Math.PI + Math.PI;
                angle = Math.Clamp(angle, Min, Max);

                int index = ToIndex(angle);
                if (this.Index == 0 && index > VelocityHalf) return;
                if (this.Index == Velocity && index < VelocityHalf) return;
                this.Index = Math.Clamp(index, 0, Velocity);
            };
            this.Thumb.DragCompleted += (s, e) =>
            {
            };
        }
    }
}