using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Elements
{
    public sealed partial class DSFloat : StackPanel
    {

        #region DependencyProperty

        /// <summary> Gets or set the Value for <see cref="DSFloat"/>. </summary>
        public double Value
        {
            get => (double)base.GetValue(ValueProperty);
            set => base.SetValue(ValueProperty, value);
        }
        /// <summary> Identifies the <see cref = "DSFloat.Value" /> dependency property. </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(double), typeof(DSFloat), new PropertyMetadata(0d, (sender, e) =>
        {
            DSFloat control = (DSFloat)sender;

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

        public DSFloat()
        {
            this.InitializeComponent();
        }
    }
}