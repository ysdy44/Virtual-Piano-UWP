namespace Virtual_Piano.Midi.Controls
{
    public interface IPianoPanel: INotePanel
    {
        double WhiteSize { get; set; }
        double BlackSize { get; set; }

        int ItemSize { get; set; }
        KeyLabel Label { get; set; }

        IPianoButton this[MidiNote item] { get; }

        void Clear(int index);
        void Add(int index);
        void Clear(MidiNote note);
        void Add(MidiNote note);
    }
}