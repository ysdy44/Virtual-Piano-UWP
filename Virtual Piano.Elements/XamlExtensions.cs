﻿using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Elements
{
    public static class XamlExtensions
    {
        public static Geometry ToGeometry(this object data) => XamlReader.Load
        (
            $"<Geometry xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\">{data}</Geometry>"
        ) as Geometry;
    }
}