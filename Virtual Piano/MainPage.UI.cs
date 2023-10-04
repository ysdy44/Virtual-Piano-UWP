using System;
using Virtual_Piano.Midi;
using Windows.UI.Xaml;

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

        public void ClickMetronomeStart()
        {
            this.MetronomeTimer.Start();
            this.MetronomeButton.IsChecked = true;
        }
        public void ClickMetronomeStop()
        {
            this.MetronomeTimer.Stop();
            this.MetronomeButton.IsChecked = false;
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
            this.TrackIndex = -1;
            this.TrackNotePanel.Visibility = Visibility.Collapsed;
            this.TrackPanel.Visibility = Visibility.Visible;
        }
        public void ClickTrackNote(int index)
        {
            this.TrackIndex = index;
            this.TrackPanel.Visibility = Visibility.Collapsed;
            this.TrackNotePanel.Visibility = Visibility.Visible;
        }

        public void UpdateTrackTempo(Tempo tempo)
        {
            this.TempoSlider.Value = tempo.Bpm;
        }

        public void UpdateTrackTimeSignature(TimeSignature timeSignature, TimeSignatureTicks TrackTicks)
        {
            this.TimeSignaturesPanel.Update(timeSignature);

            this.NumeratorComboBox.SelectedItem = timeSignature.Numerator;
            this.DenominatorComboBox.SelectedItem = timeSignature.Denominator;

            this.TrackPanel.Init(this.TrackTimeSignature, this.TrackTicks);
            this.TrackNotePanel.Init(this.TrackTimeSignature, this.TrackTicks);
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

            if (this.TrackIndex < 0)
                this.TrackPanel.ChangePosition(this.TrackTempo.ReverseScale(this.Player.PositionMilliseconds), true, true);
            else
                this.TrackNotePanel.ChangePosition(this.TrackTempo.ReverseScale(this.Player.PositionMilliseconds), true, true);
        }
        private void Progress()
        {
            this.ProgressBar.Value = this.TrackDuration.GetPercent(this.Player.PositionMilliseconds);
            this.DSTimer.Time = this.Player.Position;

            if (this.TrackIndex < 0)
                this.TrackPanel.ChangePosition(this.TrackTempo.ReverseScale(this.Player.PositionMilliseconds), true, false);
            else
                this.TrackNotePanel.ChangePosition(this.TrackTempo.ReverseScale(this.Player.PositionMilliseconds), true, false);
        }
    }
}