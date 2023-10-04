using System;

namespace Virtual_Piano.Midi.Core
{
    public interface ITrackPanel : ITrack
    {
        //@Delegate
        event EventHandler<int> ItemClick;
    }
}