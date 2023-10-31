namespace Virtual_Piano.Midi.Core
{
    public interface IKitButton : IClickButton
    {
        KitSet CommandParameter { get; set; }
    }
}