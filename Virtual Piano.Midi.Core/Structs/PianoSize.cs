namespace Virtual_Piano.Midi.Core
{
    public readonly struct PianoSize
    {
        public readonly int ItemSize;

        public readonly int WhiteSize;
        public readonly int BlackSize;
        public readonly int BlackSizeHalf;

        public PianoSize(int value)
        {
            this.ItemSize = value;

            this.WhiteSize = value * NoteExtensions.ToneCount / NoteExtensions.WhiteCount;
            this.BlackSize = this.WhiteSize * NoteExtensions.BlackCount / NoteExtensions.WhiteCount;
            this.BlackSizeHalf = this.BlackSize << 1;
            this.BlackSizeHalf--;
        }
    }
}