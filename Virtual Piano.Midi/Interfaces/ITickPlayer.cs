using System;

namespace Virtual_Piano.Midi
{
    public interface ITickPlayer
    {
        //@Delegate
        event EventHandler<object> TickProgress;
        event EventHandler<string> TickBeat;

        int Beat { get; set; }
        string BeatChar { get; }

        TimeSpan Position { get; }
        long PositionMilliseconds { get; }
        bool IsPlaying { get; }

        void Seek(TimeSpan delay);
        void Seek(long delayMilliseconds);
        void Seek(TimeSpan delay, long delayMilliseconds);

        void Play();
        void Pause();
        void Stop();
    }
}