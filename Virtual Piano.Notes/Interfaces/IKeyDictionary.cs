using System;
using Windows.System;

namespace Virtual_Piano.Notes
{
    public interface IKeyDictionary: IDisposable
    {
        bool TryGetValue(VirtualKey key, out Note value);
    }
}