using Windows.UI.Xaml;

namespace Virtual_Piano.Elements
{
    [TemplateVisualState(Name = Normal, GroupName = CommonStates)]
    [TemplateVisualState(Name = Pressed, GroupName = CommonStates)]
    public sealed class PathButton : PathButtonBase, IPathButton
    {
        //@Const
        const string CommonStates = "CommonStates";
        const string Normal = "Normal";
        const string Pressed = "Pressed";

        //@Command
        public object CommandParameter { get; set; }

        //@Construct
        public PathButton()
        {
            base.DefaultStyleKey = typeof(PathButton);
        }

        //@Override
        public override void OnClick()
        {
            ElementSoundPlayer.Play(ElementSoundKind.Invoke);

            base.Command?.Execute(this.CommandParameter); // Command
        }
        public override void GoToState(bool value)
        {
            if (value)
                VisualStateManager.GoToState(this, Pressed, false);
            else
                VisualStateManager.GoToState(this, Normal, false);
        }
    }
}