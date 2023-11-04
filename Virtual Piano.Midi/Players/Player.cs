using System;
using System.Diagnostics;
using Windows.UI.Xaml;

namespace Virtual_Piano.Midi
{
    public sealed partial class Player : IPlayer
    {
        //@Delegate
        public event EventHandler<object> Tick
        {
            remove => this.Timer.Tick -= value;
            add => this.Timer.Tick += value;
        }

        public bool IsPlaying => this.Stopwatch.IsRunning;
        private readonly Stopwatch Stopwatch = new Stopwatch();
        private readonly DispatcherTimer Timer;

        public Player() : this(TimeSpan.FromMilliseconds(25)) { }
        public Player(TimeSpan interval)
        {
            this.Timer = new DispatcherTimer
            {
                Interval = interval
            };
            this.Timer.Tick += delegate
            {
                this.Step++;

                if (this.Step % this.BeatCount == 0)
                {
                    this.TickChar();
                }
            };
        }

        public void Play()
        {
            this.Stopwatch.Restart();
            this.Timer.Start();
            this.StatePlay();
        }

        public void Pause()
        {
            this.Delay += this.Elapsed;
            this.DelayMilliseconds += this.ElapsedMilliseconds;

            this.Stopwatch.Stop();
            this.Timer.Stop();
            this.StatePause();
        }

        public void Reset()
        {
            this.Delay = TimeSpan.Zero;
            this.DelayMilliseconds = 0;

            this.Stopwatch.Stop();
            this.Timer.Stop();
            this.StateReset();
        }
    }
}