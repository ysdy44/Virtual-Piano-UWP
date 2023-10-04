using System.Windows.Input;

namespace Virtual_Piano.Midi.Core
{
    public interface IControl
    {
        ICommand Command { get; }
        int Channel { get; }
    }
}