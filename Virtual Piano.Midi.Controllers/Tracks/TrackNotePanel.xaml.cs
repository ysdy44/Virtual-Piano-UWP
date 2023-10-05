using System.Linq;
using Virtual_Piano.Midi.Core;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using TrackLayout = Virtual_Piano.Midi.Core.TrackNoteLayout;

namespace Virtual_Piano.Midi.Controllers
{
    [ContentProperty(Name = nameof(Pane))]
    public sealed partial class TrackNotePanel : UserControl, ITrackNotePanel
    {
        //@Delegate
        public event DragStartedEventHandler DragStarted { remove => this.TimelineThumb.DragStarted -= value; add => this.TimelineThumb.DragStarted += value; }
        public event DragDeltaEventHandler DragDelta { remove => this.TimelineThumb.DragDelta -= value; add => this.TimelineThumb.DragDelta += value; }
        public event DragCompletedEventHandler DragCompleted { remove => this.TimelineThumb.DragCompleted -= value; add => this.TimelineThumb.DragCompleted += value; }
        public event ItemClickEventHandler FootItemClick { remove => this.FootListView.ItemClick -= value; add => this.FootListView.ItemClick += value; }
        public event RoutedEventHandler BackClick { remove => this.BackButton.Click -= value; add => this.BackButton.Click += value; }

        //@Converter
        private double HeightConverter(bool? value) => value is true ? this.Layout.ExtentHeightFoot : this.Layout.ExtentHeight;
        private Visibility VisibilityConverter(bool? value) => value is true ? Visibility.Visible : Visibility.Collapsed;

        // Container
        public int ViewportWidth { get; private set; }
        public int ViewportHeight { get; private set; }

        // public int ExtentWidth { get; private set; }
        // public int ExtentHeight { get; private set; }

        // Content
        public int HorizontalOffset { get; private set; }
        public int VerticalOffset { get; private set; }

        private int index;
        public int Index
        {
            get => this.index;
            set
            {
                if (this.index == value) return;
                this.index = value;

                for (int i = 0; i < this.TimelineTextCanvas.Children.Count; i++)
                {
                    TextBlock item = this.TimelineTextCanvas.Children[i] as TextBlock;
                    item.Text = $"{value + i}";
                }
            }
        }

        // UI
        public object ItemsSource { get => this.BodyItemsControl.ItemsSource; set => this.BodyItemsControl.ItemsSource = value; }
        public UIElement Pane { get => this.PaneBorder.Child; set => this.PaneBorder.Child = value; }
        public PointCollection ControllerPoints { get => this.ControllerPolyline.Points; set => this.ControllerPolyline.Points = value; }
        public object ControllerItemsSource { get => this.ControllerItemsControl.ItemsSource; set => this.ControllerItemsControl.ItemsSource = value; }
        public object FootItemsSource { get => this.FootListView.ItemsSource; set => this.FootListView.ItemsSource = value; }
        public object HeadItemsSource { get => this.HeadItemsControl.ItemsSource; set => this.HeadItemsControl.ItemsSource = value; }

        // Timeline
        public long Position { get; private set; }
        private long PositionUI;
        private long TimelineUI;

        public TimeSignature TimeSignature { get; private set; } = new TimeSignature(4, 4);
        public TimeSignatureTicks Ticks { get; private set; } = new TimeSignatureTicks(new TimeSignature(4, 4), new Tempo(120, 480));

        readonly TrackLayout Layout = new TrackLayout(75, 22, 22, 150);
        readonly Windows.UI.Composition.CompositionPropertySet ScrollProperties;

