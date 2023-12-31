﻿using Virtual_Piano.Midi.Core;

namespace Virtual_Piano.Midi.Instruments
{
    public sealed class ChordButton : ClickButton, IChordButton
    {
        //@Command
        public MidiOctave CommandParameter { get; set; }

        protected override void OnClick()
        {
            if (base.Parent is IChordPanel item)
            {
                item.OnClick(this.CommandParameter);
            }
        }
    }
}