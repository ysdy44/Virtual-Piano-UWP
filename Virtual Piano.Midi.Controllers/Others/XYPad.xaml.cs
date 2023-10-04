using System.ComponentModel;
using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Controllers
{
    public abstract partial class XYPad : Canvas, INotifyPropertyChanged
    {
        //@Command
        public ICommand Command { get; set; }

        double PositionX;
        double PositionY;

        XYPadPoint Point = new XYPadPoint(64, 64, 424, 424, 24);
        XYPadLines LinesX = new XYPadLines(12, 412);
        XYPadLines LinesY = new XYPadLines(12, 412);
        Visibility Visible = Visibility.Collapsed;

        int ThumbSize => 24;
        public int X => (int)this.Point.X;
        public int Y => (int)this.Point.Y;

        public XYPad()
        {
            this.InitializeComponent();
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                int w = (int)e.NewSize.Width;
                int h = (int)e.NewSize.Height;
                if (this.Point.Width == w && this.Point.Height == h) return;

                this.Point = new XYPadPoint(this.Point.X, this.Point.Y, w, h, this.ThumbSize);
                this.LinesX = new XYPadLines(this.Point.Left, this.Point.Right);
                this.LinesY = new XYPadLines(this.Point.Top, this.Point.Bottom);

                this.OnPropertyChanged(nameof(Point));
                this.OnPropertyChanged(nameof(LinesX));
                this.OnPropertyChanged(nameof(LinesY));
            };

            this.Thumb.DragStarted += (s, e) =>
            {
                this.PositionX = e.HorizontalOffset;
                this.PositionY = e.VerticalOffset;

                int x = this.Point.GetX(this.PositionX);
                int y = this.Point.GetY(this.PositionY);
                if (this.Point.X == x && this.Point.Y == y) return;
                this.OnClick((int)x, (int)y);

                this.Point = new XYPadPoint(x, y, this.Point.Width, this.Point.Height, this.ThumbSize);
                this.Visible = Visibility.Visible;
                this.OnPropertyChanged(nameof(Point));
                this.OnPropertyChanged(nameof(Visible));
            };
            this.Thumb.DragDelta += (s, e) =>
            {
                this.PositionX += e.HorizontalChange;
                this.PositionY += e.VerticalChange;

                int x = this.Point.GetX(this.PositionX);
                int y = this.Point.GetY(this.PositionY);
                if (this.Point.X == x && this.Point.Y == y) return;
                this.OnClick((int)x, (int)y);

                this.Point = new XYPadPoint(x, y, this.Point.Width, this.Point.Height, this.ThumbSize);
                this.OnPropertyChanged(nameof(Point));
            };
            this.Thumb.DragCompleted += (s, e) =>
            {
                this.Visible = Visibility.Collapsed;
                this.OnPropertyChanged(nameof(Visible));
            };
        }

        public abstract void OnClick(int x, int y);

        //@Notify
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}