using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Notes.Controls
{
    public abstract class VelocityGaugeBase : UserControl
    {
        //@const
        public const int Velocity = 127;
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

        protected abstract int S1 { get; }
        protected abstract int S2 { get; }
        protected abstract int S3 { get; }

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

        protected abstract void Update(string value, double x, double y);
        protected void Update(int value)
        {
            double a = ToRadians(ToTick(value));
            this.Update($"{value}", Math.Cos(a) * this.S2 + this.S1, Math.Sin(a) * this.S2 + this.S1);
        }

        protected IEnumerable<Line> Lines()
        {
            for (int i = MinTick; i <= MaxTick; i++)
            {
                double angle = ToRadians(i);
                double cos = Math.Cos(angle);
                double sin = Math.Sin(angle);
                yield return new Line
                {
                    X1 = cos * this.S2 + this.S2,
                    Y1 = sin * this.S2 + this.S2,
                    X2 = cos * this.S3 + this.S2,
                    Y2 = sin * this.S3 + this.S2,
                };
            }
        }

        protected void Position(double x, double y, bool disableFlip)
        {
            double angle = Math.Atan2(y - this.S1, x - this.S1);
            if (angle < Math.PI / 2 || angle > Math.PI) angle += Math.PI + Math.PI;
            angle = Math.Clamp(angle, Min, Max);

            int index = ToIndex(angle);
            if (disableFlip)
            {
                if (this.Index == 0 && index > VelocityHalf) return;
                if (this.Index == Velocity && index < VelocityHalf) return;
                this.Index = Math.Clamp(index, 0, Velocity);
            }
            this.Index = Math.Clamp(index, 0, Velocity);
        }
    }
}