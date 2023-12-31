﻿namespace Virtual_Piano.Midi.Core
{
    public interface IMachineButton : IClickButton
    {
        bool AllowDrop { get; set; }
        bool IsEnabled { get; set; }
        bool? IsChecked { get; set; }
    }
}