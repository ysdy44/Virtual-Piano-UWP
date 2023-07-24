﻿using Virtual_Piano.Midi;

namespace Virtual_Piano.Controls
{
    public sealed class InstrumentGroupPanel : Midi.Controls.InstrumentGroupPanel
    {
        public override string GetString(MidiProgram program)
        {
            return $"{(int)program} {App.Resource.GetString($"{program}")}";
        }
    }
}