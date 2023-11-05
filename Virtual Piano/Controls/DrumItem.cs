using Virtual_Piano.Midi;
using Virtual_Piano.Strings;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Controls
{
    public sealed class DrumItem : ContentPresenter
    {
        public MidiPercussionProgram Key
        {
            get => default;
            set => base.Content = $"{(int)value}. {value.GetString()}";
        }
    }
}