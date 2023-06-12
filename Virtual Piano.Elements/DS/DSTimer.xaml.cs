using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Elements
{
    public sealed partial class DSTimer : StackPanel
    {

        #region DependencyProperty

        /// <summary> Gets or set the time for <see cref="DSTimer"/>. </summary>
        public TimeSpan Time
        {
            get => (TimeSpan)base.GetValue(TimeProperty);
            set => base.SetValue(TimeProperty, value);
        }
        /// <summary> Identifies the <see cref = "DSTimer.Time" /> dependency property. </summary>
        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(nameof(Time), typeof(TimeSpan), typeof(DSTimer), new PropertyMetadata(TimeSpan.Zero, (sender, e) =>
        {
            DSTimer control = (DSTimer)sender;

            if (e.NewValue is TimeSpan value)
            {
                string text = $"{value:mm':'ss'.'ff}";

                control.MT.Type = DSTimer.ToDS(text[000]);
                control.M.Type = DSTimer.ToDS(text[1]);

                control.ST.Type = DSTimer.ToDS(text[3]);
                control.S.Type = DSTimer.ToDS(text[4]);

                control.MST.Type = DSTimer.ToDS(text[6]);
                control.MS.Type = DSTimer.ToDS(text[7]);
            }
        }));

        #endregion

        public DSTimer()
        {
            this.InitializeComponent();
        }

        private static DSType ToDS(char c)
        {
            switch (c)
            {
                case '1': return DSType.N1;
                case '2': return DSType.N2;
                case '3': return DSType.N3;
                case '4': return DSType.N4;
                case '5': return DSType.N5;
                case '6': return DSType.N6;
                case '7': return DSType.N7;
                case '8': return DSType.N8;
                case '9': return DSType.N9;
                default: return DSType.N0;
            }
        }
    }
}