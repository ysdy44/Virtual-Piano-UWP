using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Core
{
    public interface ITrackNotePanel : ITrack
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