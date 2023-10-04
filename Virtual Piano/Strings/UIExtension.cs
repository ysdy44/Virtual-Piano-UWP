using Windows.UI.Xaml.Markup;

namespace Virtual_Piano.Strings
{
    [MarkupExtensionReturnType(ReturnType = typeof(string))]
    public class UIExtension : MarkupExtension
    {
        public UIType Type { get; set; }
        protected override object ProvideValue() =>this.Type.GetString();
    }
}