using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace Virtual_Piano.Elements
{
    public sealed class PathFlyoutButton : PathCheckedButton, IPathFlyoutButton
    {
        private FlyoutBase flyout;
        public FlyoutBase Flyout
        {
            get => this.flyout;
            set
            {
                if (this.flyout != null)
                {
                    this.flyout.Closed -= this.Closed;
                    this.flyout.Opened -= this.Opened;
                }
                this.flyout = value;
                if (this.flyout != null)
                {
                    this.flyout.Closed -= this.Closed;
                    this.flyout.Opened -= this.Opened;
                    this.flyout.Closed += this.Closed;
                    this.flyout.Opened += this.Opened;
                }
            }
        }

        //@Override
        public override void GoToState(bool value)
        {
            if (this.Flyout is null)
                base.GoToState(value, false);
            else if (this.Flyout.IsOpen)
                base.GoToState(value, true);
            else
                base.GoToState(value, false);
        }
        public override void OnClick()
        {
            if (this.Flyout is null)
            {
                ElementSoundPlayer.Play(ElementSoundKind.Invoke);
                return;
            }
            else if (this.Flyout.IsOpen)
            {
                //ElementSoundPlayer.Play(ElementSoundKind.Hide);
                this.Flyout.Hide();
            }
            else
            {
                //ElementSoundPlayer.Play(ElementSoundKind.Show);
                this.Flyout.ShowAt(this);
            }
        }

        private void Closed(object sender, object e) => base.GoToState(false, false);
        private void Opened(object sender, object e) => base.GoToState(false, true);
    }
}