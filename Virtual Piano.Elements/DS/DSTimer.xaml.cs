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

                control.MT.Update(text[0]);
                control.M.Update(text[1]);

                control.ST.Update(text[3]);
                control.S.Update(text[4]);

                control.MST.Update(text[6]);
                control.MS.Update(text[7]);
            }
        }));

        #endregion

        public DSTimer()
        {
            this.InitializeComponent();
            base.Height = DSNumber.H;

            this.MT.Update(DSType.N0);
            this.M.Update(DSType.N0);

            this.ST.Update(DSType.N0);
            this.S.Update(DSType.N0);

            this.MST.Update(DSType.N0);
            this.MS.Update(DSType.N0);
        }
    }
}