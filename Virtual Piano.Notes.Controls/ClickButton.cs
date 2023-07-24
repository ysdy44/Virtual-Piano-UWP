using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Controls
{
    public abstract class ClickButton : ContactButton
    {
        //@Command
        public ClickMode ClickMode { get; set; } = ClickMode.Press;
        
        public double X { get => Canvas.GetLeft(this); set => Canvas.SetLeft(this, value); }
        public double Y { get => Canvas.GetTop(this); set => Canvas.SetTop(this, value); }

        protected abstract void OnClick();

        protected override void OnContactOff()
        {
            switch (this.ClickMode)
            {
                case ClickMode.Release:
                    this.OnClick();
                    break;
                default:
                    break;
            }
        }

        protected override void OnContactOn()
        {
            switch (this.ClickMode)
            {
                case ClickMode.Press:
                    this.OnClick();
                    break;
                default:
                    break;
            }
        }
    }
}