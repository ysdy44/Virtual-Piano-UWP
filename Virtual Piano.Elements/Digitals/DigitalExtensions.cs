using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Elements
{
    /// <summary>
    /// Digital-Digital Regular
    /// </summary>
    public static class DigitalExtensions
    {
        //@Const
        const int N0 = 0;
        const int N1 = 1;
        const int N2 = 2;
        const int N8 = 8;
        const int N9 = 9;
        const int N16 = 16;
        const double Padding = 0.4;

        public const int W = 10;
        public const int H = 18;

        public static PathGeometry Number() => new PathGeometry
        {
            Figures = new PathFigureCollection
            {
                DigitalExtensions.V(DigitalExtensions.N0, DigitalExtensions.N0, DigitalExtensions.Padding),
                DigitalExtensions.U(DigitalExtensions.N0, DigitalExtensions.N0, DigitalExtensions.Padding),
                DigitalExtensions.V(DigitalExtensions.N8, DigitalExtensions.N0, DigitalExtensions.Padding),
                DigitalExtensions.V(DigitalExtensions.N8, DigitalExtensions.N8, DigitalExtensions.Padding),
                DigitalExtensions.U(DigitalExtensions.N0, DigitalExtensions.N16, DigitalExtensions.Padding),
                DigitalExtensions.V(DigitalExtensions.N0, DigitalExtensions.N8, DigitalExtensions.Padding),
                DigitalExtensions.U(DigitalExtensions.N0, DigitalExtensions.N8, DigitalExtensions.Padding),
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
                        // new Point(DigitalExteions.N2+x+padding, N2+y),
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
                case '1': Update(path, DigitalType.N1); break;
                case '2': Update(path, DigitalType.N2); break;
                case '3': Update(path, DigitalType.N3); break;
                case '4': Update(path, DigitalType.N4); break;
                case '5': Update(path, DigitalType.N5); break;
                case '6': Update(path, DigitalType.N6); break;
                case '7': Update(path, DigitalType.N7); break;
                case '8': Update(path, DigitalType.N8); break;
                case '9': Update(path, DigitalType.N9); break;
                default: Update(path, DigitalType.N0); break;
            }
        }
        public static void Update(this PathGeometry path, int type)
        {
            switch (type)
            {
                case 1: Update(path, DigitalType.N1); break;
                case 2: Update(path, DigitalType.N2); break;
                case 3: Update(path, DigitalType.N3); break;
                case 4: Update(path, DigitalType.N4); break;
                case 5: Update(path, DigitalType.N5); break;
                case 6: Update(path, DigitalType.N6); break;
                case 7: Update(path, DigitalType.N7); break;
                case 8: Update(path, DigitalType.N8); break;
                case 9: Update(path, DigitalType.N9); break;
                default: Update(path, DigitalType.N0); break;
            }
        }
        internal static void Update(this PathGeometry path, DigitalType type)
        {
            path.Figures[0].IsFilled = type.HasFlag(DigitalType.Line0);
            path.Figures[1].IsFilled = type.HasFlag(DigitalType.Line1);
            path.Figures[2].IsFilled = type.HasFlag(DigitalType.Line2);
            path.Figures[3].IsFilled = type.HasFlag(DigitalType.Line3);
            path.Figures[4].IsFilled = type.HasFlag(DigitalType.Line4);
            path.Figures[5].IsFilled = type.HasFlag(DigitalType.Line5);
            path.Figures[6].IsFilled = type.HasFlag(DigitalType.Line6);
        }
    }
}