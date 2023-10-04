using System.Collections.Generic;
using System.Linq;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Core;
using Virtual_Piano.Strings;

namespace Virtual_Piano.Controls
{
    public sealed class InstrumentGroupingCollection : List<InstrumentItemGrouping>
    {
        public InstrumentGroupingCollection() : base(GetItemsSource()) { }

        private static IEnumerable<InstrumentItemGrouping> GetItemsSource()
        {
            foreach (var item in MidiProgramFactory.Instance)
            {
                foreach (var item2 in item.Value)
                {
                    yield return new InstrumentItemGrouping(item2.Value.Select(GetItem))
                    {
                        Key = item2.Key,
                        Text = item2.Key.GetString()
                    };
                }
            }
        }

        private static InstrumentItem GetItem(MidiProgram item) => new InstrumentItem
        {
            Key = item,
            Text = item.GetString()
        };
    }
}