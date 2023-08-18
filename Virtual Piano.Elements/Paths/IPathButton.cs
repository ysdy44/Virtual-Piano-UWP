namespace Virtual_Piano.Elements
{
    public interface IPathButton
    {
        object CommandParameter { get; set; }

        void OnClick();
        void GoToState(bool value);
    }
}