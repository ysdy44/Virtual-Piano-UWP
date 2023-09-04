using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.Storage;

namespace Virtual_Piano.Midi
{
    public sealed class AudioSynthesizer : Dictionary<MidiNote, AudioFileInputNode>, IAudioOutPort, IAudioSynthesizer
    {
        IAudioOutPort Source;

        private AudioSynthesizer()
        {
        }

        public async static Task<AudioSynthesizer> CreateAsync() => await AudioOutPort.CreateAsync() is IAudioOutPort outPort ? new AudioSynthesizer
        {
            Source = outPort
        } : null;

        public bool Remove(AudioFileInputNode node) => this.Source is null ? false : this.Source.Remove(node);
        public async Task<AudioFileInputNode> AddAsync(IStorageFile file) => this.Source is null ? null : await this.Source.AddAsync(file);

        public void Reset()
        {
            foreach (var item in this)
            {
                this.Source?.Remove(item.Value);
            }
            base.Clear();
        }

        public void Dispose()
        {
            this.Reset();
            this.Source?.Dispose();
        }

        public void NoteOn(MidiNote note)
        {
            if (base.ContainsKey(note))
            {
                base[note].Seek(TimeSpan.Zero);
                base[note].Start();
            }
        }

        public void NoteOff(MidiNote note)
        {
            if (base.ContainsKey(note))
            {
                base[note].Stop();
            }
        }
    }
}