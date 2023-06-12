using System.Collections.Generic;
using System.Linq;
using Windows.System;

namespace Virtual_Piano.Notes
{
    public abstract class KeyDictionary : Dictionary<VirtualKey, Note>
    {
        //@Abstract
        public abstract VirtualKey CreateKey(int index);

        //@Construct
        public KeyDictionary(ToneType type)
        {
            int index = 0;
            foreach (Octave octave in System.Enum.GetValues(typeof(Octave)).Cast<Octave>())
            {
                foreach (Tone tone in System.Enum.GetValues(typeof(Tone)).Cast<Tone>())
                {
                    Note note = octave.ToNote(tone);
                    switch (tone.ToType())
                    {
                        case ToneType.White:
                            switch (type)
                            {
                                case ToneType.White:
                                    base[CreateKey(index)] = note;
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
                                    base[CreateKey(index - 1)] = note;
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
        }

        public void Dispose()
        {
            base.Clear();
        }
    }
}