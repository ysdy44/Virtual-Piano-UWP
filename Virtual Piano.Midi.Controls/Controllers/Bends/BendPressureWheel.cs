using System.Windows.Input;
using Windows.Devices.Midi;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed class BendPressureWheel : BendWheel
    {
        //@Command
        public ICommand Command { get; set; }

        public BendPressureWheel() : base(127) { }

        public override void Start() { }
        public override void Stop() { }

        public override void Execute(int value)
        {
            this.Command?.Execute(new MidiMessage
            {
                Type = MidiMessageType.ChannelPressure,
                Channel = 0,
                Pressure = (byte)value
            }); // Command
        }
    }
}