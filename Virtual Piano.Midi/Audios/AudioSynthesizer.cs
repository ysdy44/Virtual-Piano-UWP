using System;
using System.Collections.Generic;
using Windows.Media.Audio;

namespace Virtual_Piano.Midi
{
    public sealed class AudioSynthesizer : Dictionary<MidiNote, AudioFileInputNode>
    {
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