namespace Virtual_Piano.Midi
{
    public readonly struct TempoDuration
    {
        public readonly long Source;
        private readonly double Percent;

        public TempoDuration(Tempo tempo, int duration = 1000 * 60)
        {
            this.Source = tempo.Scale(duration);
            this.Percent = 100d * tempo.Bpm / 120d / duration;
        }

        public double GetPercent(long position) => this.Percent * position;

        public override string ToString() => $"Duration ({this.Source})Milliseconds";
    }
}