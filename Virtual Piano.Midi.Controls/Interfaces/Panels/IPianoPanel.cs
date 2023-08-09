namespace Virtual_Piano.Midi.Controls
{
    public interface IPianoPanel
    {
        double WhiteSize { get; set; }
        double BlackSize { get; set; }

        int ItemSize { get; set; }
        KeyLabel Label { get; set; }

        void OnClick(MidiNote note);
    }
}