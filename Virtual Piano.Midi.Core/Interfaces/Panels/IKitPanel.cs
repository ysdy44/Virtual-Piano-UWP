namespace Virtual_Piano.Midi.Core
{
    public interface IKitPanel
    {
        void OnClick(KitSet set);

        void Execute(KitSet set);
    }
}