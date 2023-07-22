using System.Windows.Input;

namespace Virtual_Piano.Notes.Controls
{
    public interface IKitPanel
    {
        ICommand Command { get; set; }

        void OnClick(KitSet set);
    }
}