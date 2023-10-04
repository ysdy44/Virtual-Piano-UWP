using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.Devices.Midi;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed class BendModulationWheel : BendWheel, IControlChange
    {
        //@Command
        public ICommand Command { get; set; }
        public MidiControlController Controller { get; set; } = MidiControlController.Modulation;
        public int Channel { get; set; }

        public BendModulationWheel() : base(0) { }

        public override void Start() { }
        public override void Stop() { }
        public override void Execute(int value)
        {
            this.Command?.Execute(new MidiMessage
            {
                Type = MidiMessageType.ControlChange,
                Channel = (byte)this.Channel,
                Controller = this.Controller,
                ControllerValue = (byte)value
            }); // Command
        }
    }
}