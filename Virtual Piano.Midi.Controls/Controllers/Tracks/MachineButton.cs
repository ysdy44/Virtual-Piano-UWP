using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Virtual_Piano.Midi.Controls
{
    public sealed class MachineButton : ToggleButton, IMachineButton
    {
        public double X { get => Canvas.GetLeft(this); set => Canvas.SetLeft(this, value); }
        public double Y { get => Canvas.GetTop(this); set => Canvas.SetTop(this, value); }

        public MachineButton()
        {
            this.Clear();
            base.Unchecked += (s, e) => this.Clear();
            base.Checked += (s, e) => this.Add();
        }

        public void Add() => base.AllowDrop = true;
        public void Clear() => base.AllowDrop = false;
    }
}