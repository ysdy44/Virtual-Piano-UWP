﻿using System.Windows.Input;
using Windows.Devices.Midi;

namespace Virtual_Piano.Notes.Controls
{
    public sealed class BendModulationWheel : BendWheel0
    {
        //@Command
        public ICommand Command { get; set; }
        public ControlController Controller { get; set; } = ControlController.Modulation;

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