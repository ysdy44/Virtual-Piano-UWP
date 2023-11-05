using Virtual_Piano.Midi;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Controls
{
    public sealed class InstrumentItem : ContentPresenter
    {
        public MidiProgram Key
        {
            get => default;
            set => base.Content = $"{(int)value}. {value.ToString()}";
        }
    }
}