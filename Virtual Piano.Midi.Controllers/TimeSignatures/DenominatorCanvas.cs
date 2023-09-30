using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed class DenominatorCanvas : Canvas
    {
        //@Const
        public const int Maximum = 12;
        public const int Minimum = 1;
        private const int Max = NumeratorCanvas.Maximum * (Maximum - 1);
        private const int Min = NumeratorCanvas.Minimum * (Minimum - 1);

        public DenominatorCanvas()
        {
            for (int i = 0; i < Max; i++)
            {
                base.Children.Add(new Line
                {
                    Visibility = Visibility.Collapsed
                });
            }
        }

        public void Update(int numerator, int denominator, double length = 400)
        {
            double space = length / numerator;
            double space2 = space / (denominator);

            for (int n = 0; n < numerator; n++)
            {
                double y = n * space;

                for (int d = 0; d < (denominator - 1); d++)
                {
                    int i = n * (denominator - 1) + d;

                    double x = y + (space2 * (d + 1));
                    if (base.Children[i] is Line line)
                    {
                        line.X1 = x;
                        line.X2 = x;
                        line.Visibility = Visibility.Visible;
                    }
                }
            }

            for (int i = numerator * (denominator - 1); i < Max; i++)
            {
                if (base.Children[i] is Line line)
                {
                    line.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}