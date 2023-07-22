namespace Virtual_Piano.Notes.Controls
{
    public interface IKitButton : IClickButton
    {
        KitSet CommandParameter { get; set; }
    }
}