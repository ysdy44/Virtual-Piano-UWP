using Virtual_Piano.Midi.Core;
using Windows.Devices.Midi;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed class NoteXYPad : XYPad
    {
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