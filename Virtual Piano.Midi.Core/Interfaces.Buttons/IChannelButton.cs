namespace Virtual_Piano.Midi.Core
{
    public interface IChannelButton
    {
        byte Channel { get; }

        string Label { get; set; }
        string Text { get; set; }

        bool? IsMute { get; set; }
        bool? IsSolo { get; set; }
    }
}