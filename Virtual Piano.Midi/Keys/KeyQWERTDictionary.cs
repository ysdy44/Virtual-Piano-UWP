using Windows.System;

namespace Virtual_Piano.Midi.Core
{
    public sealed class KeyQWERTDictionary : KeyDictionary, IKeyDictionary
    {
        const int N1 = (int)VirtualKey.Number1;
        const int N9 = (int)VirtualKey.Number9;
        const int N19 = N9 - N1 + 1;

        //@Construct
        public KeyQWERTDictionary(ToneType type) : base(type) { }

        //@Override
        public override VirtualKey CreateKey(int index)
        {
            if (index >= 0 && index < N19)
            {
                return (VirtualKey)(index + N1);
            }
            else if (index == N19)
            {
                return VirtualKey.Number0;
            }
            else
            {
                switch (index - N19 - 1)
                {
                    case 0: return VirtualKey.Q;
                    case 1: return VirtualKey.W;
                    case 2: return VirtualKey.E;
                    case 3: return VirtualKey.R;
                    case 4: return VirtualKey.T;
                    case 5: return VirtualKey.Y;
                    case 6: return VirtualKey.U;
                    case 7: return VirtualKey.I;
                    case 8: return VirtualKey.O;
                    case 9: return VirtualKey.P;

                    case 10: return VirtualKey.A;
                    case 11: return VirtualKey.S;
                    case 12: return VirtualKey.D;
                    case 13: return VirtualKey.F;
                    case 14: return VirtualKey.G;
                    case 15: return VirtualKey.H;
                    case 16: return VirtualKey.J;
                    case 17: return VirtualKey.K;
                    case 18: return VirtualKey.L;

                    case 19: return VirtualKey.Z;
                    case 20: return VirtualKey.X;
                    case 21: return VirtualKey.C;
                    case 22: return VirtualKey.V;
                    case 23: return VirtualKey.B;
                    case 24: return VirtualKey.N;
                    case 25: return VirtualKey.M;

                    default: return default;
                }
            }
        }
    }
}