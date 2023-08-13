using System.Collections.Generic;
using Windows.System;

namespace Virtual_Piano.Midi.Core
{
    public abstract class KeyDictionary : Dictionary<VirtualKey, MidiNote>
    {
        //@Abstract
        public abstract VirtualKey CreateKey(int index);

        //@Construct
        public KeyDictionary(ToneType type)
        {
            int index = 0;

            int min = (int)MidiNote.C3;
            int max = (int)MidiNote.C8;

            for (int i = min; i <= max; i++)
            {
                MidiNote note = (MidiNote)i;
                Tone tone = note.ToTone();

                switch (tone.ToType())
                {
                    case ToneType.White:
                        switch (type)
                        {
                            case ToneType.White:
                                base[this.CreateKey(index)] = note;
                                break;
                            default:
                                break;
                        }
                        index++;
                        break;
                    case ToneType.Black:
                        switch (type)
                        {
                            case ToneType.Black:
                                base[this.CreateKey(index - 1)] = note;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void Dispose()
        {
            base.Clear();
        }
    }
}