        ~TrackNotePanel() => this.ScrollProperties?.Dispose();
        public TrackNotePanel()
        {
            this.InitializeComponent();

            // Composition
            this.ScrollProperties = this.ScrollViewer.GetScroller();
            var ex = this.ScrollProperties.SnapScrollerX();
            var ey = this.ScrollProperties.SnapScrollerY();
            var ey3 = this.ScrollProperties.SnapScrollerY(1 - this.Layout.Timerline1);
            var sx = this.ScrollProperties.SnapScrollerX(this.Ticks.TicksPerBar / TrackLayout.Scaling, this.Layout.Pane);

            this.TimelinePoint.GetVisual().StartY(ey);
            this.TimelineLine.GetVisual().StartY(ey3);

            this.TimelineThumb.GetVisual().StartXY(ex, ey);
            this.HeadBorder.GetVisual().StartXY(ex, ey);

            this.PaneBorder.GetVisual().StartXY(ex, this.Layout.Head);
            this.BodyBackgroundCanvas.GetVisual().StartXY(ex, this.Layout.Head);

            this.BodyLineCanvas.GetVisual().StartXY(sx, ey);
            this.TimelineTextCanvas.GetVisual().StartXY(sx, ey);
            this.TimelinePointCanvas.GetVisual().StartXY(sx, ey);

            this.HeadItemsControl.GetVisual().StartXY(this.Layout.Pane, ey);
            this.FootListView.GetVisual().StartX(ex);


            // BodyLineCanvas
            this.BodyLineCanvas.Width = 16 * this.Ticks.TicksPerBar / TrackLayout.Scaling;
            for (int i = 0; i < 16; i++)
            {
                double x = i * this.Ticks.TicksPerBar / TrackLayout.Scaling;
                this.BodyLineCanvas.Children.Add(new Line
                {
                    StrokeThickness = 2,
                    Y1 = 0,
                    X1 = x,
                    X2 = x,
                    //Y2 = h
                });

                for (int j = 1; j < this.TimeSignature.Numerator; j++)
                {
                    double x2 = x + j * this.Ticks.TicksPerBeat / TrackLayout.Scaling;
                    this.BodyLineCanvas.Children.Add(new Line
                    {
                        StrokeThickness = 1,
                        Y1 = 0,
                        X1 = x2,
                        X2 = x2,
                        //Y2 = h
                    });
                }
            }

            // TimelinePointCanvas
            this.TimelinePointCanvas.Width = 16 * this.Ticks.TicksPerBar / TrackLayout.Scaling;
            for (int i = 0; i < 16; i++)
            {
                double x = i * this.Ticks.TicksPerBar / TrackLayout.Scaling;
                this.TimelinePointCanvas.Children.Add(new Line
                {
                    Y1 = 0,
                    X1 = x,
                    X2 = x,
                    Y2 = this.Layout.Head
                });

                for (int j = 0; j < this.TimeSignature.Numerator; j++)
                {
                    double x2 = x + j * this.Ticks.TicksPerBeat / TrackLayout.Scaling;
                    if (j != 0) this.TimelinePointCanvas.Children.Add(new Line
                    {
                        X1 = x2,
                        Y1 = this.Layout.Head - 9,
                        X2 = x2,
                        Y2 = this.Layout.Head
                    });

                    for (int k = 1; k < this.TimeSignature.Denominator; k++)
                    {
                        double x3 = x2 + k * this.Ticks.TicksPerBeat / this.TimeSignature.Denominator / TrackLayout.Scaling;
                        this.TimelinePointCanvas.Children.Add(new Line
                        {
                            X1 = x3,
                            Y1 = this.Layout.Head - 5,
                            X2 = x3,
                            Y2 = this.Layout.Head
                        });
                    }
                }
            }

            // TimelineTextCanvas
            this.TimelineTextCanvas.Width = 16 * this.Ticks.TicksPerBar / TrackLayout.Scaling;
            for (int i = 0; i < 16; i++)
            {
                double x = i * this.Ticks.TicksPerBar / TrackLayout.Scaling;
                TextBlock item = new TextBlock
                {
                    Text = $"{i}"
                };
                Canvas.SetLeft(item, x);
                this.TimelineTextCanvas.Children.Add(item);
            }


            // BodyBackgroundCanvas
            foreach (MidiNote item in System.Enum.GetValues(typeof(MidiNote)).Cast<MidiNote>())
            {
                switch (item.ToType())
                {
                    case ToneType.White:
                        break;
                    case ToneType.Black:
                        Rectangle rect = new Rectangle
                        {
                            Height = TrackLayout.ItemSize
                        };

                        int i = item.ToInedx();
                        int y = i * TrackLayout.ItemSize;

                        Canvas.SetTop(rect, y);
                        this.BodyBackgroundCanvas.Children.Add(rect);
                        break;
                    default:
                        break;
                }
            }

            foreach (MidiOctave item in System.Enum.GetValues(typeof(MidiOctave)).Cast<MidiOctave>())
            {
                int i = (int)item + 1;
                int y = i * TrackLayout.ItemSize * NoteExtensions.ToneCount;
                this.BodyBackgroundCanvas.Children.Add(new Line
                {
                    Y1 = y,
                    Y2 = y,
                });
            }


            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                int w = (int)e.NewSize.Width;
                if (this.ViewportWidth != w)
                {
                    this.ViewportWidth = w;

                    this.TimelineThumb.Width = w;
                    this.BodyBackgroundCanvas.Width = w;
                    foreach (FrameworkElement item in this.BodyBackgroundCanvas.Children.Cast<FrameworkElement>())
                    {
                        if (item is Line line)
                        {
                            line.X2 = w;
                        }
                        else
                        {
                            item.Width = w;
                        }
                    }
                }

                int h = (int)e.NewSize.Height;
                if (this.ViewportHeight != h)
                {
                    this.ViewportHeight = h;

                    this.TimelineLine.Y2 = h + this.Layout.Timerline1;
                    this.BodyLineCanvas.Height = h;
                    foreach (Line item in this.BodyLineCanvas.Children.Cast<Line>())
                    {
                        item.Y2 = h;
                    }

                    var sy = this.ScrollProperties.SnapScrollerY(h - this.Layout.Foot);
                    this.ControllerBorder.GetVisual().StartXY(this.Layout.Pane, sy);
                    this.FootListView.GetVisual().StartY(sy);
                }
            };

