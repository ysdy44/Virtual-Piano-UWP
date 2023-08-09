namespace Virtual_Piano.Midi.Controls
{
    public interface IInstrumentGroupPanel
    {
        void Add(MidiProgramGroup group);
        void Remove(MidiProgramGroup group);
    }
}