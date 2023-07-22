using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Notes.Controls
{
    public partial class KitPanel : Canvas, IKitPanel
    {
        //@Command
        public ICommand Command { get; set; }

        readonly IList<KitSet> ItemsSource = new List<KitSet>
        {
            KitSet.Crash,
            KitSet.Ride,
            KitSet.Open,
            KitSet.Close,
            KitSet.Pedal,
            KitSet.HiTom,
            KitSet.LowTom,
            KitSet.FloorTom,
            KitSet.Snare,
            KitSet.Stick,
            KitSet.Kick,
        };

        public KitPanel()
        {
            this.InitializeComponent();
        }

        public void OnClick(KitSet set)
        {
            this.Command?.Execute((MidiPercussionNote)set); // Command
        }
    }
}