using System.Runtime.CompilerServices;
using Windows.UI.Xaml;

namespace Virtual_Piano.Midi
{
    public readonly struct TempoDuration
    {
        public readonly long Duration;
        public readonly long DurationMilliseconds;
        private readonly double Percent;

        public TempoDuration(Tempo tempo, long duration = 1000 * 60)
        {
            this.Duration = duration;
            this.DurationMilliseconds = tempo.ToMilliseconds(duration);
            this.Percent = tempo.ToTicksPercent(duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetPercent(long milliseconds) => this.Percent * milliseconds;

        public override string ToString() => $"Duration ({this.DurationMilliseconds})Milliseconds";
    }
}