namespace Virtual_Piano.Midi.Core
{
    public interface IPianoPanel
    {
        double WhiteSize { get; set; }
        double BlackSize { get; set; }

        int ItemSize { get; set; }
        KeyLabel Label { get; set; }

        bool this[MidiNote note] { get; set; }

        void OnClick(MidiNote note);

        void Clear(MidiNote note);
        void Add(MidiNote note);
    }
}