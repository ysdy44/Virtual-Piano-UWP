using System.Windows.Input;
using Windows.Devices.Midi;

namespace Virtual_Piano.Notes.Controls
{
    public sealed class BendModulationWheel : BendWheel
    {
        public ICommand Command { get; set; }

        public override void Execute(int value)
        {
            this.Command?.Execute(new Message
            {
                Type = MidiMessageType.NoteOn,
                Controller = ControlController.Modulation,
                ControllerValue = (byte)value
            }); // Command
        }
    }
}