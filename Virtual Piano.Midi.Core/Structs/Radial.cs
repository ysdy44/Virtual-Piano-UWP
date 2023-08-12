using Windows.Foundation;

namespace Virtual_Piano.Midi.Core
{
    public readonly struct Radial
    {
        //@const
        public const int Velocity = 127;
        public const int VelocityHalf = Velocity / 2;
        public static int ToIndex(double radians) => (int)((radians - Min) / Length * Velocity);

        public const int MinTick = 2;
        public const int MaxTick = 22;
        const int LengthTick = MaxTick - MinTick;
        const int Large = (MaxTick + MinTick) / 2 + MinTick;
        public static double ToTick(double velocity) => velocity / Radial.Velocity * Radial.LengthTick + Radial.MinTick;

        public const double Min = (Radial.MinTick + 6) * 15 * System.Math.PI / 180;
        public const double Max = (Radial.MaxTick + 6) * 15 * System.Math.PI / 180;
        const double Length = Max - Min;
        public static double ToRadians(double tick) => (tick + 6) * 15 * System.Math.PI / 180;

        public readonly double X1;
        public readonly double X2;
        public readonly double Y1;
        public readonly double Y2;
        public readonly bool IsLargeArc;
        public readonly Point Point;

        public Radial(double velocity, double radius1 = 60, double radius2 = 48, double radius3 = 20)
        {
            double tick = Radial.ToTick(velocity);
            this.IsLargeArc = tick > Radial.Large;

            double radians = Radial.ToRadians(tick);
            double cos = System.Math.Cos(radians);
            double sin = System.Math.Sin(radians);

            this.X1 = cos * radius2 + radius1;
            this.Y1 = sin * radius2 + radius1;
            this.X2 = cos * radius3 + radius1;
            this.Y2 = sin * radius3 + radius1;

            this.Point = new Point
            {
                X = cos * radius1 + radius1,
                Y = sin * radius1 + radius1,
            };
        }

        public static Point StartPoint(double radius = 60) => Radial.Create(Radial.MinTick, radius);
        public static Point EndPoint(double radius = 60) => Radial.Create(Radial.MaxTick, radius);
        private static Point Create(double tick, double radius = 60)
        {
            double radians = Radial.ToRadians(tick);
            double cos = System.Math.Cos(radians);
            double sin = System.Math.Sin(radians);

            return new Point
            {
                X = cos * radius + radius,
                Y = sin * radius + radius,
            };
        }

        public static int ToIndex(int index, double x, double y, bool disableFlip, double radius = 60)
        {
            double angle = System.Math.Atan2(y - radius, x - radius);
            if (angle < System.Math.PI / 2 || angle > System.Math.PI) angle += System.Math.PI + System.Math.PI;
            angle = System.Math.Clamp(angle, Radial.Min, Radial.Max);

            int i = Radial.ToIndex(angle);
            if (disableFlip)
            {
                if (index == 0 && i > Radial.VelocityHalf) return index;
                if (index == Radial.Velocity && i < Radial.VelocityHalf) return index;
                return System.Math.Clamp(i, 0, Radial.Velocity);
            }
            return System.Math.Clamp(i, 0, Radial.Velocity);
        }
    }
}