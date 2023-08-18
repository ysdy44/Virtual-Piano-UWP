using Windows.UI.Xaml;

namespace Virtual_Piano.Elements
{
    public sealed class PathToggleButton : PathCheckedButton, IPathToggleButton
    {
        //@Command
        public object UncheckedCommandParameter { get; set; }
        public object CheckedCommandParameter { get; set; }

        #region DependencyProperty

        /// <summary> Gets or set the type for <see cref="PathToggleButton"/>. </summary>
        public bool IsChecked
        {
            get => (bool)base.GetValue(IsCheckedProperty);
            set => base.SetValue(IsCheckedProperty, value);
        }
        /// <summary> Identifies the <see cref = "PathToggleButton.IsChecked" /> dependency property. </summary>
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(nameof(IsChecked), typeof(bool), typeof(PathToggleButton), new PropertyMetadata(false, (sender, e) =>
        {
            PathToggleButton control = (PathToggleButton)sender;

            if (e.NewValue is bool value)
            {
                control.GoToState(control.IsPressed, value);
            }
        }));

        #endregion

        //@Override
        public override void GoToState(bool value) => base.GoToState(value, this.IsChecked);
        public override void OnClick()
        {
            if (this.IsChecked)
            {
                //ElementSoundPlayer.Play(ElementSoundKind.Invoke);
                base.Command?.Execute(this.CheckedCommandParameter); // Command
            }
            else
            {
                ElementSoundPlayer.Play(ElementSoundKind.Invoke);
                base.Command?.Execute(this.UncheckedCommandParameter); // Command
            }
        }
    }
}