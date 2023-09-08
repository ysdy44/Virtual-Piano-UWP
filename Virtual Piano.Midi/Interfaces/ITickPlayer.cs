using System;

namespace Virtual_Piano.Midi
{
    public interface ITickPlayer
    {
        event EventHandler<object> TickProgress;
        event EventHandler<string> TickBeat;
        
        int Beat { get; set; }
        string BeatChar { get; }

        TimeSpan Position { get; }
        long PositionMilliseconds { get; }
        bool IsPlaying { get; }

        void Stop();
        void Pause();
        void Play();
    }
}