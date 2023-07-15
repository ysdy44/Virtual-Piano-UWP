namespace Virtual_Piano.Notes.Controls
{
    public interface IInstrumentGroupPanel : IInstrumentPanel
    {
        void Add(MidiProgramGroup group);
        void Remove(MidiProgramGroup group);
    }
}