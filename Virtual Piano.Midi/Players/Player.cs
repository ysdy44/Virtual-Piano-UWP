using System;
using Windows.UI.Xaml;

namespace Virtual_Piano.Midi
{
    public sealed partial class Player : IPlayer
    {
        public Player() : this(TimeSpan.FromMilliseconds(25)) { }
        public Player(TimeSpan interval)
        {
            this.Timer = new DispatcherTimer
            {
                Interval = interval
            };
            this.Timer.Tick += (s, e) =>
            {
                this.Step++;

                if (this.Step % this.BeatCount == 0)
                {
                    this.TickChar();
                }
            };
        }
    }
}