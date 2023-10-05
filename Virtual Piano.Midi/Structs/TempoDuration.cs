using System.Runtime.CompilerServices;
using Windows.UI.Xaml;

namespace Virtual_Piano.Midi
{
    public readonly struct TempoDuration
    {
        public readonly long Source;
        private readonly double Percent;

        public TempoDuration(Tempo tempo, long duration = 1000 * 60)
        {
            this.Source = tempo.Scale(duration);
            this.Percent = tempo.ReverseScalePercent(duration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetPercent(long position) => this.Percent * position;

        public override string ToString() => $"Duration ({this.Source})Milliseconds";
    }
}