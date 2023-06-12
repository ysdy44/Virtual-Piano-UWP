namespace Virtual_Piano.Notes.Controls
{
    public struct NoteSize
    {
        public int Width;
        public int ItemWidth;

        public int WhiteWidth;
        public int BlackWidth;
        public int BlackWidthHalf;

        public NoteSize(int value)
        {
            this.Width = value * NoteExtensions.ToneCount * NoteExtensions.OctaveCount;
            this.ItemWidth = value;

            this.WhiteWidth = value * NoteExtensions.ToneCount / NoteExtensions.WhiteCount;
            this.BlackWidth = WhiteWidth * NoteExtensions.BlackCount / NoteExtensions.WhiteCount;
            this.BlackWidthHalf = BlackWidth << 1;
            this.BlackWidthHalf--;
        }
    }
}