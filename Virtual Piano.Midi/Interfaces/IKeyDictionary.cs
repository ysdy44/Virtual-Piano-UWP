using System;
using Windows.System;

namespace Virtual_Piano.Midi
{
    public interface IKeyDictionary: IDisposable
    {
        bool TryGetValue(VirtualKey key, out MidiNote value);
    }
}