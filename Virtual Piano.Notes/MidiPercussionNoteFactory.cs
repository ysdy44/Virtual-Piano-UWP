using System.Collections.Generic;

namespace Virtual_Piano.Notes
{
    public static class MidiPercussionNoteFactory
    {

        public static IReadOnlyDictionary<MidiPercussionNoteCategory, IEnumerable<MidiPercussionNote>> Instance = new Dictionary<MidiPercussionNoteCategory, IEnumerable<MidiPercussionNote>>
        {
            [MidiPercussionNoteCategory.Drum] = new MidiPercussionNote[]
            {
                MidiPercussionNote.BassDrum1,
                MidiPercussionNote.AcousticBassDrum,
                MidiPercussionNote.AcousticSnare,
                MidiPercussionNote.ElectricSnare,
                MidiPercussionNote.SideStick,
            },
            [MidiPercussionNoteCategory.Hat] = new MidiPercussionNote[]
            {
                 MidiPercussionNote.ClosedHiHat,
                 MidiPercussionNote.OpenHiHat,
                 MidiPercussionNote.PedalHiHat,
            },
            [MidiPercussionNoteCategory.Cymbal] = new MidiPercussionNote[]
            {
                 MidiPercussionNote.CrashCymbal1,
                 MidiPercussionNote.CrashCymbal2,
                 MidiPercussionNote.SplashCymbal,
                 MidiPercussionNote.ChineseCymbal,
                 MidiPercussionNote.RideCymbal1,
                 MidiPercussionNote.RideCymbal2,
                 MidiPercussionNote.RideBell,
            },
            [MidiPercussionNoteCategory.Tom] = new MidiPercussionNote[]
            {
                 MidiPercussionNote.LowFloorTom,
                 MidiPercussionNote.HighFloorTom,
                 MidiPercussionNote.LowTom,
                 MidiPercussionNote.LowMidTom,
                 MidiPercussionNote.HiMidTom,
                 MidiPercussionNote.HighTom,
            },
            [MidiPercussionNoteCategory.African] = new MidiPercussionNote[]
            {
                 MidiPercussionNote.HiWoodBlock,
                 MidiPercussionNote.LowWoodBlock,
                 MidiPercussionNote.Cabasa,
                 MidiPercussionNote.HighAgogo,
                 MidiPercussionNote.LowAgogo,
                 MidiPercussionNote.Vibraslap,
                 MidiPercussionNote.MuteHiConga,
                 MidiPercussionNote.OpenHiConga,
                 MidiPercussionNote.LowConga,
            },
            [MidiPercussionNoteCategory.Latin] = new MidiPercussionNote[]
            {
                 MidiPercussionNote.ShortGuiro,
                 MidiPercussionNote.LongGuiro,
                 MidiPercussionNote.Claves,
                 MidiPercussionNote.MuteCuica,
                 MidiPercussionNote.OpenCuica,
                 MidiPercussionNote.Maracas,
                 MidiPercussionNote.Cowbell,
                 MidiPercussionNote.HiBongo,
                 MidiPercussionNote.LowBongo,
                 MidiPercussionNote.Castanets,
                 MidiPercussionNote.MuteSurdo,
                 MidiPercussionNote.OpenSurdo,
            },
            [MidiPercussionNoteCategory.Special] = new MidiPercussionNote[]
            {
                 MidiPercussionNote.Tambourine,
                 MidiPercussionNote.HighTimbale,
                 MidiPercussionNote.LowTimbale,
                 MidiPercussionNote.ShortWhistle,
                 MidiPercussionNote.LongWhistle,
                 MidiPercussionNote.MuteTriangle,
                 MidiPercussionNote.OpenTriangle,
                 MidiPercussionNote.Shaker,
                 MidiPercussionNote.SleighBells,
                 MidiPercussionNote.BellTree,
                 MidiPercussionNote.Sticks,
            },
            [MidiPercussionNoteCategory.Synth] = new MidiPercussionNote[]
            {
                 MidiPercussionNote.MetronomeBell,
                 MidiPercussionNote.MetronomeClick,
                 MidiPercussionNote.SquareClick,
                 MidiPercussionNote.ScratchPull,
                 MidiPercussionNote.ScratchPush,
                 MidiPercussionNote.Slap,
                 MidiPercussionNote.HighQ,
                 MidiPercussionNote.HandClap,
            },
        };

    }
}