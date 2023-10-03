using System;
using Windows.Foundation;

namespace Virtual_Piano.Elements
{
    internal readonly struct RadarLine
    {
        public readonly Point P0;
        public readonly Point P1;
        public readonly Point P2;
        public readonly Point P3;
        public readonly Point P4;

        public RadarLine(double angle)
        {
            double r = angle / 180 * Math.PI;

            double sin = Math.Sin(r);
            double cos = Math.Cos(r);

            this.P0 = new Point(cos * 150, sin * 150);
            this.P1 = new Point(cos * 120, sin * 120);
            this.P2 = new Point(cos * 90, sin * 90);
            this.P3 = new Point(cos * 60, sin * 60);
            this.P4 = new Point(cos * 30, sin * 30);
        }

        public RadarLine(RadarLine line)
        {
            this.P0 = new Point(-line.P0.X, -line.P0.Y);
            this.P1 = new Point(-line.P1.X, -line.P1.Y);
            this.P2 = new Point(-line.P2.X, -line.P2.Y);
            this.P3 = new Point(-line.P3.X, -line.P3.Y);
            this.P4 = new Point(-line.P4.X, -line.P4.Y);
        }
    }
}