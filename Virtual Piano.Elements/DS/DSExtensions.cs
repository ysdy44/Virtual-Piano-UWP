using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Elements
{
    /// <summary>
    /// DS-Digital Regular
    /// </summary>
    public sealed class DSNumber : Canvas
    {
        readonly PathGeometry Data = DSExtensions.Number();

        public DSNumber()
        {
            base.Width = DSExtensions.W;
            base.Height = DSExtensions.H;
            base.Children.Add(new PathIcon
            {
                Data = this.Data
            });
        }

        public void Update(char type) => this.Data.Update(type);
        public void Update(int type) => this.Data.Update(type);
        internal void Update(DSType type) => this.Data.Update(type);
    }

    public static class DSExtensions
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

        readonly static Dictionary<DSType, int> Dictionary = new Dictionary<DSType, int>
        {
            [DSType.Line0] = 0,
            [DSType.Line1] = 1,
            [DSType.Line2] = 2,
            [DSType.Line3] = 3,
            [DSType.Line4] = 4,
            [DSType.Line5] = 5,
            [DSType.Line6] = 6,
        };

        public static PathGeometry Number() => new PathGeometry
        {
            Figures = new PathFigureCollection
            {
                DSExtensions.V(DSExtensions.N0, DSExtensions.N0, DSExtensions.Padding),
                DSExtensions.U(DSExtensions.N0, DSExtensions.N0, DSExtensions.Padding),
                DSExtensions.V(DSExtensions.N8, DSExtensions.N0, DSExtensions.Padding),
                DSExtensions.V(DSExtensions.N8, DSExtensions.N8, DSExtensions.Padding),
                DSExtensions.U(DSExtensions.N0, DSExtensions.N16, DSExtensions.Padding),
                DSExtensions.V(DSExtensions.N0, DSExtensions.N8, DSExtensions.Padding),
                DSExtensions.U(DSExtensions.N0, DSExtensions.N8, DSExtensions.Padding),
            }
        };

        private static PathFigure V(double x, double y, double padding, bool isVisble = true) => new PathFigure
        {
            IsClosed = true,
            IsFilled = isVisble,
            StartPoint = new Point(N2 + x, N2 + y + padding),
            Segments =
            {
                new PolyLineSegment
                {
                    Points =
                    {
                        // new Point(N2+x, N2+y+padding),
                        new Point(N1+x, N1+y+padding),
                        new Point(N0+x, N2+y+padding),

                        new Point(N0+x, N8+y-padding),
                        new Point(N1+x, N9+y-padding),
                        new Point(N2+x, N8+y-padding),
                    }
                }
            }
        };
        private static PathFigure U(double x, double y, double padding, bool isVisble = true) => new PathFigure
        {
            IsClosed = true,
            IsFilled = isVisble,
            StartPoint = new Point(N2 + x + padding, N2 + y),
            Segments =
            {
                new PolyLineSegment
                {
                    Points =
                    {
                        // new Point(DSExteions.N2+x+padding, N2+y),
                        new Point(N1+x+padding, N1+y),
                        new Point(N2+x+padding, N0+y),

                        new Point(N8+x-padding, N0+y),
                        new Point(N9+x-padding, N1+y),
                        new Point(N8+x-padding, N2+y),
                    }
                }
            }
        };

        public static void Update(this PathGeometry path, char type)
        {
            switch (type)
            {
                case '1': Update(path, DSType.N1); break;
                case '2': Update(path, DSType.N2); break;
                case '3': Update(path, DSType.N3); break;
                case '4': Update(path, DSType.N4); break;
                case '5': Update(path, DSType.N5); break;
                case '6': Update(path, DSType.N6); break;
                case '7': Update(path, DSType.N7); break;
                case '8': Update(path, DSType.N8); break;
                case '9': Update(path, DSType.N9); break;
                default: Update(path, DSType.N0); break;
            }
        }
        public static void Update(this PathGeometry path, int type)
        {
            switch (type)
            {
                case 1: Update(path, DSType.N1); break;
                case 2: Update(path, DSType.N2); break;
                case 3: Update(path, DSType.N3); break;
                case 4: Update(path, DSType.N4); break;
                case 5: Update(path, DSType.N5); break;
                case 6: Update(path, DSType.N6); break;
                case 7: Update(path, DSType.N7); break;
                case 8: Update(path, DSType.N8); break;
                case 9: Update(path, DSType.N9); break;
                default: Update(path, DSType.N0); break;
            }
        }
        internal static void Update(this PathGeometry path, DSType type)
        {
            if (path.Figures.Count == 0) return;
            foreach (var item in Dictionary)
            {
                if (path.Figures[item.Value] is null) return;

                path.Figures[item.Value].IsFilled =
                    type.HasFlag(item.Key);
            }
        }
    }
}