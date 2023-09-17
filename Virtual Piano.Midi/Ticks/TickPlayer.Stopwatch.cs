﻿using System;
using System.Diagnostics;

namespace Virtual_Piano.Midi
{
    partial class TickPlayer
    {
        TimeSpan Delay;
        TimeSpan Elapsed => this.Stopwatch.Elapsed;
        public TimeSpan Position => this.Elapsed + this.Delay;

        long DelayMilliseconds;
        long ElapsedMilliseconds => this.Stopwatch.ElapsedMilliseconds;
        public long PositionMilliseconds => this.ElapsedMilliseconds + this.DelayMilliseconds;

        public bool IsPlaying => this.Stopwatch.IsRunning;
        private readonly Stopwatch Stopwatch = new Stopwatch();


        public void Seek(TimeSpan timeSpan)
        {
            this.Delay = timeSpan;
            this.DelayMilliseconds = (long)timeSpan.TotalMilliseconds;
        }

        public void Seek(long timeSpan)
        {
            this.Delay = TimeSpan.FromMilliseconds(timeSpan);
            this.DelayMilliseconds = (long)timeSpan;
        }


        public void Play()
        {
            this.Stopwatch.Restart();
            this.Timer.Start();
        }

        public void Pause()
        {
            this.Delay += this.Elapsed;
            this.DelayMilliseconds += this.ElapsedMilliseconds;

            this.Stopwatch.Stop();
            this.Timer.Stop();
        }

        public void Stop()
        {
            this.Delay = TimeSpan.Zero;
            this.DelayMilliseconds = 0;

            this.Stopwatch.Stop();
            this.Timer.Stop();
        }
    }
}