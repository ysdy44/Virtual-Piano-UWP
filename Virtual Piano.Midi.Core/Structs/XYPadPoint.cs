namespace Virtual_Piano.Midi.Core
{
    public readonly struct XYPadPoint
    {
        public readonly int X;
        public readonly int Y;

        public readonly int Width;
        public readonly int Height;

        public readonly int Size;
        public readonly int Left;
        public readonly int Top;
        public readonly int Right;
        public readonly int Bottom;

        public readonly int LineX;
        public readonly int LineY;

        public readonly int EllipseX;
        public readonly int EllipseY;

        public XYPadPoint(int x = 64, int y = 64, int width = 424, int height = 424, int padding = 24)
        {
            this.X = x;
            this.Y = y;

            this.Width = width;
            this.Height = height;

            this.Size = System.Math.Min(width - padding, height - padding);
            this.Left = (width - this.Size) / 2;
            this.Top = (height - this.Size) / 2;
            this.Right = this.Left + this.Size;
            this.Bottom = this.Top + this.Size;

            this.LineX = this.Left + x * this.Size / Radial.Velocity;
            this.LineY = this.Top + y * this.Size / Radial.Velocity;

            this.EllipseX = this.LineX - padding / 2;
            this.EllipseY = this.LineY - padding / 2;
        }

        public int GetX(double positionX) => (int)System.Math.Clamp((positionX - this.Left) * Radial.Velocity / this.Size, 0, Radial.Velocity);
        public int GetY(double positionY) => (int)System.Math.Clamp((positionY - this.Top) * Radial.Velocity / this.Size, 0, Radial.Velocity);
    }
}