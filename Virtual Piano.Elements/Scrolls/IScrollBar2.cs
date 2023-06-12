namespace Virtual_Piano.Elements
{
    public interface IScrollBar2
    {
        bool IsScrollable { get; }

        // Container
        double ViewportSize { get; }
        double ExtentSize { get; }

        // Content
        double ScrollableSize { get; }
        double Offset { get; }

        // Bar
        ScrollData ChangeView(double change);
        ScrollData ChangeView(double viewportSize, double extentSize);

        void Cancel(ScrollData data);
    }
}