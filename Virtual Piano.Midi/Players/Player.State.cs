using System;
using System.Diagnostics;
using Virtual_Piano.Midi;
using Windows.Media.Playback;
using Windows.UI.Xaml;

namespace Virtual_Piano.Midi
{
    partial class Player
    {
        //@Delegate
        public event EventHandler<MediaPlaybackState> CurrentStateChanged;

        public MediaPlaybackState CurrentState { get; private set; }

        private void StatePlay()
        {
            if (this.CurrentState == MediaPlaybackState.Playing) return;

            this.CurrentState = MediaPlaybackState.Playing;
            this.CurrentStateChanged?.Invoke(this, MediaPlaybackState.Playing); // Delegate
        }

        private void StatePause()
        {
            if (this.CurrentState == MediaPlaybackState.Paused) return;

            this.CurrentState = MediaPlaybackState.Paused;
            this.CurrentStateChanged?.Invoke(this, MediaPlaybackState.Paused); // Delegate
        }

        private void StateReset()
        {
            if (this.CurrentState == MediaPlaybackState.None) return;

            this.CurrentState = MediaPlaybackState.None;
            this.CurrentStateChanged?.Invoke(this, MediaPlaybackState.None); // Delegate
        }
    }
}