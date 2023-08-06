using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Elements
{
    public static class XamlExtensions
    {
        //const string xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
        //const string d = "http://schemas.microsoft.com/expression/blend/2008";
        //const string x = "http://schemas.microsoft.com/winfx/2006/xaml";
        //const string mc = "http://schemas.openxmlformats.org/markup-compatibility/2006";

        //const string Head = $"<Geometry xmlns=\"{xmlns}\" xmlns:d=\"{d}\" xmlns:x=\"{x}\" xmlns:mc=\"{mc}\">";
        const string Head = "<Geometry xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\">";
        const string Foot = "</Geometry>";

        public static Geometry ToGeometry(this object data)
            => XamlReader.Load($"{XamlExtensions.Head}{data}{XamlExtensions.Foot}") as Geometry;

        public static Geometry FindGeometry(this ResourceDictionary dictionary, string dateKey)
            => dictionary.ContainsKey(dateKey)
            && dictionary[dateKey] is string data
            && XamlReader.Load($"{XamlExtensions.Head}{data}{XamlExtensions.Foot}") is Geometry geometry
            ? geometry
            : null;
    }
}