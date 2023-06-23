using System;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Input;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Elements
{
    public sealed partial class Carousel : UserControl
    {
        //@Delegate
        public event EventHandler<int> ItemClick;

        public int Count => this.ItemsControl.Items == null ? 0 : this.ItemsControl.Items.Count;
        public int SelectedIndex { get; set; } = -1;

        int Index = -1;
        double Y;

        //@Construct
        public Carousel()
        {
            this.InitializeComponent();
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                double w = e.NewSize.Width;
                this.Rectangle.Width = w;
                this.ItemsControl.Width = w;
            };

            // Manipulation
            base.ManipulationStarted += (s, e) =>
            {
            };
            base.ManipulationDelta += (s, e) =>
            {
                this.Y += e.Delta.Translation.Y;

                if (this.Y > 0) this.Y = 0;
                if (this.Y < 40 - this.Count * 40) this.Y = 40 - this.Count * 40;
                Canvas.SetTop(this.ItemsControl, this.Y);

                if (e.IsInertial is false) return;
                this.Index = (int)System.Math.Round(-this.Y / 40, MidpointRounding.AwayFromZero);

                if (this.Index < 0) e.Complete();
                if (this.Index > this.Count - 1) e.Complete();
            };
            base.ManipulationCompleted += (s, e) =>
            {
                this.Index = (int)System.Math.Round(-this.Y / 40, MidpointRounding.AwayFromZero);

                if (this.Index < 0) this.Index = 0;
                if (this.Index > this.Count - 1) this.Index = this.Count - 1;

                this.Y = 40 * -this.Index;
                this.YAnimation.To = this.Y;
                this.YStoryboard.Begin(); // Storyboard

                if (this.SelectedIndex == this.Index) return;
                this.SelectedIndex = this.Index;
                this.ItemClick?.Invoke(this, this.SelectedIndex); // Delegate
            };

            // Key
            base.KeyDown += (s, e) =>
            {
                switch (e.Key)
                {
                    case VirtualKey.Up:
                    case VirtualKey.Left:
                    case VirtualKey.Down:
                    case VirtualKey.Right:
                        switch (e.Key)
                        {
                            case VirtualKey.Up: this.Index -= 1; break;
                            case VirtualKey.Left: this.Index -= 1; break;
                            case VirtualKey.Down: this.Index += 1; break;
                            case VirtualKey.Right: this.Index += 1; break;
                            default: break;
                        }

                        if (this.Index < 0) this.Index = 0;
                        if (this.Index > this.Count - 1) this.Index = this.Count - 1;

                        this.Y = 40 * -this.Index;
                        this.YAnimation.To = this.Y;
                        this.YStoryboard.Begin(); // Storyboard

                        if (this.SelectedIndex == this.Index) break;
                        this.SelectedIndex = this.Index;
                        this.ItemClick?.Invoke(this, this.SelectedIndex); // Delegate
                        break;
                    default:
                        break;
                }
            };

            // Wheel
            base.PointerWheelChanged += (s, e) =>
            {
                PointerPoint pp = e.GetCurrentPoint(this);
                PointerPointProperties prop = pp.Properties;

                double delta = prop.MouseWheelDelta;
                this.Index += System.Math.Clamp((int)-delta, -1, 1);

                if (this.Index < 0) this.Index = 0;
                if (this.Index > this.Count - 1) this.Index = this.Count - 1;

                this.Y = 40 * -this.Index;
                this.YAnimation.To = this.Y;
                this.YStoryboard.Begin(); // Storyboard

                if (this.SelectedIndex == this.Index) return;
                this.SelectedIndex = this.Index;
                this.ItemClick?.Invoke(this, this.SelectedIndex); // Delegate
            };

            // Pointer
            this.ItemsControl.Tapped += (s, e) =>
            {
                Point p = e.GetPosition(this.ItemsControl);
                this.Y = p.Y - 20;

                this.Index = (int)System.Math.Round(this.Y / 40, MidpointRounding.AwayFromZero);

                if (this.Index < 0) this.Index = 0;
                if (this.Index > this.Count - 1) this.Index = this.Count - 1;

                this.Y = 40 * -this.Index;
                this.YAnimation.To = this.Y;
                this.YStoryboard.Begin(); // Storyboard

                if (this.SelectedIndex == this.Index) return;
                this.SelectedIndex = this.Index;
                this.ItemClick?.Invoke(this, this.SelectedIndex); // Delegate
            };
        }

        public void Reset(int index = 0)
        {
            this.Y = index * 40;
            Canvas.SetTop(this.ItemsControl, this.Y);

            this.Index = index;
            this.SelectedIndex = index;
        }
        public void Reset(object itemSource, int index = 0)
        {
            this.ItemsControl.ItemsSource = itemSource;
            this.Reset(index);
        }
    }
}