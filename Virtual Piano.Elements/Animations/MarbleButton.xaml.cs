using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Elements
{
    public sealed partial class MarbleButton : Button
    {
        double X = 32;
        double Y = 32;
        //double MinX = 32;
        //double MinY = 32;
        double MaxX = 1024;
        double MaxY = 1024;

        readonly Marble MarbleX = new Marble();
        readonly Marble MarbleY = new Marble();

        public MarbleButton()
        {
            this.InitializeComponent();
            base.ManipulationStarted += (s, e) =>
            {
                base.IsEnabled = false;
                if (base.Parent is FrameworkElement canvas)
                {
                    this.MaxX = canvas.ActualWidth - 32;
                    this.MaxY = canvas.ActualHeight - 32;
                }
                else
                {
                    this.MaxX = 1024;
                    this.MaxY = 1024;
                }
            };
            base.ManipulationDelta += (s, e) =>
            {
                this.X = this.MarbleX.Move(this.X, e.Delta.Translation.X, e.IsInertial, 32, this.MaxX);
                this.Y = this.MarbleY.Move(this.Y, e.Delta.Translation.Y, e.IsInertial, 32, this.MaxY);
                Canvas.SetLeft(this, this.X - 32);
                Canvas.SetTop(this, this.Y - 32);
            };
            base.ManipulationCompleted += (s, e) => base.IsEnabled = true;

            this.HideStoryboard.Completed += (s, e) =>
            {
                this.RootBorder.Visibility = Visibility.Collapsed;
            };
        }

        public void Layout(double width, double height)
        {
            this.MaxX = width - 32;
            this.MaxY = height - 32;
            this.X = System.Math.Clamp(this.X, 32, this.MaxX);
            this.Y = System.Math.Clamp(this.Y, 32, this.MaxY);
            Canvas.SetLeft(this, this.X - 32);
            Canvas.SetTop(this, this.Y - 32);
        }

        public void Layout(double width, double height, int radius)
        {
            this.MaxX = width - radius;
            this.MaxY = height - radius;
            this.X = System.Math.Clamp(this.X, radius, this.MaxX);
            this.Y = System.Math.Clamp(this.Y, radius, this.MaxY);
            Canvas.SetLeft(this, this.X - 32);
            Canvas.SetTop(this, this.Y - 32);
        }

        public void Hide()
        {
            this.ShowStoryboard.Stop(); // Storyboard
            this.HideStoryboard.Begin(); // Storyboard
        }

        public void Show()
        {
            this.RootBorder.Visibility = Visibility.Visible;
            this.HideStoryboard.Stop(); // Storyboard
            this.ShowStoryboard.Begin(); // Storyboard
        }

        public void ShowRing()
        {
            this.Ring.Show();
        }
    }
}