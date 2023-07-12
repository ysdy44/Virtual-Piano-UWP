using System.Windows.Input;
using Windows.Devices.Midi;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Notes.Controls
{
    public sealed class VelocitySlider : Slider
    {
        //@Command
        public ICommand Command { get; set; }
        public ControlController Controller { get; set; }

        public VelocitySlider()
        {
            base.ValueChanged += (s, e) =>
            {
                int value = System.Math.Clamp((int)e.NewValue, 0, Radial.Velocity);
                this.Command?.Execute(new Message
                {
                    Type = MidiMessageType.ControlChange,
                    Controller = this.Controller,
                    ControllerValue = (byte)value
                }); // Command
            };
        }
    }
}