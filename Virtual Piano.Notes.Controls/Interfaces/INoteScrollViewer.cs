using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Notes.Controls
{
    public interface INoteScrollViewer
    {
        int ItemSize { get; set; }
        ScrollViewer ScrollViewer { get; set; }

        void PageDown();
        void PageUp();
    }
}