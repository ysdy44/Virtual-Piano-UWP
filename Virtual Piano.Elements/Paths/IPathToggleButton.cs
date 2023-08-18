namespace Virtual_Piano.Elements
{
    public interface IPathToggleButton
    {
        object CheckedCommandParameter { get; set; }
        object UncheckedCommandParameter { get; set; }

        void OnClick();
    }
}