namespace Virtual_Piano.Notes.Controls
{
    public readonly struct NoteSize
    {
        public readonly int ItemSize;

        public readonly int WhiteSize;
        public readonly int BlackSize;
        public readonly int BlackSizeHalf;

        public NoteSize(int value)
        {
            this.ItemSize = value;

            this.WhiteSize = value * NoteExtensions.ToneCount / NoteExtensions.WhiteCount;
            this.BlackSize = WhiteSize * NoteExtensions.BlackCount / NoteExtensions.WhiteCount;
            this.BlackSizeHalf = BlackSize << 1;
            this.BlackSizeHalf--;
        }
    }
}