using Windows.UI.Xaml;

namespace Virtual_Piano.Elements
{
    [TemplateVisualState(Name = UncheckedNormal, GroupName = CombinedStates)]
    [TemplateVisualState(Name = UncheckedPressed, GroupName = CombinedStates)]
    [TemplateVisualState(Name = CheckedNormal, GroupName = CombinedStates)]
    [TemplateVisualState(Name = CheckedPressed, GroupName = CombinedStates)]
    public abstract class PathCheckedButton : PathButtonBase
    {
        //@Const
        const string CombinedStates = "CombinedStates";
        const string UncheckedNormal = "UncheckedNormal";
        const string UncheckedPressed = "UncheckedPressed";
        const string CheckedNormal = "CheckedNormal";
        const string CheckedPressed = "CheckedPressed";

        //@Construct
        protected PathCheckedButton()
        {
            base.DefaultStyleKey = typeof(PathCheckedButton);
        }

        //@Override
        protected void GoToState(bool isPressed, bool isChecked)
        {
            if (isChecked)
            {
                if (isPressed)
                    VisualStateManager.GoToState(this, CheckedPressed, false);
                else
                    VisualStateManager.GoToState(this, CheckedNormal, false);
            }
            else
            {
                if (isPressed)
                    VisualStateManager.GoToState(this, UncheckedPressed, false);
                else
                    VisualStateManager.GoToState(this, UncheckedNormal, false);
            }
        }
    }
}