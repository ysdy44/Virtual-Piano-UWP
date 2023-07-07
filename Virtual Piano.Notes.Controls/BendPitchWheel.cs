using System.Windows.Input;
using Windows.Devices.Midi;

namespace Virtual_Piano.Notes.Controls
{
    public sealed class BendPitchWheel : BendWheel
    {
        public ICommand Command { get; set; }

        public override void Execute(int value)
        {
            this.Command?.Execute(new Message
            {
                Type = MidiMessageType.PitchBendChange,
                Bend = (byte)value
            }); // Command
        }
    }
}