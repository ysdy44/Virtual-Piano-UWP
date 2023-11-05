using System.Collections.Generic;
using System.Linq;

namespace Virtual_Piano.Midi.Core
{
    public sealed class InstrumentItemGrouping : List<MidiProgram>, IList<MidiProgram>, IGrouping<MidiProgramGroup, MidiProgram>
    {
        public MidiProgramGroup Key { get; set; }
        public string Text { get; set; }

        public InstrumentItemGrouping(IEnumerable<MidiProgram> collection) : base(collection) { }

        public override string ToString() => this.Text;
    }
}