using System;
using System.Diagnostics;
using Windows.Media.Playback;
using Windows.UI.Xaml;

namespace Virtual_Piano.Midi
{
    public interface IPlayer : IBeatPlayer, ISeekPlayer
    {
        //@Delegate
        event EventHandler<object> Tick;

        bool IsPlaying { get; }

        void Play();
        void Pause();
        void Reset();
    }

    public interface IBeatPlayer
    {
        //@Delegate
        event EventHandler<string> Beat;

        int BeatCount { get; set; }
        string BeatChar { get; }
    }

    public interface ISeekPlayer
    {
        TimeSpan Position { get; }
        long PositionMilliseconds { get; }

        void Seek(TimeSpan delay);
        void Seek(long delayMilliseconds);
        void Seek(TimeSpan delay, long delayMilliseconds);
    }
}