using System;
using System.Windows.Input;
using Windows.Devices.Midi;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Controls
{
    public sealed class VelocitySlider : Slider, ICommand
    {
        //@Command
        public ICommand Command { get; set; }
        public MidiControlController Controller { get; set; }

        public VelocitySlider()
        {
            base.ValueChanged += (s, e) =>
            {
                int value = System.Math.Clamp((int)e.NewValue, 0, Radial.Velocity);
                this.Command?.Execute(new MidiMessage
                {
                    Type = MidiMessageType.ControlChange,
                    Controller = this.Controller,
                    ControllerValue = (byte)value
                }); // Command
            };
        }

        //@Delegate
        public event EventHandler CanExecuteChanged;

        //@Command
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            if (parameter is bool item)
            {
                if (item)
                {
                    base.IsEnabled = true;

                    int value = System.Math.Clamp((int)base.Value, 0, Radial.Velocity);
                    this.Command?.Execute(new MidiMessage
                    {
                        Type = MidiMessageType.ControlChange,
                        Controller = this.Controller,
                        ControllerValue = (byte)value
                    }); // Command
                }
                else
                {
                    base.IsEnabled = false;

                    this.Command?.Execute(new MidiMessage
                    {
                        Type = MidiMessageType.ControlChange,
                        Controller = this.Controller,
                        ControllerValue = 0
                    }); // Command
                }
            }
        }
    }
}