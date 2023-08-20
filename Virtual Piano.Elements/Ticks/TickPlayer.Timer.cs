using System;
using Windows.UI.Xaml;

namespace Virtual_Piano.Elements
{
    partial class TickPlayer
    {
        //@Delegate
        public event EventHandler<string> TickBeat;
        public event EventHandler<object> TickProgress
        {
            remove => this.Timer.Tick -= value;
            add => this.Timer.Tick += value;
        }

        //@Const
        const string S0 = "/";
        const string S1 = "|";
        const string S2 = "\\";
        const string S3 = "—";

        int Index;
        int Step;

        public int Beat { get; set; } = 4;
        public string BeatChar
        {
            get
            {
                switch (this.Index)
                {
                    case 0: return TickPlayer.S0;
                    case 1: return TickPlayer.S1;
                    case 2: return TickPlayer.S2;
                    default: return TickPlayer.S3;
                }
            }
        }

        private readonly DispatcherTimer Timer;

        private void TickChar()
        {
            switch (this.Index)
            {
                case 0:
                    this.Index = 1;
                    this.TickBeat?.Invoke(this, TickPlayer.S0); // Delegate
                    break;
                case 1:
                    this.Index = 2;
                    this.TickBeat?.Invoke(this, TickPlayer.S1); // Delegate
                    break;
                case 2:
                    this.Index = 3;
                    this.TickBeat?.Invoke(this, TickPlayer.S2); // Delegate
                    break;
                default:
                    this.Index = 0;
                    this.TickBeat?.Invoke(this, TickPlayer.S3); // Delegate
                    break;
            }
        }
    }
}