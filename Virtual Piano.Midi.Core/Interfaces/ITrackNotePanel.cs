using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Core
{
    public interface ITrackNotePanel : ITrackPanelBase
    {
        //@Delegate
        event ItemClickEventHandler FootItemClick;
        event RoutedEventHandler BackClick;

        // UI
        PointCollection ControllerPoints { get; set; }
        object ControllerItemsSource { get; set; }
        object FootItemsSource { get; set; }
        object HeadItemsSource { get; set; }
    }
}