            base.PointerWheelChanged += (s, e) =>
            {
                PointerPoint pp = e.GetCurrentPoint(this);
                if (pp.Properties.IsHorizontalMouseWheel) return;

                int delta = pp.Properties.MouseWheelDelta;
                if (delta == 0) return;

                switch (e.KeyModifiers)
                {
                    case VirtualKeyModifiers.Control:
                        break;
                    case VirtualKeyModifiers.Shift:
                        double offset = this.HorizontalOffset - delta;
                        this.ScrollViewer.ChangeView(offset, null, null);
                        e.Handled = true;
                        break;
                    default:
                        break;
                }
            };

            this.TimelineThumb.PointerWheelChanged += (s, e) => e.Handled = true;
            this.HeadItemsControl.PointerWheelChanged += (s, e) => e.Handled = true;
            this.ControllerBorder.PointerWheelChanged += (s, e) =>
            {
                PointerPoint pp = e.GetCurrentPoint(this.ControllerBorder);
                if (pp.Properties.IsHorizontalMouseWheel) return;

                int delta = pp.Properties.MouseWheelDelta;
                if (delta == 0) return;

                double offset = this.HorizontalOffset - delta;
                this.ScrollViewer.ChangeView(offset, null, null);

                e.Handled = true;
            };

            this.ScrollViewer.ViewChanging += (s, e) =>
            {
                this.HorizontalOffset = (int)e.FinalView.HorizontalOffset;
                this.VerticalOffset = (int)e.FinalView.VerticalOffset;
                this.Index = this.HorizontalOffset * TrackLayout.Scaling / this.Ticks.TicksPerBar;
            };
        }

