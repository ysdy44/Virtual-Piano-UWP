using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Core
{
    public interface ITrackNotePanel
    {
        //@Delegate
        event DragStartedEventHandler DragStarted;
        event DragDeltaEventHandler DragDelta;
        event DragCompletedEventHandler DragCompleted;
        event ItemClickEventHandler FootItemClick;
        event RoutedEventHandler BackClick;

        // Container
        int ViewportWidth { get; }
        int ViewportHeight { get; }

        // Content
        int HorizontalOffset { get; }
        int VerticalOffset { get; }

        int Index { get; set; }

        // UI
        object ItemsSource { get; set; }
        UIElement Pane { get; set; }
        PointCollection ControllerPoints { get; set; }
        object ControllerItemsSource { get; set; }
        object FootItemsSource { get; set; }
        object HeadItemsSource { get; set; }

        // Timeline
        int Time { get; }

        void ChangeDuration(int time);
        void ChangePosition(int timeline);

        int UpdateTimeline(int timeline);
    }
}