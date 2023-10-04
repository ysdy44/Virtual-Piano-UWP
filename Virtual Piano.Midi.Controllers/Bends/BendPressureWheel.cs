using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.Devices.Midi;

namespace Virtual_Piano.Midi.Controllers
{
    public sealed class BendPressureWheel : BendWheel, IControl
    {
        //@Command
        public ICommand Command { get; set; }
        public int Channel { get; set; }

        public BendPressureWheel() : base(Radial.Velocity) { }

        public override void Start() { }
        public override void Stop() { }

        public override void Execute(int value)
        {
            this.Command?.Execute(new MidiMessage
            {
                Type = MidiMessageType.ChannelPressure,
                Channel = (byte)this.Channel,
                Pressure = (byte)value
            }); // Command
        }
    }
}