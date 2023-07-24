﻿using System.Windows.Input;

namespace Virtual_Piano.Midi.Controls
{
    public interface IGuitarPanel
    {
        ICommand Command { get; set; }

        void OnClick(MidiNote note, GuitarString strings);
    }
}