using System.Linq;
using System.Windows.Input;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Controls
{
    public sealed class ChannelPanel : StackPanel, IChannelPanel
    {
        //@Command
        public ICommand Command { get; set; }

        public ChannelPanel()
        {
            foreach (MidiChannel item in System.Enum.GetValues(typeof(MidiChannel)).Cast<MidiChannel>())
            {
                byte i = (byte)item;
                base.Children.Add(new ChannelButton(i)
                {
                    Height = TrackLayout.ItemSize,
                    Label = $"{i}",
                });
            }
        }

        public void OnClick(MidiChannelMessage message) => this.Command?.Execute(message); // Command

        public void Demute()
        {
            foreach (IChannelButton item in base.Children)
            {
                item.IsMute = false;
            }
        }

        public void Desolo()
        {
            foreach (IChannelButton item in base.Children)
            {
                item.IsSolo = false;
            }
        }

        public void Desolo(int channel)
        {
            for (int i = 0; i < base.Children.Count; i++)
            {
                if (i == channel) continue;

                IChannelButton item = (IChannelButton)base.Children[i];
                item.IsSolo = false;
            }
        }
    }
}