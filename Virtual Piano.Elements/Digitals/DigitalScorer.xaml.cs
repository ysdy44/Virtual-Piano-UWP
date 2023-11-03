using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Elements
{
    public sealed partial class DigitalScorer : StackPanel
    {

        #region DependencyProperty

        /// <summary> Gets or set the Denominator for <see cref="DigitalScorer"/>. </summary>
        public int Denominator
        {
            get => (int)base.GetValue(DenominatorProperty);
            set => base.SetValue(DenominatorProperty, value);
        }
        /// <summary> Identifies the <see cref = "DigitalScorer.Denominator" /> dependency property. </summary>
        public static readonly DependencyProperty DenominatorProperty = DependencyProperty.Register(nameof(Denominator), typeof(int), typeof(DigitalScorer), new PropertyMetadata(0, (sender, e) =>
        {
            DigitalScorer control = (DigitalScorer)sender;

            if (e.NewValue is int value)
            {
                control.D.Update(value);
            }
        }));

        /// <summary> Gets or set the Numerator for <see cref="DigitalScorer"/>. </summary>
        public int Numerator
        {
            get => (int)base.GetValue(NumeratorProperty);
            set => base.SetValue(NumeratorProperty, value);
        }
        /// <summary> Identifies the <see cref = "DigitalScorer.Numerator" /> dependency property. </summary>
        public static readonly DependencyProperty NumeratorProperty = DependencyProperty.Register(nameof(Numerator), typeof(int), typeof(DigitalScorer), new PropertyMetadata(0, (sender, e) =>
        {
            DigitalScorer control = (DigitalScorer)sender;

            if (e.NewValue is int value)
            {
                control.N.Update(value);
            }
        }));

        #endregion

        readonly int W = DigitalExtensions.W;
        readonly int H = DigitalExtensions.H;

        readonly PathGeometry D = DigitalExtensions.Number();
        readonly PathGeometry N = DigitalExtensions.Number();

        public DigitalScorer()
        {
            this.InitializeComponent();
        }
    }
}