﻿namespace Virtual_Piano.Midi
{
    public enum KitSet : byte
    {
        Crash = MidiPercussionNote.CrashCymbal1,
        Ride = MidiPercussionNote.RideCymbal1,
        Open = MidiPercussionNote.OpenHiHat,
        Close = MidiPercussionNote.ClosedHiHat,
        Pedal = MidiPercussionNote.PedalHiHat,

        HiTom = MidiPercussionNote.HighTom,
        LowTom = MidiPercussionNote.LowTom,
        FloorTom = MidiPercussionNote.LowFloorTom,

        Snare = MidiPercussionNote.AcousticSnare,
        Stick = MidiPercussionNote.SideStick,
        Kick = MidiPercussionNote.BassDrum1,

        Clap = MidiPercussionNote.HandClap,
        Ring = MidiPercussionNote.Cowbell,
        Sandham = MidiPercussionNote.Shaker,
        Tambourine = MidiPercussionNote.Tambourine,
    }
}