using Windows.UI.Xaml.Controls.Primitives;

namespace Virtual_Piano.Elements
{
    public interface IPathFlyoutButton
    {
        FlyoutBase Flyout { get; set; }

        void OnClick();
    }
}