using System;
using Windows.Foundation;
using Windows.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Virtual_Piano.Midi.Controllers
{
    [ContentProperty(Name = nameof(Text))]
    public abstract partial class BendWheel : UserControl
    {
        double Y;
        double W;
        double WHalf;
        double H;

        readonly int DefaultValue;

        private int index;
        public int Index
        {
            get => this.index;
            set
            {
                if (this.index == value) return;
                this.index = value;
                this.Update(value);
                this.Execute(value);
            }
        }

        // UI
        public string Text { get => this.TextBlock2.Text; set => this.TextBlock2.Text = value; }

        public BendWheel(int defaultValue)
        {
            this.InitializeComponent();
            this.DefaultValue = defaultValue;
            this.index = defaultValue;
            this.Update(defaultValue);
            this.Canvas.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                this.W = e.NewSize.Width;
                this.WHalf = this.W / 2;
                this.H = e.NewSize.Height;

                this.Rectangle.Width = this.W;
                this.Rectangle.Height = this.W;

                this.Update(this.Index);
            };

            // Wheel
            this.Thumb.PointerWheelChanged += (s, e) =>
            {
                PointerPoint pp = e.GetCurrentPoint(this.Thumb);
                PointerPointProperties prop = pp.Properties;

                double delta = prop.MouseWheelDelta;
                int index = this.Index + System.Math.Clamp((int)delta, -1, 1);
                this.Index = Math.Clamp(index, 0, 127);

                e.Handled = true;
            };

            // Thumb
            this.Thumb.DragStarted += (s, e) =>
            {
                this.Y = e.VerticalOffset;
                this.Position(this.Y);
                this.Stop();
            };
            this.Thumb.DragDelta += (s, e) =>
            {
                this.Y += e.VerticalChange;
                this.Position(this.Y);
            };
            this.Thumb.DragCompleted += (s, e) =>
            {
                if (this.Index == this.DefaultValue)
                    this.Stop();
                else
                    this.Start();
            };
        }

        public abstract void Stop();
        public abstract void Start();
        public abstract void Execute(int value);
        private void Update(int value)
        {
            double top = (this.H - this.W) * (127 - value) / 127;
            this.TextBlock1.Text = $"{value}";
            Canvas.SetTop(this.Rectangle, top);
        }

        private void Position(double y)
        {
            int index = (int)((y - this.W - this.WHalf) / (this.H - this.W) * 127);
            this.Index = Math.Clamp(127 - index, 0, 127);
        }
    }
}