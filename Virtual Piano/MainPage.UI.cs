using System;
using System.Linq;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Controllers;
using Virtual_Piano.Midi.Core;
using Virtual_Piano.Strings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano
{
    partial class MainPage
    {
        public void ClickRoulette()
        {
            if (this.IsRoulette)
            {
                this.IsRoulette = false;

                this.Rouletter.Hide();
                this.MarbleButton.Show();
            }
            else
            {
                this.IsRoulette = true;

                this.MarbleButton.Layout(base.ActualWidth, base.ActualHeight, 100);

                this.MarbleButton.Hide();
                this.Rouletter.Show();
            }
        }

        public void ClickFullScreen()
        {
            if (this.ApplicationView.IsFullScreenMode)
            {
                this.ClickUnFullScreen();
            }
            else if (this.ApplicationView.TryEnterFullScreenMode())
            {
                this.FullScreenButton.IsChecked = true;
            }
        }
        public void ClickUnFullScreen()
        {
            this.ApplicationView.ExitFullScreenMode();
            this.FullScreenButton.IsChecked = false;
        }

        public void ClickPlay()
        {
            this.PlayOrPauseIcon.Symbol = Symbol.Play;
        }
        public void ClickPause()
        {
            this.PlayOrPauseIcon.Symbol = Symbol.Pause;
        }

        public void ClickMetronomeStart()
        {
            this.MetronomeIndex = 0;
            this.MetronomeTimer.Start();
            this.MetronomeButton.IsChecked = true;
        }
        public void ClickMetronomeStop()
        {
            this.MetronomeIndex = 0;
            this.MetronomeTimer.Stop();
        }
        public void UpdateMetronome(Ticks ticks, Tempo tempo)
        {
            this.MetronomeTimer.Interval = TimeSpan.FromMilliseconds(tempo.ToMilliseconds(ticks.TicksPerBeat));
        }

        public void SetTheme(ElementTheme value)
        {
            this.LocalSettings.Values["Theme"] = (int)value;
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                if (frameworkElement.RequestedTheme == value) return;
                frameworkElement.RequestedTheme = value;
            }
        }
        public ElementTheme GetTheme()
        {
            if (this.LocalSettings.Values.ContainsKey("Theme"))
            {
                if (this.LocalSettings.Values["Theme"] is int item)
                {
                    return (ElementTheme)item;
                }
            }
            return ElementTheme.Dark;
        }

        public void ClickMute()
        {
            this.MuteButton.IsChecked = true;
        }
        public void ClickVolume()
        {
            this.MuteButton.IsChecked = false;
        }

        public void ClickTrack()
        {
            this.TrackSelectedChannel = -1;
            this.TrackNotePanel.Visibility = Visibility.Collapsed;
            this.TrackPanel.Visibility = Visibility.Visible;
        }
        public void ClickTrackNote(int channel)
        {
            this.TrackSelectedChannel = channel;
            this.TrackPanel.Visibility = Visibility.Collapsed;
            this.TrackNotePanel.Visibility = Visibility.Visible;
        }

        public void UpdateTrackPanel(TrackCollection tracks)
        {
            this.TrackPanel.ItemsSource = tracks;
            this.TrackPanel.ChangeDuration(tracks.Duration);

            int count = tracks.Count;
            foreach (MidiChannel item in System.Enum.GetValues(typeof(MidiChannel)).Cast<MidiChannel>())
            {
                int i = (byte)item;
                if (this.ChannelPanel.Children[i] is IChannelButton channel)
                {
                    channel.Text = "";
                    if (i >= count) continue;

                    if (tracks[i].Content is Track track)
                    {
                        if (track.Source == null) continue;
                        if (track.Source.Programs == null) continue;
                        if (track.Source.Programs.Count == 0) continue;

                        MidiMessage message = track.Source.Programs.First();
                        channel.Text = message.Program.GetString();
                    }
                }
            }
        }
        public void UpdateTrackTimeSignature(TimeSignature timeSignature)
        {
            this.TimeSignaturesPanel.Update(timeSignature);

            this.NumeratorComboBox.SelectedItem = timeSignature.Numerator;
            this.DenominatorComboBox.SelectedItem = timeSignature.Denominator;
        }
        public void UpdateTrackTicks(TimeSignature timeSignature, Ticks ticks)
        {
            this.TrackPanel.Init(timeSignature, ticks);
            this.TrackNotePanel.Init(timeSignature, ticks);
        }
        public void UpdateTrackTempo(Ticks ticks, Tempo tempo)
        {
            this.TempoSlider.Value = tempo.Bpm;
            this.PPQNSlider.Value = ticks.TicksPerQuarterNote;
        }

        private void Stop()
        {
            this.ProgressBar.Value = 0;
            this.DSTimer.Time = TimeSpan.Zero;

            this.TrackPanel.Stop();
            this.TrackNotePanel.Stop();
        }
        private void Start()
        {
            this.ProgressBar.Value = this.TrackDuration.GetPercent(this.Player.PositionMilliseconds);
            this.DSTimer.Time = this.Player.Position;

            if (this.TrackSelectedChannel < 0)
                this.TrackPanel.ChangePosition(this.TrackTempo.ToTicks(this.Player.PositionMilliseconds), true, true);
            else
                this.TrackNotePanel.ChangePosition(this.TrackTempo.ToTicks(this.Player.PositionMilliseconds), true, true);
        }
        private void Progress()
        {
            this.ProgressBar.Value = this.TrackDuration.GetPercent(this.Player.PositionMilliseconds);
            this.DSTimer.Time = this.Player.Position;

            if (this.TrackSelectedChannel < 0)
                this.TrackPanel.ChangePosition(this.TrackTempo.ToTicks(this.Player.PositionMilliseconds), true, false);
            else
                this.TrackNotePanel.ChangePosition(this.TrackTempo.ToTicks(this.Player.PositionMilliseconds), true, false);
        }
    }
}