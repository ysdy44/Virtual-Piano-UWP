﻿using System.Windows.Input;

namespace Virtual_Piano.Notes.Controls
{
    public interface IInstrumentPanel
    {
        ICommand Command { get; set; }

        void OnClick(MidiProgram program);
        string GetString(MidiProgram note);

        void Add(MidiProgramGroup group);
        void Remove(MidiProgramGroup group);
    }
}