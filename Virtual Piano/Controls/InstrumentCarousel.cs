using Virtual_Piano.Midi;

namespace Virtual_Piano.Controls
{
    public class InstrumentCarousel : Midi.Controls.InstrumentCarousel
    {
        public override string GetString(MidiProgram note)
        {
            return App.Resource.GetString($"{note}");
        }
        public override string GetString(MidiProgramGroup group)
        {
            return App.Resource.GetString($"{group}");
        }
    }
}