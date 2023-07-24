namespace Virtual_Piano.Midi.Controls
{
    public interface IInstrumentGroupPanel : IInstrumentPanel
    {
        void Add(MidiProgramGroup group);
        void Remove(MidiProgramGroup group);
    }
}