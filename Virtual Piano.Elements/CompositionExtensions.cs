using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace Virtual_Piano.Elements
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
        public static CompositionPropertySet GetManipulation(this ScrollViewer scrollViewer) => ElementCompositionPreview.GetScrollViewerManipulationPropertySet(scrollViewer);
        public static Visual GetVisual(this UIElement element) => ElementCompositionPreview.GetElementVisual(element);

        // Offset
        public static void Offset(this Visual visual, float x, float y, float z = 0) => visual.Offset = new System.Numerics.Vector3(x, y, z);
        public static void OffsetX(this Visual visual, float value) => visual.Offset(value, 0, 0);
        public static void OffsetY(this Visual visual, float value) => visual.Offset(0, value, 0);
        public static void OffsetZ(this Visual visual, float value) => visual.Offset(0, 0, value);

        public static void OffsetX(this Visual visual, ExpressionAnimation expression) => visual.StartAnimation(CompositionExtensions.X, expression);
        public static void OffsetY(this Visual visual, ExpressionAnimation expression) => visual.StartAnimation(CompositionExtensions.Y, expression);

        public static void Offset(this Visual visual, ExpressionAnimation x, ExpressionAnimation y)
        {
            visual.OffsetX(x);
            visual.OffsetY(y);
        }
        public static void Offset(this Visual visual, float x, ExpressionAnimation y)
        {
            visual.OffsetX(x); // 1. float
            visual.OffsetY(y); // 2. ExpressionAnimation
        }
        public static void Offset(this Visual visual, ExpressionAnimation x, float y)
        {
            visual.OffsetY(y); // 1. float
            visual.OffsetX(x); // 2. ExpressionAnimation
        }
  
        // Expression
        const string ProgressX = "-scroller.Translation.X";
        const string ProgressY = "-scroller.Translation.Y";
        const string X = "Offset.X";
        const string Y = "Offset.Y";
        const string Scroller = "scroller";

        private static ExpressionAnimation ToExpression(this CompositionPropertySet propertySet, string key)
        {
            ExpressionAnimation expression = propertySet.Compositor.CreateExpressionAnimation(key);
            expression.SetReferenceParameter(CompositionExtensions.Scroller, propertySet);
            return expression;
        }

        public static ExpressionAnimation ExpressionX(this CompositionPropertySet propertySet) => propertySet.ToExpression(CompositionExtensions.ProgressX);
        public static ExpressionAnimation ExpressionY(this CompositionPropertySet propertySet) => propertySet.ToExpression(CompositionExtensions.ProgressY);

        public static ExpressionAnimation ExpressionX(this CompositionPropertySet propertySet, float value) => propertySet.ToExpression($"{CompositionExtensions.ProgressX}+{value}");
        public static ExpressionAnimation ExpressionY(this CompositionPropertySet propertySet, float value) => propertySet.ToExpression($"{CompositionExtensions.ProgressY}+{value}");

        public static ExpressionAnimation ExpressionX(this CompositionPropertySet propertySet, float denominator, float value) => propertySet.ToExpression($"{CompositionExtensions.ProgressX}+{value}-{CompositionExtensions.ProgressX}%{denominator}");
        public static ExpressionAnimation ExpressionY(this CompositionPropertySet propertySet, float denominator, float value) => propertySet.ToExpression($"{CompositionExtensions.ProgressX}+{value}-{CompositionExtensions.ProgressY}%{denominator}");
    }
}