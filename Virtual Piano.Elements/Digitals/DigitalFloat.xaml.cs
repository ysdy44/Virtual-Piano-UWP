using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Elements
{
    public sealed partial class DigitalFloat : StackPanel
    {

        #region DependencyProperty

        /// <summary> Gets or set the Value for <see cref="DigitalFloat"/>. </summary>
        public double Value
        {
            get => (double)base.GetValue(ValueProperty);
            set => base.SetValue(ValueProperty, value);
        }
        /// <summary> Identifies the <see cref = "DigitalFloat.Value" /> dependency property. </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(double), typeof(DigitalFloat), new PropertyMetadata(0d, (sender, e) =>
        {
            DigitalFloat control = (DigitalFloat)sender;

            if (e.NewValue is double value)
            {
                string text = ((int)(value * 100)).ToString("00000");

                control.N100.Update(text[0]);
                control.N10.Update(text[1]);
                control.N1.Update(text[2]);

                control.F1.Update(text[3]);
                control.F10.Update(text[4]);
            }
        }));

        #endregion

        readonly int W = DigitalExtensions.W;
        readonly int H = DigitalExtensions.H;

        readonly PathGeometry N100 = DigitalExtensions.Number();
        readonly PathGeometry N10 = DigitalExtensions.Number();
        readonly PathGeometry N1 = DigitalExtensions.Number();

        readonly PathGeometry F1 = DigitalExtensions.Number();
        readonly PathGeometry F10 = DigitalExtensions.Number();

        public DigitalFloat()
        {
            this.InitializeComponent();
        }
    }
}