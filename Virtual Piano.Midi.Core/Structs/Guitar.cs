using System.Collections.Generic;
using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml.Data;

namespace Virtual_Piano.Midi.Core
{
    public readonly struct Guitar
    {
        public const int Count = 25;

        public readonly ItemIndexRange[] Fretboards;
        public readonly int Length;

        public Guitar(double scale, double space = 70, double length = 10, int a = 2, int b = 7, int c = 2)
        {
            this.Fretboards = new ItemIndexRange[Guitar.Count];

            for (int i = 0; i < Guitar.Count; i++)
            {
                double angle = System.Math.PI / 2 * (i / a + b) / (Guitar.Count + c);
                double index = space * (double)System.Math.Cos(angle);
                this.Fretboards[i] = new ItemIndexRange((int)(length * scale), (uint)(index * scale));

                length += index;
            }
            this.Length = (int)(length * scale);
        }

        public IEnumerable<int> Inlay1()
        {
            foreach (GuitarInlay1 item in System.Enum.GetValues(typeof(GuitarInlay1)))
            {
                int i = (int)item;
                yield return this.Fretboards[i].FirstIndex + (int)this.Fretboards[i].Length / 2;
            }
        }

        public IEnumerable<int> Inlay2()
        {
            foreach (GuitarInlay2 item in System.Enum.GetValues(typeof(GuitarInlay2)))
            {
                int i = (int)item;
                yield return this.Fretboards[i].FirstIndex + (int)this.Fretboards[i].Length / 2;
            }
        }
    }
}