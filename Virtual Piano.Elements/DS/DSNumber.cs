using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Elements
{
    /// <summary>
    /// DS-Digital Regular
    /// </summary>
    public sealed class DSNumber : Canvas
    {
        const double N0 = 0;
        const double N1 = 1 * 1;
        const double N2 = 2 * 1;
        const double N8 = 8 * 1;
        const double N9 = 9 * 1;
        const double N16 = 16 * 1;
        const double Padding = 0.4;

        readonly Dictionary<DSType, int> Dictionary = new Dictionary<DSType, int>
        {
            [DSType.Line0] = 0,
            [DSType.Line1] = 1,
            [DSType.Line2] = 2,
            [DSType.Line3] = 3,
            [DSType.Line4] = 4,
            [DSType.Line5] = 5,
            [DSType.Line6] = 6,
        };

        #region DependencyProperty

        /// <summary> Gets or set the type for <see cref="DSNumber"/>. </summary>
        internal DSType Type
        {
            get => (DSType)base.GetValue(TypeProperty);
            set => base.SetValue(TypeProperty, value);
        }
        /// <summary> Identifies the <see cref = "DSNumber.Type" /> dependency property. </summary>
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(nameof(Type), typeof(DSType), typeof(DSNumber), new PropertyMetadata(DSType.N0, (sender, e) =>
        {
            DSNumber control = (DSNumber)sender;

            if (e.NewValue is DSType value)
            {
                DSType type = value;
                control.Update(type);
            }

            if (e.NewValue is int t)
            {
                control.Update((DSType)t);
            }
        }));

        #endregion

        public DSNumber()
        {
            base.Width = 10 * 1;
            base.Height = 18 * 1;

            base.Children.Add(V(N0, N0, Padding));
            base.Children.Add(H(N0, N0, Padding));
            base.Children.Add(V(N8, N0, Padding));
            base.Children.Add(V(N8, N8, Padding));
            base.Children.Add(H(N0, N16, Padding));
            base.Children.Add(V(N0, N8, Padding));
            base.Children.Add(H(N0, N8, Padding));

            this.Update(this.Type);
        }

        private void Update(DSType type)
        {
            if (this.Children.Count == 0) return;
            foreach (var item in this.Dictionary)
            {
                if (this.Children[item.Value] is null) return;

                this.Children[item.Value].Visibility =
                type.HasFlag(item.Key) ?
                 Visibility.Visible : Visibility.Collapsed;
            }
        }

        private static Polygon V(double x, double y, double padding) => new Polygon
        {
            Points =
            {
                new Point(N2+x, N2+y+padding),
                new Point(N1+x, N1+y+padding),
                new Point(N0+x, N2+y+padding),

                new Point(N0+x, N8+y-padding),
                new Point(N1+x, N9+y-padding),
                new Point(N2+x, N8+y-padding),
            }
        };

        private static Polygon H(double x, double y, double padding) => new Polygon
        {
            Points =
            {
                new Point(N2+x+padding, N2+y),
                new Point(N1+x+padding, N1+y),
                new Point(N2+x+padding, N0+y),

                new Point(N8+x-padding, N0+y),
                new Point(N9+x-padding, N1+y),
                new Point(N8+x-padding, N2+y),
            }
        };
    }
}