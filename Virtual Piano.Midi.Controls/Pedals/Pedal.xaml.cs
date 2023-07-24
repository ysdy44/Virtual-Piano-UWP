﻿using System.Windows.Input;
using Windows.Devices.Midi;

namespace Virtual_Piano.Midi.Controls
{
    public sealed partial class Pedal : ContactButton
    {
        //@Command
        public ICommand Command { get; set; }
        public ControlController Controller { get; set; }

        public Pedal()
        {
            this.InitializeComponent();
        }

        protected override void OnContactOn()
        {
            this.Command?.Execute(new Message
            {
                Type = MidiMessageType.ControlChange,
                Controller = this.Controller,
                ControllerValue = 127
            }); // Command
        }

        protected override void OnContactOff()
        {
            this.Command?.Execute(new Message
            {
                Type = MidiMessageType.ControlChange,
                Controller = this.Controller,
                ControllerValue = 0
            }); // Command
        }
    }
}