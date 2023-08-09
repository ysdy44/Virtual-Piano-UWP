using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Controls
{
    public partial class HarpPanel : StackPanel, IHarpPanel
    {
        //@Command
        public ICommand Command { get; set; }

        //@Const
        const MidiOctave Number = MidiOctave.Number3;
        const int Min = (int)HarpPanel.Number;
        const int Max = HarpPanel.Min + 7;

        //@Construct
        public HarpPanel()
        {
            this.InitializeComponent();

            for (int y = HarpPanel.Min; y < HarpPanel.Max; y++)
            {
                base.Children.Add(new HarpButton
                {
                    CommandParameter = (MidiNote)(Tone.C + y * 12),
                    Style = base.Resources[$"{HarpType.Red}"] as Style,
                });
                base.Children.Add(new HarpButton
                {
                    CommandParameter = (MidiNote)(Tone.D + y * 12),
                    Style = base.Resources[$"{HarpType.White}"] as Style,
                });
                base.Children.Add(new HarpButton
                {
                    CommandParameter = (MidiNote)(Tone.E + y * 12),
                    Style = base.Resources[$"{HarpType.White}"] as Style,
                });
                base.Children.Add(new HarpButton
                {
                    CommandParameter = (MidiNote)(Tone.F + y * 12),
                    Style = base.Resources[$"{HarpType.Blue}"] as Style,
                });
                base.Children.Add(new HarpButton
                {
                    CommandParameter = (MidiNote)(Tone.G + y * 12),
                    Style = base.Resources[$"{HarpType.White}"] as Style,
                });
                base.Children.Add(new HarpButton
                {
                    CommandParameter = (MidiNote)(Tone.A + y * 12),
                    Style = base.Resources[$"{HarpType.White}"] as Style,
                });
                base.Children.Add(new HarpButton
                {
                    CommandParameter = (MidiNote)(Tone.B + y * 12),
                    Style = base.Resources[$"{HarpType.White}"] as Style,
                });
            }
        }

        public void OnClick(MidiNote note)
        {
            this.Command?.Execute(note); // Command
        }
    }
}