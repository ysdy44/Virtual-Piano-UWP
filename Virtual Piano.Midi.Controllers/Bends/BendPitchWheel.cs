using System;
using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.Devices.Midi;
using Windows.UI.Xaml;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed class BendPitchWheel : BendWheel, IControl
    {
        //@Command
        public ICommand Command { get; set; }
        public int Channel { get; set; }

        readonly DispatcherTimer Timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(25)
        };

        public BendPitchWheel() : base(64)
        {
            this.Timer.Tick += (s, e) =>
            {
                switch (this.Index)
                {
                    #region 0~63
                    case 0: this.Index = 8; break;
                    case 1: this.Index = 8; break;
                    case 2: this.Index = 9; break;
                    case 3: this.Index = 10; break;
                    case 4: this.Index = 10; break;
                    case 5: this.Index = 11; break;
                    case 6: this.Index = 12; break;
                    case 7: this.Index = 13; break;
                    case 8: this.Index = 14; break;
                    case 9: this.Index = 15; break;
                    case 10: this.Index = 15; break;
                    case 11: this.Index = 16; break;
                    case 12: this.Index = 17; break;
                    case 13: this.Index = 18; break;
                    case 14: this.Index = 19; break;
                    case 15: this.Index = 20; break;
                    case 16: this.Index = 21; break;
                    case 17: this.Index = 22; break;
                    case 18: this.Index = 22; break;
                    case 19: this.Index = 23; break;
                    case 20: this.Index = 24; break;
                    case 21: this.Index = 25; break;
                    case 22: this.Index = 26; break;
                    case 23: this.Index = 27; break;
                    case 24: this.Index = 28; break;
                    case 25: this.Index = 29; break;
                    case 26: this.Index = 30; break;
                    case 27: this.Index = 30; break;
                    case 28: this.Index = 31; break;
                    case 29: this.Index = 32; break;
                    case 30: this.Index = 33; break;
                    case 31: this.Index = 34; break;
                    case 32: this.Index = 35; break;
                    case 33: this.Index = 36; break;
                    case 34: this.Index = 37; break;
                    case 35: this.Index = 38; break;
                    case 36: this.Index = 39; break;
                    case 37: this.Index = 39; break;
                    case 38: this.Index = 41; break;
                    case 39: this.Index = 41; break;
                    case 40: this.Index = 42; break;
                    case 41: this.Index = 43; break;
                    case 42: this.Index = 44; break;
                    case 43: this.Index = 45; break;
                    case 44: this.Index = 46; break;
                    case 45: this.Index = 47; break;
                    case 46: this.Index = 48; break;
                    case 47: this.Index = 49; break;
                    case 48: this.Index = 50; break;
                    case 49: this.Index = 50; break;
                    case 50: this.Index = 51; break;
                    case 51: this.Index = 52; break;
                    case 52: this.Index = 53; break;
                    case 53: this.Index = 54; break;
                    case 54: this.Index = 55; break;
                    case 55: this.Index = 56; break;
                    case 56: this.Index = 57; break;
                    case 57: this.Index = 58; break;
                    case 58: this.Index = 59; break;
                    case 59: this.Index = 60; break;
                    case 60: this.Index = 61; break;
                    case 61: this.Index = 62; break;
                    case 62: this.Index = 63; break;
                    case 63: this.Index = 64; this.Timer.Stop(); break;
                    #endregion
                    case 64: this.Timer.Stop(); break;
                    #region 65~126
                    case 65: this.Index = 64; this.Timer.Stop(); break;
                    case 66: this.Index = 65; break;
                    case 67: this.Index = 66; break;
                    case 68: this.Index = 67; break;
                    case 69: this.Index = 68; break;
                    case 70: this.Index = 69; break;
                    case 71: this.Index = 70; break;
                    case 72: this.Index = 71; break;
                    case 73: this.Index = 72; break;
                    case 74: this.Index = 73; break;
                    case 75: this.Index = 74; break;
                    case 76: this.Index = 75; break;
                    case 77: this.Index = 76; break;
                    case 78: this.Index = 77; break;
                    case 79: this.Index = 78; break;
                    case 80: this.Index = 78; break;
                    case 81: this.Index = 79; break;
                    case 82: this.Index = 80; break;
                    case 83: this.Index = 81; break;
                    case 84: this.Index = 82; break;
                    case 85: this.Index = 83; break;
                    case 86: this.Index = 84; break;
                    case 87: this.Index = 85; break;
                    case 88: this.Index = 86; break;
                    case 89: this.Index = 87; break;
                    case 90: this.Index = 87; break;
                    case 91: this.Index = 89; break;
                    case 92: this.Index = 89; break;
                    case 93: this.Index = 90; break;
                    case 94: this.Index = 91; break;
                    case 95: this.Index = 92; break;
                    case 96: this.Index = 93; break;
                    case 97: this.Index = 94; break;
                    case 98: this.Index = 95; break;
                    case 99: this.Index = 96; break;
                    case 100: this.Index = 97; break;
                    case 101: this.Index = 98; break;
                    case 102: this.Index = 98; break;
                    case 103: this.Index = 99; break;
                    case 104: this.Index = 100; break;
                    case 105: this.Index = 101; break;
                    case 106: this.Index = 102; break;
                    case 107: this.Index = 103; break;
                    case 108: this.Index = 104; break;
                    case 109: this.Index = 105; break;
                    case 110: this.Index = 106; break;
                    case 111: this.Index = 106; break;
                    case 112: this.Index = 107; break;
                    case 113: this.Index = 108; break;
                    case 114: this.Index = 109; break;
                    case 115: this.Index = 110; break;
                    case 116: this.Index = 111; break;
                    case 117: this.Index = 112; break;
                    case 118: this.Index = 113; break;
                    case 119: this.Index = 113; break;
                    case 120: this.Index = 114; break;
                    case 121: this.Index = 115; break;
                    case 122: this.Index = 116; break;
                    case 123: this.Index = 117; break;
                    case 124: this.Index = 118; break;
                    case 125: this.Index = 118; break;
                    case 126: this.Index = 119; break;
                    #endregion
                    case 127: this.Index = 119; break;
                    default: this.Timer.Stop(); break;
                }
            };
        }

        public override void Start() => this.Timer.Start();
        public override void Stop() => this.Timer.Stop();
        public override void Execute(int value)
        {
            this.Command?.Execute(new MidiMessage
            {
                Type = MidiMessageType.PitchBendChange,
                Channel = (byte)this.Channel,
                Bend = (ushort)(value * 128)
            }); // Command
        }
    }
}