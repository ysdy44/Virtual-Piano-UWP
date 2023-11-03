using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Elements
{
    public sealed partial class DigitalTimer : StackPanel
    {

        #region DependencyProperty

        /// <summary> Gets or set the time for <see cref="DigitalTimer"/>. </summary>
        public TimeSpan Time
        {
            get => (TimeSpan)base.GetValue(TimeProperty);
            set => base.SetValue(TimeProperty, value);
        }
        /// <summary> Identifies the <see cref = "DigitalTimer.Time" /> dependency property. </summary>
        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(nameof(Time), typeof(TimeSpan), typeof(DigitalTimer), new PropertyMetadata(TimeSpan.Zero, (sender, e) =>
        {
            DigitalTimer control = (DigitalTimer)sender;

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

        readonly int W = DigitalExtensions.W;
        readonly int H = DigitalExtensions.H;

        readonly PathGeometry MT = DigitalExtensions.Number();
        readonly PathGeometry M = DigitalExtensions.Number();

        readonly PathGeometry ST = DigitalExtensions.Number();
        readonly PathGeometry S = DigitalExtensions.Number();

        readonly PathGeometry MST = DigitalExtensions.Number();
        readonly PathGeometry MS = DigitalExtensions.Number();
   
        public DigitalTimer()
        {
            this.InitializeComponent();
            this.MT.Update(DigitalType.N0);
            this.M.Update(DigitalType.N0);

            this.ST.Update(DigitalType.N0);
            this.S.Update(DigitalType.N0);

            this.MST.Update(DigitalType.N0);
            this.MS.Update(DigitalType.N0);
        }
    }
}