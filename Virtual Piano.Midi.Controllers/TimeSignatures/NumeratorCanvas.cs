using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed class NumeratorCanvas : Canvas
    {
        //@Const
        public const int Maximum = 12;
        public const int Minimum = 1;
        private const int Min = Minimum;
        private const int Max = Maximum;

        public NumeratorCanvas()
        {
            base.Children.Add(new Line());
            for (int i = 1; i < Max; i++)
            {
                base.Children.Add(new Line
                {
                    Visibility = Visibility.Collapsed
                });
            }
            base.Children.Add(new Line());
        }

        public void Update(int numerator, double length = 400)
        {
            double space = length / numerator;

            for (int n = 1; n <= numerator; n++)
            {
                double x = n * space;
                if (base.Children[n] is Line line)
                {
                    line.X1 = x;
                    line.X2 = x;
                    line.Visibility = Visibility.Visible;
                }
            }

            for (int i = numerator + 1; i < Max; i++)
            {
                if (base.Children[i] is Line line)
                {
                    line.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}