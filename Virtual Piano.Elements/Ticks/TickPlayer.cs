using System;
using Windows.UI.Xaml;

namespace Virtual_Piano.Elements
{
    public sealed partial class TickPlayer : ITickPlayer
    {
        public TickPlayer() : this(TimeSpan.FromMilliseconds(25)) { }
        public TickPlayer(TimeSpan interval)
        {
            this.Timer = new DispatcherTimer
            {
                Interval = interval
            };
            this.Timer.Tick += (s, e) =>
            {
                this.Step++;

                if (this.Step % this.Beat == 0)
                {
                    this.TickChar();
                }
            };
        }
    }
}