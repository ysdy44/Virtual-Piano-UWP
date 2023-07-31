namespace Virtual_Piano.Midi
{
    public readonly struct Tempo
    {
        public readonly int Bpm;
        public readonly int MillisecondsPerQuarterNote;
        public readonly int MicrosecondsPerQuarterNote;

        public Tempo(int bpm = 60)
        {
            this.Bpm = bpm;
            this.MillisecondsPerQuarterNote = 60 * 1000 / bpm;
            this.MicrosecondsPerQuarterNote = 60 * 1000 * 1000 / bpm;
        }

        public override string ToString() => $"Tempo {this.Bpm}bpm ({this.MicrosecondsPerQuarterNote})";
    }
}