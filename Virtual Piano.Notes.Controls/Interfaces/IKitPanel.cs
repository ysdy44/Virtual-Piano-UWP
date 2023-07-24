using System.Windows.Input;

namespace Virtual_Piano.Midi.Controls
{
    public interface IKitPanel
    {
        ICommand Command { get; set; }

        void OnClick(KitSet set);
    }
}