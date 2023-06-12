using Windows.Foundation;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Virtual_Piano.Elements
{
    [ContentProperty(Name = nameof(Child))]
    public sealed partial class ScrollViewer2 : UserControl
    {
        public Orientation Orientation { get; set; } = Orientation.Horizontal;
        public IScrollBar2 HorizontalScrollBar { get; } = new ScrollBar2();
        public IScrollBar2 VerticalScrollBar { get; } = new ScrollBar2();

        public object Child
        {
            get => this.ContentPresenter.Content;
            set
            {
                if (this.Child is FrameworkElement oldValue)
                {
                    oldValue.SizeChanged -= this.ContentSizeChanged;
                }
                this.ContentPresenter.Content = value;
                if (this.Child is FrameworkElement newValue)
                {
                    newValue.SizeChanged -= this.ContentSizeChanged;
                    newValue.SizeChanged += this.ContentSizeChanged;
                }
            }
        }

        public ScrollViewer2()
        {
            this.InitializeComponent();
            base.SizeChanged += (s, e) =>
            {
                if (this.Child is null) return;

                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                if (e.NewSize.Width != e.PreviousSize.Width)
                    this.ScrollX(this.HorizontalScrollBar.ChangeView(e.NewSize.Width, this.HorizontalScrollBar.ExtentSize));

                if (e.NewSize.Height != e.PreviousSize.Height)
                    this.ScrollY(this.VerticalScrollBar.ChangeView(e.NewSize.Height, this.VerticalScrollBar.ExtentSize));
            };

            // X
            this.XThumb.DragDelta += (s, e) =>
            {
                ScrollData data = this.HorizontalScrollBar.ChangeView(e.HorizontalChange);

                this.TranslateTransform.X = -data.Offset;
                Canvas.SetLeft(this.XThumb, data.Location);
            };
            this.XThumb.DragCompleted += (s, e) =>
            {
                ScrollData data = this.HorizontalScrollBar.ChangeView(0);
                this.VerticalScrollBar.Cancel(data);
            };

            // Y
            this.YThumb.DragDelta += (s, e) =>
            {
                ScrollData data = this.VerticalScrollBar.ChangeView(e.VerticalChange);

                this.TranslateTransform.Y = -data.Offset;
                Canvas.SetTop(this.YThumb, data.Location);
            };
            this.YThumb.DragCompleted += (s, e) =>
            {
                ScrollData data = this.VerticalScrollBar.ChangeView(0);
                this.VerticalScrollBar.Cancel(data);
            };

            // Wheel
            base.PointerWheelChanged += (s, e) =>
            {
                switch (this.Orientation)
                {
                    case Orientation.Vertical:
                        {
                            if (this.VerticalScrollBar.IsScrollable is false) return;

                            PointerPoint pp = e.GetCurrentPoint(this);
                            PointerPointProperties prop = pp.Properties;

                            double delta = prop.MouseWheelDelta;
                            ScrollData data = this.VerticalScrollBar.ChangeView(-delta / 4);
                            this.VerticalScrollBar.Cancel(data);

                            this.YAnimation.To = -data.Offset;
                            this.YStoryboard.Begin(); // Storyboard

                            Canvas.SetTop(this.YThumb, data.Location);
                        }
                        break;
                    case Orientation.Horizontal:
                        {
                            if (this.HorizontalScrollBar.IsScrollable is false) return;

                            PointerPoint pp = e.GetCurrentPoint(this);
                            PointerPointProperties prop = pp.Properties;

                            double delta = prop.MouseWheelDelta;
                            ScrollData data = this.HorizontalScrollBar.ChangeView(-delta / 4);
                            this.HorizontalScrollBar.Cancel(data);

                            this.XAnimation.To =- data.Offset;
                            this.XStoryboard.Begin(); // Storyboard

                            Canvas.SetLeft(this.XThumb, data.Location);
                        }
                        break;
                    default:
                        break;
                }
            };
        }

        private void ContentSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize == Size.Empty) return;
            if (e.NewSize == e.PreviousSize) return;

            if (e.NewSize.Width != e.PreviousSize.Width)
                this.ScrollX(this.HorizontalScrollBar.ChangeView(this.HorizontalScrollBar.ViewportSize, e.NewSize.Width));

            if (e.NewSize.Height != e.PreviousSize.Height)
                this.ScrollY(this.VerticalScrollBar.ChangeView(this.VerticalScrollBar.ViewportSize, e.NewSize.Height));
        }

        private void ScrollX(ScrollData data)
        {
            this.TranslateTransform.X = -data.Offset;
            Canvas.SetLeft(this.XThumb, data.Location);

            this.XThumb.Width = data.Size;
            this.XThumb.IsEnabled = data.IsScrollable;
            this.XThumb.Visibility = data.IsScrollable ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ScrollY(ScrollData data)
        {
            this.TranslateTransform.Y = -data.Offset;
            Canvas.SetTop(this.YThumb, data.Location);

            this.YThumb.Height = data.Size;
            this.YThumb.IsEnabled = data.IsScrollable;
            this.YThumb.Visibility = data.IsScrollable ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}