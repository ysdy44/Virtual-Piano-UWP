using Windows.System;

namespace Virtual_Piano.Midi.Core
{
    public sealed class KeyABCDictionary : KeyDictionary, IKeyDictionary
    {
        const int N1 = (int)VirtualKey.Number1;
        const int N9 = (int)VirtualKey.Number9;
        const int N19 = N9 - N1 + 1;

        const int A = (int)VirtualKey.A;
        const int Z = (int)VirtualKey.Z;
        const int AZ = Z - A + 1;

        const int N19AZ = N19 + AZ;

        //@Construct
        public KeyABCDictionary(ToneType type) : base(type) { }

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
            else if (index > N19 && index < N19AZ)
            {
                return (VirtualKey)(index - N19 - 1 + A);
            }
            else
            {
                return default;
            }
        }
    }
}