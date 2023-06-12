using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Notes.Controls
{
    public interface IClickButton
    {
        object Tag { get; set; }
        int TabIndex { get; set; }
        double Width { get; set; }
        double Height { get; set; }

        double X { get; set; }
        double Y { get; set; }
        ClickMode ClickMode { get; set; }

        void Add();
        void Clear();
    }
}