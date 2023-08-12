using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.Devices.Midi;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed partial class Pedal : ContactButton
    {
        //@Command
        public ICommand Command { get; set; }
        public MidiControlController Controller { get; set; }

        public Pedal()
        {
            this.InitializeComponent();
        }

        protected override void OnContactOn()
        {
            this.Command?.Execute(new MidiMessage
            {
                Type = MidiMessageType.ControlChange,
                Controller = this.Controller,
                ControllerValue = Radial.Velocity
            }); // Command
        }

        protected override void OnContactOff()
        {
            this.Command?.Execute(new MidiMessage
            {
                Type = MidiMessageType.ControlChange,
                Controller = this.Controller,
                ControllerValue = 0
            }); // Command
        }
    }
}