using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Elements
{
    /// <summary>
    /// <list type="number">
    ///     <itemheader>  
    ///         <term>Xaml</term>
    ///         <see cref="Canvas"/> container <see cref="Thumb"/> 
    ///     </itemheader>
    ///     <item>
    ///         <term><see cref="Canvas"/></term>
    ///         <see cref="FrameworkElement.SizeChanged"/> register <see cref="ScrollBar2.ChangeView(double, double)"/> 
    ///     </item>
    ///     <item>
    ///         <term><see cref="Thumb"/></term>
    ///         <see cref="Thumb.DragDelta"/> register <see cref="ScrollBar2.ChangeView(double)"/> 
    ///     </item>
    ///     <item>
    ///         <term><see cref="ScrollData"/></term>
    ///         <see cref="TranslateTransform"/> follow -<see cref="ScrollData.Offset"/> 
    ///         <see cref="Thumb"/> follow <see cref="ScrollData.Location"/>, <see cref="ScrollData.Size"/>, <see cref="ScrollData.IsScrollable"/> 
    ///     </item>
    /// </list>
    /// </summary>
    public sealed class ScrollBar2 : IScrollBar2
    {
        public bool IsScrollable { get; private set; }

        // Container
        public double ViewportSize { get; private set; }
        public double ExtentSize { get; private set; }

        // Content
        public double ScrollableSize => this.ExtentSize - this.ViewportSize;
        private double ActualOffset => System.Math.Max(0, System.Math.Min(this.ScrollableSize, this.Offset));
        public double Offset { get; private set; }

        // Bar
        double ActualLocation => System.Math.Max(0, System.Math.Min(this.ViewportSize - this.Size, this.Location));
        double Location;
        double Size;

        public ScrollData ChangeView(double change)
        {
            this.Location += change;
            this.Offset = this.ActualLocation * this.ExtentSize / this.ViewportSize;

            return new ScrollData
            {
                IsScrollable = true,
                Offset = this.ActualOffset,
                Location = this.ActualLocation,
                Size = this.Size
            };
        }

        public ScrollData ChangeView(double viewportSize, double extentSize)
        {
            this.ViewportSize = viewportSize;
            this.ExtentSize = extentSize;

            this.IsScrollable = this.ViewportSize < this.ExtentSize;
            this.Offset = this.IsScrollable ? System.Math.Max(0, System.Math.Min(this.ScrollableSize, this.Offset)) : 0;
            this.Location = this.IsScrollable ? this.ActualOffset * this.ViewportSize / this.ExtentSize : 0;
            this.Size = this.IsScrollable ? this.ViewportSize * this.ViewportSize / this.ExtentSize : this.ExtentSize;

            return new ScrollData
            {
                IsScrollable = this.IsScrollable,
                Offset = this.ActualOffset,
                Location = this.IsScrollable ? this.ActualLocation : 0,
                Size = this.Size
            };
        }

        public void Cancel(ScrollData data)
        {
            this.Offset = data.Offset;
            this.Location = data.Location;
        }
    }
}