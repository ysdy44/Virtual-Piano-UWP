using Windows.UI.Xaml.Controls.Primitives;

namespace Virtual_Piano.Midi.Core
{
    public interface ITrack
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
        long Position { get; }

        TimeSignature TimeSignature { get; }
        Ticks Ticks { get; }

        void Init(TimeSignature timeSignature, Ticks ticks);

        void ChangeDuration(long duration);
        void ChangePosition(long position, bool scrollNext, bool scrollPrevious);
        void Stop();
        void ChangePositionUI(long positionUI);
    }
}