using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed partial class TimeSignaturesPanel : Canvas
    {
        //@Const
        public const int MaxNumerator = 12;
        public const int MinNumerator = 1;
        
        public const int MaxDenominator = 8;
        public const int MinDenominator = 2;

        private const int Max = TimeSignaturesPanel.MaxNumerator * (TimeSignaturesPanel.MaxDenominator - 1);
        private const int Min = TimeSignaturesPanel.MinNumerator * (TimeSignaturesPanel.MinDenominator - 1);

        public TimeSignaturesPanel()
        {
            this.InitializeComponent();

            for (int i = 1; i < TimeSignaturesPanel.MaxNumerator; i++)
            {
                this.NumeratorCanvas.Children.Add(new Line
                {
                    Visibility = Visibility.Collapsed
                });
            }

            for (int i = 0; i < TimeSignaturesPanel.Max; i++)
            {
                this.DenominatorCanvas.Children.Add(new Line
                {
                    Visibility = Visibility.Collapsed
                });
            }
        }

        public void Update(TimeSignature timeSignature, double w)
        {
            this.Line.X1 = w;
            this.Line.X2 = w;
            this.UpdateNumerator(timeSignature.Numerator, w);
            this.UpdateDenominator(timeSignature.Numerator, timeSignature.Denominator, w);
        }

        public void UpdateNumerator(TimeSignature timeSignature)
        {
            this.UpdateNumerator(timeSignature.Numerator, this.ActualWidth);
            this.UpdateDenominator(timeSignature.Numerator, timeSignature.Denominator, base.ActualWidth);
        }

        public void UpdateDenominator(TimeSignature timeSignature)
        {
            this.UpdateDenominator(timeSignature.Numerator, timeSignature.Denominator, base.ActualWidth);
        }

        private void UpdateNumerator(int numerator, double length)
        {
            double space = length / numerator;

            for (int n = 0; n < numerator - 1; n++)
            {
                double x = (n + 1) * space;
                var aaa = this.NumeratorCanvas.Children[n];
                if (aaa is Line line)
                {
                    line.X1 = x;
                    line.X2 = x;
                    line.Visibility = Visibility.Visible;
                }
            }

            for (int i = numerator - 1; i < TimeSignaturesPanel.MaxNumerator; i++)
            {
                if (this.NumeratorCanvas.Children[i] is Line line)
                {
                    line.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void UpdateDenominator(int numerator, int denominator, double length)
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
                    if (this.DenominatorCanvas.Children[i] is Line line)
                    {
                        line.X1 = x;
                        line.X2 = x;
                        line.Visibility = Visibility.Visible;
                    }
                }
            }

            for (int i = numerator * (denominator - 1); i < TimeSignaturesPanel.Max; i++)
            {
                if (this.DenominatorCanvas.Children[i] is Line line)
                {
                    line.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}