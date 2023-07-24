namespace Virtual_Piano.Midi.Controls
{
    public interface IKitButton : IClickButton
    {
        KitSet CommandParameter { get; set; }
    }
}