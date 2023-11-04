using System;

namespace Virtual_Piano.Midi
{
    public interface IPlayer
    {
        //@Delegate
        event EventHandler<object> Tick;
        event EventHandler<string> Beat;

        int BeatCount { get; set; }
        string BeatChar { get; }

        TimeSpan Position { get; }
        long PositionMilliseconds { get; }
        bool IsPlaying { get; }

        void Seek(TimeSpan delay);
        void Seek(long delayMilliseconds);
        void Seek(TimeSpan delay, long delayMilliseconds);

        void Play();
        void Pause();
        void Reset();
    }
}