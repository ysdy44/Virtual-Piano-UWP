using System.Windows.Input;
using Windows.Devices.Midi;

namespace Virtual_Piano.Midi.Controls
{
    public sealed class BendModulationWheel : BendWheel0
    {
        //@Command
        public ICommand Command { get; set; }
        public MidiControlController Controller { get; set; } = MidiControlController.Modulation;

        public override void Execute(int value)
        {
            this.Command?.Execute(new Message
            {
                Type = MidiMessageType.NoteOn,
                Controller = this.Controller,
                ControllerValue = (byte)value
            }); // Command
        }
    }
}