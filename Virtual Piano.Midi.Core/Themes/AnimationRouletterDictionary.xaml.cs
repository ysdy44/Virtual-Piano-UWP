using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Core
{
    public sealed partial class AnimationRouletterDictionary : ResourceDictionary
    {
        public Geometry this[int index] => this.FindGeometry($"Geometry{index}");
        public AnimationRouletterDictionary()
        {
            this.InitializeComponent();
        }
    }
}