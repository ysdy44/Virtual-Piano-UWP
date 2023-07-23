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

            switch (set)
            {
                case KitSet.Crash:
                    this.CrashStoryboard.Stop(); // Storyboard
                    this.CrashStoryboard.Begin(); // Storyboard
                    break;
                case KitSet.Ride:
                    this.RideStoryboard.Stop(); // Storyboard
                    this.RideStoryboard.Begin(); // Storyboard
                    break;
                case KitSet.Open:
                    break;
                case KitSet.Close:
                    break;
                case KitSet.Pedal:
                    break;
                case KitSet.HiTom:
                    break;
                case KitSet.LowTom:
                    break;
                case KitSet.FloorTom:
                    break;
                case KitSet.Snare:
                    break;
                case KitSet.Stick:
                    break;
                case KitSet.Kick:
                    this.KickStoryboard1.Stop(); // Storyboard
                    this.KickStoryboard2.Stop(); // Storyboard
                    this.KickStoryboard1.Begin(); // Storyboard
                    this.KickStoryboard2.Begin(); // Storyboard
                    break;
                default:
                    break;
            }
        }
    }
}