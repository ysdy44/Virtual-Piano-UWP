using System.Collections.Generic;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Notes.Controls
{
    public sealed partial class DrumPanel : Canvas, IDrumPanel
    {
        //@Command
        public ICommand Command { get; set; }

        public IDrumButton this[int item] => base.Children[System.Math.Clamp(item, 0, this.ItemSource.Count - 1)] as IDrumButton;
        public IDrumButton this[MidiPercussionNote item] => this.ItemSource.Contains(item) ? this[this.ItemSource.IndexOf(item)] : null;

        public readonly IList<MidiPercussionNote> ItemSource = new List<MidiPercussionNote>
        {
             MidiPercussionNote.OpenHiHat, MidiPercussionNote.RideCymbal1, MidiPercussionNote.Shaker, MidiPercussionNote.CrashCymbal1,
             MidiPercussionNote.ClosedHiHat, MidiPercussionNote.LowTom, MidiPercussionNote.LowLowMidTom, MidiPercussionNote.HighTom,
             MidiPercussionNote.Cowbell, MidiPercussionNote.HandClap, MidiPercussionNote.AcousticSnareDrum, MidiPercussionNote.AcousticBassDrum,
        };

        public DrumPanel()
        {
            this.InitializeComponent();
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                var w = e.NewSize.Width / 4;
                var h = e.NewSize.Height / 3;

                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        int i = 4 * y + x;
                        IDrumButton item = base.Children[i] as IDrumButton;

                        item.X = x * w;
                        item.Y = y * h;
                        item.Width = w;
                        item.Height = h;
                    }
                }
            };

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    int i = 4 * y + x;
                    MidiPercussionNote item = this.ItemSource[i];

                    base.Children.Add(new DrumButton
                    {
                        Tag = MidiPercussionNoteFactory.English[item.ToString()],
                        CommandParameter = item
                    });
                }
            }
        }

        public void OnClick(MidiPercussionNote note) => this.Command?.Execute(note); // Command
    }
}