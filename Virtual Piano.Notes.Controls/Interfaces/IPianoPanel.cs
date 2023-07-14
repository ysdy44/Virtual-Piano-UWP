namespace Virtual_Piano.Notes.Controls
{
    public interface IPianoPanel : INotePanel
    {
        double WhiteSize { get; set; }
        double BlackSize { get; set; }

        int ItemSize { get; set; }
        KeyLabel Label { get; set; }

        INoteButton this[Note item] { get; }

        void Clear(int index);
        void Add(int index);
        void Clear(Note note);
        void Add(Note note);
    }
}