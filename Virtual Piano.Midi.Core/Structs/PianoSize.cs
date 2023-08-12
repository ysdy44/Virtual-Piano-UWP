namespace Virtual_Piano.Midi.Core
{
    public readonly struct PianoSize
    {
        public readonly int ItemSize;

        public readonly int WhiteSize;
        public readonly int BlackSize;
        public readonly int BlackSizeHalf;

        public readonly int Length;

        public PianoSize(int value)
        {
            if (value < 4) value = 4;

            this.ItemSize = value;

            this.WhiteSize = value * NoteExtensions.ToneCount / NoteExtensions.WhiteCount;
            this.BlackSize = this.WhiteSize * NoteExtensions.BlackCount / NoteExtensions.WhiteCount;
            this.BlackSizeHalf = this.BlackSize << 1;
            this.BlackSizeHalf--;

            this.Length = NoteExtensions.NoteWhiteCount * this.WhiteSize;
        }

        public int ToWhite(int index) => index * this.WhiteSize;
        public int ToBlack(int index) => (index + 1) * this.WhiteSize - this.BlackSizeHalf;

        public int ReverseToWhite(int index) => (NoteExtensions.NoteWhiteCount - index - 1) * this.WhiteSize;
        public int ReverseToBlack(int index) => (NoteExtensions.NoteWhiteCount - index + 1) * this.WhiteSize - this.BlackSizeHalf;
    }
}