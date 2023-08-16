using System.Collections.Generic;
using System.Linq;

namespace Virtual_Piano.Midi.Core
{
    public sealed class InstrumentItemGrouping : List<InstrumentItem>, IList<InstrumentItem>, IGrouping<MidiProgramGroup, InstrumentItem>
    {
        public MidiProgramGroup Key { get; set; }
        public string Text { get; set; }

        public InstrumentItemGrouping(IEnumerable<InstrumentItem> collection) : base(collection) { }

        public override string ToString() => this.Text;
    }
}