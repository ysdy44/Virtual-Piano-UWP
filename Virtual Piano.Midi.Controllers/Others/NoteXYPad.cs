using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.Devices.Midi;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed class NoteXYPad : XYPad
    {
        //@Command
        public ICommand Command { get; set; }

        public override void OnClick(int x, int y)
        {
            this.Command?.Execute(new MidiMessage
            {
                Type = MidiMessageType.NoteOn,
                Duration = 2000,
                Note = (MidiNote)(byte)x,
                Velocity = (byte)(Radial.Velocity - y)
            }); // Command
        }
    }
}