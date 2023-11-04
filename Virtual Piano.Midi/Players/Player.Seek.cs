using System;
using System.Diagnostics;

namespace Virtual_Piano.Midi
{
    partial class Player
    {
        TimeSpan Delay;
        TimeSpan Elapsed => this.Stopwatch.Elapsed;
        public TimeSpan Position => this.Elapsed + this.Delay;

        long DelayMilliseconds;
        long ElapsedMilliseconds => this.Stopwatch.ElapsedMilliseconds;
        public long PositionMilliseconds => this.ElapsedMilliseconds + this.DelayMilliseconds;

        public void Seek(TimeSpan delay)
        {
            this.Delay = delay;
            this.DelayMilliseconds = (long)delay.TotalMilliseconds;
        }

        public void Seek(long delayMilliseconds)
        {
            this.Delay = TimeSpan.FromMilliseconds(delayMilliseconds);
            this.DelayMilliseconds = delayMilliseconds;
        }

        public void Seek(TimeSpan delay, long delayMilliseconds)
        {
            this.Delay = delay;
            this.DelayMilliseconds = delayMilliseconds;
        }
    }
}