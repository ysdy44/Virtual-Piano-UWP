using Virtual_Piano.Midi;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.TestApp
{
    public sealed partial class TimeSignaturePage : Page
    {
        //@Converter
        private string ObjectToStringConverter(object value) => $"{value}";

        TimeSignature TimeSignature = new TimeSignature(4, 4);

        public TimeSignaturePage()
        {
            this.InitializeComponent();

            // TimeSignature
            this.NumeratorComboBox.ItemsSource = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            this.DenominatorComboBox.ItemsSource = new int[] { 2, 4, 8 };
            this.NumeratorComboBox.SelectedItem = this.TimeSignature.Numerator;
            this.DenominatorComboBox.SelectedItem = this.TimeSignature.Denominator;
            this.NumeratorComboBox.SelectionChanged += (s, e) =>
            {
                this.TimeSignature = new TimeSignature((int)this.NumeratorComboBox.SelectedItem, this.TimeSignature.Denominator);
                this.TimeSignaturesPanel.UpdateNumerator(this.TimeSignature);
            };
            this.DenominatorComboBox.SelectionChanged += (s, e) =>
            {
                this.TimeSignature = new TimeSignature(this.TimeSignature.Numerator, (int)this.DenominatorComboBox.SelectedItem);
                this.TimeSignaturesPanel.UpdateDenominator(this.TimeSignature);
            };
            this.TimeSignaturesPanel.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                this.TimeSignaturesPanel.Update(this.TimeSignature, e.NewSize.Width);
            };
        }
    }
}