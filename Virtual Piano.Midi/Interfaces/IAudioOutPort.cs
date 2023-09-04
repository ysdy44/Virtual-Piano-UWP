using System;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.Storage;

namespace Virtual_Piano.Midi
{
    public interface IAudioOutPort : IDisposable
    {
        bool Remove(AudioFileInputNode node);
        Task<AudioFileInputNode> AddAsync(IStorageFile file);
    }
}