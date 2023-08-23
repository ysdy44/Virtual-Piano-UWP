using System.Threading.Tasks;
using Virtual_Piano.Elements;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Controls
{
    public class CarouselCCm : ChordCarousel { public CarouselCCm() : base(0, Chord.C, Chord.Cm) { } }
    public class CarouselDmD : ChordCarousel { public CarouselDmD() : base(1, Chord.D, Chord.Dm) { } }
    public class CarouselEmE : ChordCarousel { public CarouselEmE() : base(1, Chord.E, Chord.Em) { } }
    public class CarouselFFm : ChordCarousel { public CarouselFFm() : base(0, Chord.F, Chord.Fm) { } }
    public class CarouselGGm : ChordCarousel { public CarouselGGm() : base(0, Chord.G, Chord.Gm) { } }
    public class CarouselAmA : ChordCarousel { public CarouselAmA() : base(1, Chord.A, Chord.Am) { } }
    public class CarouselBdimBBm : ChordCarousel { public CarouselBdimBBm() : base(2, Chord.B, Chord.Bm, Chord.Bdim) { } }
    public class ChordCarousel : Carousel
    {
        public IChordPanel ChordPanel { get; set; }

        readonly Chord[] Items;

        #region DependencyProperty

        /// <summary> Gets or set the visible for <see cref="ChordCarousel"/>. </summary>
        public bool IsVisible
        {
            get => (bool)base.GetValue(IsVisibleProperty);
            set => base.SetValue(IsVisibleProperty, value);
        }
        /// <summary> Identifies the <see cref = "ChordCarousel.IsVisible" /> dependency property. </summary>
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register(nameof(IsVisible), typeof(bool), typeof(ChordCarousel), new PropertyMetadata(false, async (sender, e) =>
        {
            ChordCarousel control = (ChordCarousel)sender;

            if (e.NewValue is bool value)
            {
                if (value)
                {
                    control.Visibility = Visibility.Visible;
                    return;
                }

                await Task.Delay(1000);
                if (control.IsVisible) return;

                control.Visibility = Visibility.Collapsed;
            }
        }));

        #endregion

        public ChordCarousel(int index, params Chord[] items)
        {
            base.Visibility = Visibility.Collapsed;
            this.Items = items;
            base.Reset(items, index);
            base.ItemClick += (s, e) =>
            {
                if (this.ChordPanel is null) return;

                this.ChordPanel.Chord = this.Items[e];
            };
        }
    }
}