using Virtual_Piano.Midi.Core;

namespace Virtual_Piano.Midi.Controls
{
    public readonly struct PadLayout
    {
        public readonly PadLayoutMode Mode;

        public readonly int Width;
        public readonly int Height;

        public readonly int Column;
        public readonly int Row;

        public PadLayout(int width, int height)
        {
            int n = height > 0 ? 12 * width / height : 1;

            if (n < 2) this.Mode = PadLayoutMode.L1x12;
            else if (n < 7) this.Mode = PadLayoutMode.L2x6;
            else if (n < 12) this.Mode = PadLayoutMode.L3x4;
            else if (n < 26) this.Mode = PadLayoutMode.L4x3;
            else if (n < 54) this.Mode = PadLayoutMode.L6x2;
            else this.Mode = PadLayoutMode.L12x1;

            switch (this.Mode)
            {
                case PadLayoutMode.L1x12: this.Column = 1; break;
                case PadLayoutMode.L2x6: this.Column = 2; break;
                case PadLayoutMode.L3x4: this.Column = 3; break;
                case PadLayoutMode.L4x3: this.Column = 4; break;
                case PadLayoutMode.L6x2: this.Column = 6; break;
                case PadLayoutMode.L12x1: this.Column = 12; break;
                default: this.Column = 12; break;
            }

            switch (this.Mode)
            {
                case PadLayoutMode.L1x12: this.Row = 12; break;
                case PadLayoutMode.L2x6: this.Row = 6; break;
                case PadLayoutMode.L3x4: this.Row = 4; break;
                case PadLayoutMode.L4x3: this.Row = 3; break;
                case PadLayoutMode.L6x2: this.Row = 2; break;
                case PadLayoutMode.L12x1: this.Row = 1; break;
                default: this.Row = 1; break;
            }

            this.Width = width / this.Column;
            this.Height = height / this.Row;
        }
    }
}