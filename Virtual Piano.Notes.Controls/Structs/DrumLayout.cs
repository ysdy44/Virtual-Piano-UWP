namespace Virtual_Piano.Notes.Controls
{
    public readonly struct DrumLayout
    {
        public readonly int Width;
        public readonly int Height;

        public readonly int Column;
        public readonly int Row;

        public DrumLayout(int width, int height)
        {
            int n = 12 * width / height;

            // 1x12: n = 12 * 1 / 12 = 1 (n<2)
            // 2x6: n = 12 * 2 / 6 = 4 (n<7)
            // 3x4: n = 12 * 3 / 4 = 9 (n<12)
            // 4x3: n = 12 * 4 / 3 = 16 (n<26)
            // 6x2: n = 12 * 6 / 2 = 36 (n<54)
            // 12x1: n = 12 * 12 / 1 = 144
            if (n < 2) { this.Column = 1; this.Row = 12; }
            else if (n < 7) { this.Column = 2; this.Row = 6; }
            else if (n < 12) { this.Column = 3; this.Row = 4; }
            else if (n < 26) { this.Column = 4; this.Row = 3; }
            else if (n < 54) { this.Column = 6; this.Row = 2; }
            else { this.Column = 12; this.Row = 1; }

            this.Width = width / this.Column;
            this.Height = height / this.Row;
        }
    }
}