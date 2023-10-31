namespace Virtual_Piano.Midi.Core
{
    public interface IChannelPanel
    {
        void OnClick(MidiChannelMessage message);

        void Demute();
        void Desolo();
        void Desolo(int channel);
    }
}