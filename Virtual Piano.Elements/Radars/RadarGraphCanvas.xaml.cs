using System;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Elements
{
    public sealed partial class RadarGraphCanvas : Canvas
    {
        readonly RadarGraph R = new RadarGraph(-90);

        readonly TextBlock TextBlock0 = new TextBlock();
        readonly TextBlock TextBlock1 = new TextBlock();
        readonly TextBlock TextBlock2 = new TextBlock();
        readonly TextBlock TextBlock3 = new TextBlock();
        readonly TextBlock TextBlock4 = new TextBlock();
        readonly TextBlock TextBlock5 = new TextBlock();

        public string Text0 { get => this.TextBlock0.Text; set => this.TextBlock0.Text = value; }
        public string Text1 { get => this.TextBlock1.Text; set => this.TextBlock1.Text = value; }
        public string Text2 { get => this.TextBlock2.Text; set => this.TextBlock2.Text = value; }
        public string Text3 { get => this.TextBlock3.Text; set => this.TextBlock3.Text = value; }
        public string Text4 { get => this.TextBlock4.Text; set => this.TextBlock4.Text = value; }
        public string Text5 { get => this.TextBlock5.Text; set => this.TextBlock5.Text = value; }

        public RadarGraphCanvas()
        {
            this.InitializeComponent();

            base.Children.Add(this.TextBlock0);
            base.Children.Add(this.TextBlock1);
            base.Children.Add(this.TextBlock2);
            base.Children.Add(this.TextBlock3);
            base.Children.Add(this.TextBlock4);
            base.Children.Add(this.TextBlock5);

            this.AddPoint(this.R.L0);
            this.AddPoint(this.R.L1);
            this.AddPoint(this.R.L2);
            this.AddPoint(this.R.L3);
            this.AddPoint(this.R.L4);
            this.AddPoint(this.R.L5);

            this.AddLine(this.R.L0.P0, this.R.L3.P0);
            this.AddLine(this.R.L1.P0, this.R.L4.P0);
            this.AddLine(this.R.L2.P0, this.R.L5.P0);

            this.AddLines(this.R.L0.P4, this.R.L1.P4, this.R.L2.P4, this.R.L3.P4, this.R.L4.P4, this.R.L5.P4);
            this.AddLines(this.R.L0.P3, this.R.L1.P3, this.R.L2.P3, this.R.L3.P3, this.R.L4.P3, this.R.L5.P3);
            this.AddLines(this.R.L0.P2, this.R.L1.P2, this.R.L2.P2, this.R.L3.P2, this.R.L4.P2, this.R.L5.P2);
            this.AddLines(this.R.L0.P1, this.R.L1.P1, this.R.L2.P1, this.R.L3.P1, this.R.L4.P1, this.R.L5.P1);

            base.Children.Add(new Polygon
            {
                Points =
                {
                    this.R.L0.P0,
                    this.R.L1.P0,
                    this.R.L2.P0,
                    this.R.L3.P0,
                    this.R.L4.P0,
                    this.R.L5.P0,
                }
            });


            this.TextBlock0.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                if (e.NewSize.Width == e.PreviousSize.Width) return;

                Canvas.SetLeft(this.TextBlock0, this.R.L0.P0.X - e.NewSize.Width / 2);
                Canvas.SetTop(this.TextBlock0, this.R.L0.P0.Y - e.NewSize.Height - this.TextBlock0.FontSize / 2);
            };
            this.TextBlock1.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                if (e.NewSize.Width == e.PreviousSize.Width) return;

                Canvas.SetLeft(this.TextBlock1, this.R.L1.P0.X + this.TextBlock1.FontSize / 2);
                Canvas.SetTop(this.TextBlock1, this.R.L1.P0.Y - e.NewSize.Height / 2);
            };
            this.TextBlock2.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                if (e.NewSize.Width == e.PreviousSize.Width) return;

                Canvas.SetLeft(this.TextBlock2, this.R.L2.P0.X + this.TextBlock2.FontSize / 2);
                Canvas.SetTop(this.TextBlock2, this.R.L2.P0.Y - e.NewSize.Height / 2);
            };
            this.TextBlock3.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                if (e.NewSize.Width == e.PreviousSize.Width) return;

                Canvas.SetLeft(this.TextBlock3, this.R.L3.P0.X - e.NewSize.Width / 2);
                Canvas.SetTop(this.TextBlock3, this.R.L3.P0.Y + this.TextBlock3.FontSize / 2);
            };
            this.TextBlock4.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                if (e.NewSize.Width == e.PreviousSize.Width) return;

                Canvas.SetLeft(this.TextBlock4, this.R.L4.P0.X - e.NewSize.Width - this.TextBlock4.FontSize / 2);
                Canvas.SetTop(this.TextBlock4, this.R.L4.P0.Y - e.NewSize.Height / 2);
            };
            this.TextBlock5.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                if (e.NewSize.Width == e.PreviousSize.Width) return;

                Canvas.SetLeft(this.TextBlock5, this.R.L5.P0.X - e.NewSize.Width - this.TextBlock5.FontSize / 2);
                Canvas.SetTop(this.TextBlock5, this.R.L5.P0.Y - e.NewSize.Height / 2);
            };
        }

        private void AddLines(params Point[] points)
        {
            this.AddLine(points.Last(), points.First());
            for (int i = 1; i < points.Length; i++)
            {
                this.AddLine(points[i - 1], points[i]);
            }
        }
        private void AddLine(Point point1, Point point2)
        {
            this.Children.Add(new Line
            {
                X1 = point1.X,
                Y1 = point1.Y,
                X2 = point2.X,
                Y2 = point2.Y,
            });
        }

        private void AddPoint(RadarLine liner)
        {
            Point point = liner.P0;

            Ellipse ellipse = new Ellipse();
            Canvas.SetLeft(ellipse, point.X - 7);
            Canvas.SetTop(ellipse, point.Y - 7);
            this.Children.Add(ellipse);
        }
    }
}