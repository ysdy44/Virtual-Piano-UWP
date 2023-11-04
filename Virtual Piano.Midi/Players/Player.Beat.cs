using System;
using Windows.UI.Xaml;

namespace Virtual_Piano.Midi
{
    partial class Player
    {
        //@Delegate
        public event EventHandler<string> Beat;

        //@Const
        const string S0 = "/";
        const string S1 = "|";
        const string S2 = "\\";
        const string S3 = "—";

        int Index;
        int Step;

        public int BeatCount { get; set; } = 4;
        public string BeatChar
        {
            get
            {
                switch (this.Index)
                {
                    case 0: return Player.S0;
                    case 1: return Player.S1;
                    case 2: return Player.S2;
                    default: return Player.S3;
                }
            }
        }

        private void TickChar()
        {
            switch (this.Index)
            {
                case 0:
                    this.Index = 1;
                    this.Beat?.Invoke(this, Player.S0); // Delegate
                    break;
                case 1:
                    this.Index = 2;
                    this.Beat?.Invoke(this, Player.S1); // Delegate
                    break;
                case 2:
                    this.Index = 3;
                    this.Beat?.Invoke(this, Player.S2); // Delegate
                    break;
                default:
                    this.Index = 0;
                    this.Beat?.Invoke(this, Player.S3); // Delegate
                    break;
            }
        }
    }
}