        public void Init(TimeSignature timeSignature, TimeSignatureTicks ticks)
        {
            this.TimeSignature = timeSignature;
            this.Ticks = ticks;
            this.Index = this.HorizontalOffset * TrackLayout.Scaling / this.Ticks.TicksPerBar;


            // Composition
            var ey = this.ScrollProperties.SnapScrollerY();
            var sx = this.ScrollProperties.SnapScrollerX(this.Ticks.TicksPerBar, this.Layout.Pane);

            var bl = this.BodyLineCanvas.GetVisual();
            bl.StopX();
            bl.StopY();
            bl.StartXY(sx, ey);

            var tt = this.TimelineTextCanvas.GetVisual();
            tt.StopX();
            tt.StopY();
            tt.StartXY(sx, ey);

            var tp = this.TimelinePointCanvas.GetVisual();
            tp.StopX();
            tp.StopY();
            tp.StartXY(sx, ey);


            // BodyLineCanvas
            int h = (int)base.ActualHeight;
            this.BodyLineCanvas.Children.Clear();
            this.BodyLineCanvas.Width = 16 * this.Ticks.TicksPerBar / TrackLayout.Scaling;
            for (int i = 0; i < 16; i++)
            {
                double x = i * this.Ticks.TicksPerBar / TrackLayout.Scaling;
                this.BodyLineCanvas.Children.Add(new Line
                {
                    StrokeThickness = 2,
                    Y1 = 0,
                    X1 = x,
                    X2 = x,
                    Y2 = h,
                });

                for (int j = 1; j < this.TimeSignature.Numerator; j++)
                {
                    double x2 = x + j * this.Ticks.TicksPerBeat / TrackLayout.Scaling;
                    this.BodyLineCanvas.Children.Add(new Line
                    {
                        StrokeThickness = 1,
                        Y1 = 0,
                        X1 = x2,
                        X2 = x2,
                        Y2 = h,
                    });
                }
            }

            // TimelinePointCanvas
            this.TimelinePointCanvas.Children.Clear();
            this.TimelinePointCanvas.Width = 16 * this.Ticks.TicksPerBar / TrackLayout.Scaling;
            for (int i = 0; i < 16; i++)
            {
                double x = i * this.Ticks.TicksPerBar / TrackLayout.Scaling;
                this.TimelinePointCanvas.Children.Add(new Line
                {
                    Y1 = 0,
                    X1 = x,
                    X2 = x,
                    Y2 = this.Layout.Head
                });

                for (int j = 0; j < this.TimeSignature.Numerator; j++)
                {
                    double x2 = x + j * this.Ticks.TicksPerBeat / TrackLayout.Scaling;
                    if (j != 0) this.TimelinePointCanvas.Children.Add(new Line
                    {
                        X1 = x2,
                        Y1 = this.Layout.Head - 9,
                        X2 = x2,
                        Y2 = this.Layout.Head
                    });

                    for (int k = 1; k < this.TimeSignature.Denominator; k++)
                    {
                        double x3 = x2 + k * this.Ticks.TicksPerBeat / this.TimeSignature.Denominator / TrackLayout.Scaling;
                        this.TimelinePointCanvas.Children.Add(new Line
                        {
                            X1 = x3,
                            Y1 = this.Layout.Head - 5,
                            X2 = x3,
                            Y2 = this.Layout.Head
                        });
                    }
                }
            }

            // TimelineTextCanvas
            this.TimelineTextCanvas.Children.Clear();
            this.TimelineTextCanvas.Width = 16 * this.Ticks.TicksPerBar / TrackLayout.Scaling;
            for (int i = 0; i < 16; i++)
            {
                double x = i * this.Ticks.TicksPerBar / TrackLayout.Scaling;
                TextBlock item = new TextBlock
                {
                    Text = $"{i}"
                };
                Canvas.SetLeft(item, x);
                this.TimelineTextCanvas.Children.Add(item);
            }
        }

