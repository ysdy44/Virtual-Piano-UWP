using System.Windows.Input;
using Windows.Devices.Midi;

namespace Virtual_Piano.Midi.Controls
{
    public sealed class BendModulationWheel : BendWheel
    {
        //@Command
        public ICommand Command { get; set; }
        public MidiControlController Controller { get; set; } = MidiControlController.Modulation;

        public BendModulationWheel() : base(0) { }

        public override void Start() { }
        public override void Stop() { }
        public override void Execute(int value)
        {
            this.Command?.Execute(new MidiMessage
            {
                Type = MidiMessageType.ControlChange,
                Controller = this.Controller,
                ControllerValue = (byte)value
            }); // Command
        }
    }
}