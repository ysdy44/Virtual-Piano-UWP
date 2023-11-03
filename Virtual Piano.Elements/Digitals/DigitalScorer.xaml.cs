using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Elements
{
    public sealed partial class DSScorer : StackPanel
    {

        #region DependencyProperty

        /// <summary> Gets or set the Denominator for <see cref="DSScorer"/>. </summary>
        public int Denominator
        {
            get => (int)base.GetValue(DenominatorProperty);
            set => base.SetValue(DenominatorProperty, value);
        }
        /// <summary> Identifies the <see cref = "DSScorer.Denominator" /> dependency property. </summary>
        public static readonly DependencyProperty DenominatorProperty = DependencyProperty.Register(nameof(Denominator), typeof(int), typeof(DSScorer), new PropertyMetadata(0, (sender, e) =>
        {
            DSScorer control = (DSScorer)sender;

            if (e.NewValue is int value)
            {
                control.D.Update(value);
            }
        }));

        /// <summary> Gets or set the Numerator for <see cref="DSScorer"/>. </summary>
        public int Numerator
        {
            get => (int)base.GetValue(NumeratorProperty);
            set => base.SetValue(NumeratorProperty, value);
        }
        /// <summary> Identifies the <see cref = "DSScorer.Numerator" /> dependency property. </summary>
        public static readonly DependencyProperty NumeratorProperty = DependencyProperty.Register(nameof(Numerator), typeof(int), typeof(DSScorer), new PropertyMetadata(0, (sender, e) =>
        {
            DSScorer control = (DSScorer)sender;

            if (e.NewValue is int value)
            {
                control.N.Update(value);
            }
        }));

        #endregion

        readonly int W = DSExtensions.W;
        readonly int H = DSExtensions.H;

        readonly PathGeometry D = DSExtensions.Number();
        readonly PathGeometry N = DSExtensions.Number();

        public DSScorer()
        {
            this.InitializeComponent();
        }
    }
}