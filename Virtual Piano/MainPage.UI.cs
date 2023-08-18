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
        }
        public void ClickMetronomeStop()
        {
            this.MetronomeTimer.Stop();
        }
    }
}