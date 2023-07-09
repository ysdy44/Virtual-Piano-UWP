using System;
using Windows.UI.Input;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Notes.Controls
{
    public sealed partial class VelocityGauge : VelocityGaugeBase
    {
        protected override int S1 => 22;
        protected override int S2 => 20;
        protected override int S3 => 16;

        double X;
        double Y;

        public VelocityGauge()
        {
            this.InitializeComponent();
            base.Update(base.Index);

            foreach (Line item in base.Lines())
            {
                this.Canvas.Children.Add(item);
            }

            // Wheel
            this.Thumb.PointerWheelChanged += (s, e) =>
            {
                PointerPoint pp = e.GetCurrentPoint(this.Thumb);
                PointerPointProperties prop = pp.Properties;

                double delta = prop.MouseWheelDelta;
                int index = base.Index + System.Math.Clamp((int)-delta, -1, 1);
                base.Index = Math.Clamp(index, 0, VelocityGaugeBase.Velocity);
            };

            // Thumb
            this.Thumb.DragStarted += (s, e) =>
            {
                this.X = e.HorizontalOffset;
                this.Y = e.VerticalOffset;
                base.Position(this.X, this.Y, false);
            };
            this.Thumb.DragDelta += (s, e) =>
            {
                this.X += e.HorizontalChange;
                this.Y += e.VerticalChange;
                base.Position(this.X, this.Y, true);
            };
            this.Thumb.DragCompleted += (s, e) =>
            {
            };
        }

        protected override void Update(string value, double x, double y)
        {
            this.TextBlock.Text = value;
            this.Line.X1 = x;
            this.Line.Y1 = y;
        }
    }
}