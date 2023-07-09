using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Elements
{
    /// <summary>
    /// DS-Digital Regular
    /// </summary>
    public sealed class DSNumber : Canvas
    {

        const int N0 = 0;
        const int N1 = 1;
        const int N2 = 2;
        const int N8 = 8;
        const int N9 = 9;
        const int N16 = 16;
        const double Padding = 0.4;

        public const int W = 10;
        public const int H = 18;

        readonly Dictionary<DSType, int> Dictionary = new Dictionary<DSType, int>
        {
            [DSType.Line0] = 0,
            [DSType.Line1] = 1,
            [DSType.Line2] = 2,
            [DSType.Line3] = 3,
            [DSType.Line4] = 4,
            [DSType.Line5] = 5,
            [DSType.Line6] = 6,
        };

        public DSNumber()
        {
            base.Width = DSNumber.W;
            base.Height = DSNumber.H;

            base.Children.Add(DSNumber.V(DSNumber.N0, DSNumber.N0, DSNumber.Padding));
            base.Children.Add(DSNumber.U(DSNumber.N0, DSNumber.N0, DSNumber.Padding));
            base.Children.Add(DSNumber.V(DSNumber.N8, DSNumber.N0, DSNumber.Padding));
            base.Children.Add(DSNumber.V(DSNumber.N8, DSNumber.N8, DSNumber.Padding));
            base.Children.Add(DSNumber.U(DSNumber.N0, DSNumber.N16, DSNumber.Padding));
            base.Children.Add(DSNumber.V(DSNumber.N0, DSNumber.N8, DSNumber.Padding));
            base.Children.Add(DSNumber.U(DSNumber.N0, DSNumber.N8, DSNumber.Padding));
        }

        private static Polygon V(double x, double y, double padding) => new Polygon
        {
            Points =
            {
                new Point(N2+x, N2+y+padding),
                new Point(N1+x, N1+y+padding),
                new Point(N0+x, N2+y+padding),

                new Point(N0+x, N8+y-padding),
                new Point(N1+x, N9+y-padding),
                new Point(N2+x, N8+y-padding),
            }
        };
        private static Polygon U(double x, double y, double padding) => new Polygon
        {
            Points =
            {
                new Point(N2+x+padding, N2+y),
                new Point(N1+x+padding, N1+y),
                new Point(N2+x+padding, N0+y),

                new Point(N8+x-padding, N0+y),
                new Point(N9+x-padding, N1+y),
                new Point(N8+x-padding, N2+y),
            }
        };

        internal void Update(char type)
        {
            switch (type)
            {
                case '1': this.Update(DSType.N1); break;
                case '2': this.Update(DSType.N2); break;
                case '3': this.Update(DSType.N3); break;
                case '4': this.Update(DSType.N4); break;
                case '5': this.Update(DSType.N5); break;
                case '6': this.Update(DSType.N6); break;
                case '7': this.Update(DSType.N7); break;
                case '8': this.Update(DSType.N8); break;
                case '9': this.Update(DSType.N9); break;
                default: this.Update(DSType.N0); break;
            }
        }
        internal void Update(int type)
        {
            switch (type)
            {
                case 1: this.Update(DSType.N1); break;
                case 2: this.Update(DSType.N2); break;
                case 3: this.Update(DSType.N3); break;
                case 4: this.Update(DSType.N4); break;
                case 5: this.Update(DSType.N5); break;
                case 6: this.Update(DSType.N6); break;
                case 7: this.Update(DSType.N7); break;
                case 8: this.Update(DSType.N8); break;
                case 9: this.Update(DSType.N9); break;
                default: this.Update(DSType.N0); break;
            }
        }
        internal void Update(DSType type)
        {
            if (this.Children.Count == 0) return;
            foreach (var item in this.Dictionary)
            {
                if (this.Children[item.Value] is null) return;

                this.Children[item.Value].Visibility =
                type.HasFlag(item.Key) ?
                 Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}