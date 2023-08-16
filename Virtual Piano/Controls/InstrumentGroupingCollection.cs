using System.Collections.Generic;
using System.Linq;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Core;

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
                        Text = App.Resource.GetString($"{item2.Key}")
                    };
                }
            }
        }

        private static InstrumentItem GetItem(MidiProgram item) => new InstrumentItem
        {
            Key = item,
            Text = App.Resource.GetString($"{item}")
        };
    }
}