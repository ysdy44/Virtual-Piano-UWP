﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Notes.Controls
{
    public interface IClickButton
    {
        object Tag { get; set; }
        int TabIndex { get; set; }
        Visibility Visibility { get; set; }
        Brush Foreground { get; set; }

        double Width { get; set; }
        double Height { get; set; }

        double X { get; set; }
        double Y { get; set; }
        ClickMode ClickMode { get; set; }

        void Add();
        void Clear();
    }
}