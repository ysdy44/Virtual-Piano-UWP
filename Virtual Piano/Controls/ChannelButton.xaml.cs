using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Virtual_Piano.Controls
{
    [ContentProperty(Name = nameof(Text))]
    public sealed partial class ChannelButton : UserControl, IChannelButton
    {
        //@Converter
        private double OpacityConverter(bool? value) => value is true ? 0.2 : 0.0;

        public byte Channel { get; }

        public string Label { get => this.LabelTextBlock.Text; set => this.LabelTextBlock.Text = value; }
        public string Text { get => this.TextBlock.Text; set => this.TextBlock.Text = value; }

        public bool? IsMute { get => this.MuteButton.IsChecked; set => this.MuteButton.IsChecked = value; }
        public bool? IsSolo { get => this.SoloButton.IsChecked; set => this.SoloButton.IsChecked = value; }

        readonly MidiChannelMessage ProgramChange;
        readonly MidiChannelMessage Edit;

        readonly MidiChannelMessage UnMute;
        readonly MidiChannelMessage Mute;

        readonly MidiChannelMessage UnSolo;
        readonly MidiChannelMessage Solo;

        readonly MidiChannelMessage UnRecord;
        readonly MidiChannelMessage Record;

        public ChannelButton(byte channel)
        {
            this.InitializeComponent();
            this.Channel = channel;

            this.ProgramChange = new MidiChannelMessage
            {
                Type = MidiChannelMessageType.ProgramChange,
                Channel = channel
            };
            this.Edit = new MidiChannelMessage
            {
                Type = MidiChannelMessageType.Edit,
                Channel = channel
            };

            this.UnMute = new MidiChannelMessage
            {
                Type = MidiChannelMessageType.UnMute,
                Channel = channel
            };
            this.Mute = new MidiChannelMessage
            {
                Type = MidiChannelMessageType.Mute,
                Channel = channel
            };

            this.UnSolo = new MidiChannelMessage
            {
                Type = MidiChannelMessageType.UnSolo,
                Channel = channel
            };
            this.Solo = new MidiChannelMessage
            {
                Type = MidiChannelMessageType.Solo,
                Channel = channel
            };

            this.UnRecord = new MidiChannelMessage
            {
                Type = MidiChannelMessageType.UnRecord,
                Channel = channel
            };
            this.Record = new MidiChannelMessage
            {
                Type = MidiChannelMessageType.Record,
                Channel = channel
            };

            this.Button.Click += (s, e) =>
            {
                if (base.Parent is IChannelPanel item)
                {
                    item.OnClick(this.ProgramChange);
                }
            };
            this.EditButton.Click += (s, e) =>
            {
                if (base.Parent is IChannelPanel item)
                {
                    item.OnClick(this.Edit);
                }
            };

            this.MuteButton.Unchecked += (s, e) =>
            {
                if (base.Parent is IChannelPanel item)
                {
                    item.OnClick(this.UnMute);
                }
            };
            this.MuteButton.Checked += (s, e) =>
            {
                if (base.Parent is IChannelPanel item)
                {
                    item.OnClick(this.Mute);
                }
            };

            this.SoloButton.Unchecked += (s, e) =>
            {
                if (base.Parent is IChannelPanel item)
                {
                    item.OnClick(this.UnSolo);
                }
            };
            this.SoloButton.Checked += (s, e) =>
            {
                if (base.Parent is IChannelPanel item)
                {
                    item.OnClick(this.Solo);
                }
            };
        }
    }
}