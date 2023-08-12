using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace Virtual_Piano.Midi.Core
{
    /// <summary>
    /// The <see cref="UIElement"/> of <see cref="Visual"/> without:
    /// <list type="number">
    ///     <item><see cref="FrameworkElement.Margin"/></item>
    ///     <item><see cref="Canvas.SetLeft(UIElement, double)"/></item>
    ///     <item><see cref="Canvas.SetTop(UIElement, double)"/></item>
    /// </list>
    /// </summary>
    public static partial class CompositionExtensions
    {
        // Expression
        const string Scroller = "scroller";
        const string TranslationX = "scroller.Translation.X";
        const string TranslationY = "scroller.Translation.Y";
        const string ReverseTranslationX = "-scroller.Translation.X";
        const string ReverseTranslationY = "-scroller.Translation.Y";
        const string OffsetX = "Offset.X";
        const string OffsetY = "Offset.Y";

        public static CompositionPropertySet GetScroller(this ScrollViewer scrollViewer) => ElementCompositionPreview.GetScrollViewerManipulationPropertySet(scrollViewer);
        public static Visual GetVisual(this UIElement element) => ElementCompositionPreview.GetElementVisual(element);
        private static ExpressionAnimation ToExpression(this CompositionPropertySet propertySet, string key)
        {
            ExpressionAnimation expression = propertySet.Compositor.CreateExpressionAnimation(key);
            expression.SetReferenceParameter(CompositionExtensions.Scroller, propertySet);
            return expression;
        }

        // Snap
        public static ExpressionAnimation SnapScrollerX(this CompositionPropertySet propertySet) => propertySet.ToExpression(ReverseTranslationX);
        public static ExpressionAnimation SnapScrollerY(this CompositionPropertySet propertySet) => propertySet.ToExpression(ReverseTranslationY);

        public static ExpressionAnimation SnapScrollerX(this CompositionPropertySet propertySet, float offsetX) => propertySet.ToExpression($"{offsetX}-{TranslationX}");
        public static ExpressionAnimation SnapScrollerY(this CompositionPropertySet propertySet, float offsetY) => propertySet.ToExpression($"{offsetY}-{TranslationY}");

        public static ExpressionAnimation SnapScrollerX(this CompositionPropertySet propertySet, float denominatorX, float offsetX) => propertySet.ToExpression($"{offsetX}+{TranslationX}%{denominatorX}-{TranslationX}");
        public static ExpressionAnimation SnapScrollerY(this CompositionPropertySet propertySet, float denominatorY, float offsetY) => propertySet.ToExpression($"{offsetY}+{TranslationY}%{denominatorY}-{TranslationY}");

        // Offset
        public static void AnimationX(this Visual visual, ExpressionAnimation expression) => visual.StartAnimation(OffsetX, expression);
        public static void AnimationY(this Visual visual, ExpressionAnimation expression) => visual.StartAnimation(OffsetY, expression);

        public static void AnimationXY(this Visual visual, ExpressionAnimation x, ExpressionAnimation y)
        {
            visual.AnimationX(x);
            visual.AnimationY(y);
        }
        public static void AnimationXY(this Visual visual, float x, ExpressionAnimation y)
        {
            visual.Offset = new System.Numerics.Vector3(x, 0, 0); // 1. float
            visual.AnimationY(y); // 2. ExpressionAnimation
        }
        public static void AnimationXY(this Visual visual, ExpressionAnimation x, float y)
        {
            visual.Offset = new System.Numerics.Vector3(0, y, 0); // 1. float
            visual.AnimationX(x); // 2. ExpressionAnimation
        }
    }
}