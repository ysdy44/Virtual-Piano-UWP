namespace Virtual_Piano.Midi.Core
{
    public sealed class InstrumentItem
    {
        public MidiProgram Key { get; set; }
        public string Text { get; set; }
        public override string ToString() => $"{(int)this.Key}. {this.Text}";
    }
}