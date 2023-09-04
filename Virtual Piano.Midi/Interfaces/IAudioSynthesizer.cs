using System;

namespace Virtual_Piano.Midi
{
    public interface IAudioSynthesizer : IDisposable
    {
        void NoteOff(MidiNote note);
        void NoteOn(MidiNote note);
        void Reset();
    }
}