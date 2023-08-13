using System.Collections.Generic;
using Windows.System;

namespace Virtual_Piano.Midi.Core
{
    public class KeyDrumDictionary : Dictionary<VirtualKey, MidiPercussionNote>
    {
        public KeyDrumDictionary()
        {
            this[VirtualKey.F1] = (MidiPercussionNote)KitSet.Kick;
            this[VirtualKey.F2] = (MidiPercussionNote)KitSet.Snare;
            this[VirtualKey.F3] = (MidiPercussionNote)KitSet.Clap;
            this[VirtualKey.F4] = (MidiPercussionNote)KitSet.Close;
            this[VirtualKey.F5] = (MidiPercussionNote)KitSet.LowTom;
            this[VirtualKey.F6] = (MidiPercussionNote)KitSet.Open;
            this[VirtualKey.F7] = (MidiPercussionNote)KitSet.MidTom;
            this[VirtualKey.F8] = (MidiPercussionNote)KitSet.Crash;
            this[VirtualKey.F9] = (MidiPercussionNote)KitSet.FloorTom;
            this[VirtualKey.F10] = (MidiPercussionNote)KitSet.Ride;
            this[VirtualKey.F11] = (MidiPercussionNote)KitSet.Ring;
            this[VirtualKey.F12] = (MidiPercussionNote)KitSet.Sandham;
        }
    }
}