using Windows.UI.Xaml.Markup;

namespace Virtual_Piano.Strings
{
    [MarkupExtensionReturnType(ReturnType = typeof(string))]
    public class OptionExtension : MarkupExtension
    {
        public OptionType Type { get; set; }
        protected override object ProvideValue() => $"{this.Type}";
    }
}