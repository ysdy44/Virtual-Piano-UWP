namespace Virtual_Piano.Midi.Core
{
    public interface IInstrumentGroupPanel : IInstrumentPanel
    {
        void Add(MidiProgramGroup group);
        void Remove(MidiProgramGroup group);
    }
}