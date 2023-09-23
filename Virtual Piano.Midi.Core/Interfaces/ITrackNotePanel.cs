using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Core
{
    public interface ITrackPanelBase
    {
        //@Delegate
        event DragStartedEventHandler DragStarted;
        event DragDeltaEventHandler DragDelta;
        event DragCompletedEventHandler DragCompleted;

        // Container
        int ViewportWidth { get; }
        int ViewportHeight { get; }

        // Content
        int HorizontalOffset { get; }
        int VerticalOffset { get; }

        int Index { get; set; }

        // UI
        object ItemsSource { get; set; }

        // Timeline
        int Position { get; }

        void ChangeDuration(int duration);
        void ChangePosition(int position, bool scrollNext, bool scrollPrevious);
        void Stop();
        void ChangePositionUI(int positionUI);
    }

    public interface ITrackPanel : ITrackPanelBase
    {
        //@Delegate
        event EventHandler<int> ItemClick;
    }

    public interface ITrackNotePanel : ITrackPanelBase
    {
        //@Delegate
        event ItemClickEventHandler FootItemClick;
        event RoutedEventHandler BackClick;

        // UI
        UIElement Pane { get; set; }
        PointCollection ControllerPoints { get; set; }
        object ControllerItemsSource { get; set; }
        object FootItemsSource { get; set; }
        object HeadItemsSource { get; set; }
    }
}