        public void ChangeDuration(long duration)
        {
            double extentWidth = duration / TrackLayout.Scaling;
            this.BodyItemsControl.Width = extentWidth;
            this.Canvas.Width = extentWidth + this.Layout.Pane;
            this.ControllerBorder.Width = extentWidth;
            this.ControllerItemsControl.Width = extentWidth;
            this.HeadItemsControl.Width = extentWidth;
        }

        public void ChangePosition(long position, bool scrollNext, bool scrollPrevious)
        {
            this.Position = System.Math.Max(0, position);
            this.PositionUI = position / TrackLayout.Scaling;
            this.TimelineUI = this.PositionUI + this.Layout.Pane;

            // UI
            this.TimelineLine.X1 = this.TimelineUI;
            this.TimelineLine.X2 = this.TimelineUI;
            this.TimelinePoint.X1 = this.TimelineUI;
            this.TimelinePoint.X2 = this.TimelineUI;

            if (scrollNext && this.TimelineUI >= this.HorizontalOffset + this.ViewportWidth)
            {
                this.ScrollViewer.ChangeView(this.TimelineUI - this.Layout.Pane, null, null, true);
            }
            else if (scrollPrevious && this.TimelineUI < this.HorizontalOffset)
            {
                this.ScrollViewer.ChangeView(this.TimelineUI - this.Layout.Pane, null, null, true);
            }
        }

        public void Stop()
        {
            this.Position = 0;
            this.PositionUI = 0;
            this.TimelineUI = this.Layout.Pane;

            // UI
            this.TimelineLine.X1 = this.Layout.Pane;
            this.TimelineLine.X2 = this.Layout.Pane;
            this.TimelinePoint.X1 = this.Layout.Pane;
            this.TimelinePoint.X2 = this.Layout.Pane;

            this.ScrollViewer.ChangeView(0, null, null, true);
        }

        public void ChangePositionUI(long positionUI)
        {
            if (positionUI > this.Layout.Pane)
            {
                this.Position = this.PositionUI * TrackLayout.Scaling;
                this.PositionUI = this.TimelineUI - this.Layout.Pane;
                this.TimelineUI = System.Math.Max(this.Layout.Pane, positionUI);

                // UI
                this.TimelineLine.X1 = this.TimelineUI;
                this.TimelineLine.X2 = this.TimelineUI;
                this.TimelinePoint.X1 = this.TimelineUI;
                this.TimelinePoint.X2 = this.TimelineUI;
            }
            else
            {
                this.Position = 0;
                this.PositionUI = 0;
                this.TimelineUI = this.Layout.Pane;

                // UI
                this.TimelineLine.X1 = this.Layout.Pane;
                this.TimelineLine.X2 = this.Layout.Pane;
                this.TimelinePoint.X1 = this.Layout.Pane;
                this.TimelinePoint.X2 = this.Layout.Pane;
            }
        }

        public void Load(Track track)
        {
            if (track is null)
            {
                this.ChangeDuration(this.Layout.ExtentHeight * TrackLayout.Scaling);
                this.ItemsSource = null;
                this.HeadItemsSource = null;
                this.FootItemsSource = null;
                this.LoadCC(null);
            }
            else
            {
                this.ChangeDuration(track.Source.Duration);
                this.ItemsSource = track.Notes;
                this.HeadItemsSource = track.Programs;
                this.FootItemsSource = track.Controllers.Keys;
                if (track.Controllers.Count is 0)
                    this.LoadCC(null);
                else
                    this.LoadCC(track.Controllers.Values.First());
            }
        }

        public void LoadCC(ControllerCollection cc)
        {
            if (cc is null)
            {
                this.ControllerItemsSource = null;
                this.ControllerPoints = null;
            }
            else
            {
                this.ControllerItemsSource = cc.ItemsSource;
                this.ControllerPoints = cc.Points;
            }
        }
    }
}