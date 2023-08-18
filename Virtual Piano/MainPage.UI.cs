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
    }
}