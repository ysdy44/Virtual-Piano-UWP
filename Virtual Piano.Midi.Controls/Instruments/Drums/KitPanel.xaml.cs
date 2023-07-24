using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Controls
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

                    this.LeftHandCrashStoryboard.Begin(); // Storyboard
                    break;
                case KitSet.Ride:
                    this.RideStoryboard.Stop(); // Storyboard
                    this.RideStoryboard.Begin(); // Storyboard

                    this.RightHandRideStoryboard.Begin(); // Storyboard
                    break;
                case KitSet.Open:
                    this.OpenStoryboard.Stop(); // Storyboard
                    this.OpenStoryboard.Begin(); // Storyboard

                    this.LeftHandHiHatStoryboard.Begin(); // Storyboard
                    break;
                case KitSet.Close:
                    this.CloseStoryboard.Stop(); // Storyboard
                    this.CloseStoryboard.Begin(); // Storyboard

                    this.LeftHandHiHatStoryboard.Begin(); // Storyboard
                    break;
                case KitSet.Pedal:
                    this.PedalStoryboard.Stop(); // Storyboard
                    this.PedalStoryboard.Begin(); // Storyboard
                    break;
                case KitSet.HiTom:
                    this.LeftHandHiTomStoryboard.Begin(); // Storyboard
                    break;
                case KitSet.LowTom:
                    this.RightHandLowTomStoryboard.Begin(); // Storyboard
                    break;
                case KitSet.FloorTom:
                    this.RightHandFloorTomStoryboard.Begin(); // Storyboard
                    break;
                case KitSet.Snare:
                    this.RightHandSnareStoryboard.Begin(); // Storyboard
                    break;
                case KitSet.Stick:
                    this.RightHandSnareStoryboard.Begin(); // Storyboard
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