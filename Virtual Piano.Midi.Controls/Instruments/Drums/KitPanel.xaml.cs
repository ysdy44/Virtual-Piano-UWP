using System;
using System.Linq;
using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Instruments
{
    public partial class KitPanel : Canvas, IKitPanel, ICommand
    {
        //@Command
        public ICommand Command { get; set; }

        readonly string[] Strings;
        readonly KitSet[] ItemsSource = new KitSet[]
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

        readonly DispatcherTimer Timer = new DispatcherTimer
        {
            Interval = System.TimeSpan.FromMilliseconds(400)
        };

        public KitPanel()
        {
            this.InitializeComponent();
            this.Strings = this.ItemsSource.Select(this.GetString).ToArray();
            this.Timer.Tick += (s, e) => this.HideToolTip();
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

        private string GetString(KitSet note) => this.GetString((MidiPercussionNote)note);
        public virtual string GetString(MidiPercussionNote note)
        {
            return note.ToString();
        }

        //@Delegate
        public event EventHandler CanExecuteChanged;

        //@Command
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            this.ShowToolTip();

            //if (parameter is bool item)
            //{
            //    if (item)
            //        this.ShowToolTip();
            //    else
            //        this.HideToolTip();
            //}
        }

        public void HideToolTip()
        {
            this.Timer.Stop();
            base.AllowDrop = false;
        }
        public void ShowToolTip()
        {
            this.Timer.Stop();
            this.Timer.Start();
            base.AllowDrop = true;
        }
    }
}