using System.Collections.Generic;

namespace Virtual_Piano.Notes
{
    public static class MidiProgramFactory
    {

        public static IDictionary<MidiProgramGroup, string> Emoji = new Dictionary<MidiProgramGroup, string>
        {
            [MidiProgramGroup.Piano] = "🎹",
            [MidiProgramGroup.ChromaticPercussion] = "🎼",
            [MidiProgramGroup.Organ] = "🪗",

            [MidiProgramGroup.Guitar] = "🎸",
            [MidiProgramGroup.Bass] = "🎸",
            [MidiProgramGroup.Strings] = "🎻",
            [MidiProgramGroup.Ensemble] = "👄",

            [MidiProgramGroup.Brass] = "🎺",
            [MidiProgramGroup.Reed] = "🎷",
            [MidiProgramGroup.Pipe] = "🍾",

            [MidiProgramGroup.SynthLead] = "📯",
            [MidiProgramGroup.SynthPad] = "👾",
            [MidiProgramGroup.SynthEffects] = "💦",

            [MidiProgramGroup.Ethnic] = "🪕",
            [MidiProgramGroup.Percussive] = "🥁",
            [MidiProgramGroup.SoundEffects] = "🔫",
        };
        
#pragma warning disable format

        public static IReadOnlyDictionary<MidiProgramCategory,
            IReadOnlyDictionary<MidiProgramGroup,
                IEnumerable<MidiProgram>>> Instance

            = new Dictionary<MidiProgramCategory,
                IReadOnlyDictionary<MidiProgramGroup,
                    IEnumerable<MidiProgram>>>
            {
                [MidiProgramCategory.Keyboard] = new Dictionary<MidiProgramGroup, IEnumerable<MidiProgram>> 
                {
                    [MidiProgramGroup.Piano] = new MidiProgram[]
                    {
                        MidiProgram.AcousticGrand,
                        MidiProgram.BrightAcoustic,
                        MidiProgram.ElectricGrand,
                        MidiProgram.HonkyTonk,
                        MidiProgram.ElectricPiano1,
                        MidiProgram.ElectricPiano2,
                        MidiProgram.Harpsichord,
                        MidiProgram.Clav,
                    },
                    [MidiProgramGroup.ChromaticPercussion] = new MidiProgram[]
                    {
                        MidiProgram.Celesta,
                        MidiProgram.Glockenspiel, 
                        MidiProgram.MusicBox, 
                        MidiProgram.Vibraphone, 
                        MidiProgram.Marimba, 
                        MidiProgram.Xylophone, 
                        MidiProgram.TubularBells, 
                        MidiProgram.Dulcimer, 
                    },
                    [MidiProgramGroup.Organ] = new MidiProgram[] 
                    {
                        MidiProgram.DrawbarOrgan, 
                        MidiProgram.PercussiveOrgan, 
                        MidiProgram.RockOrgan, 
                        MidiProgram.ChurchOrgan, 
                        MidiProgram.ReedOrgan, 
                        MidiProgram.Accoridan, 
                        MidiProgram.Harmonica, 
                        MidiProgram.TangoAccordian, 
                    },
                },



                [MidiProgramCategory.SoloStrings] = new Dictionary<MidiProgramGroup, IEnumerable<MidiProgram>> 
                {
                    [MidiProgramGroup.Guitar] = new MidiProgram[] 
                    {
                        MidiProgram.AcousticGuitarNylon, 
                        MidiProgram.AcousticGuitarSteel, 
                        MidiProgram.ElectricGuitarJazz, 
                        MidiProgram.ElectricGuitarClean, 
                        MidiProgram.ElectricGuitarMuted, 
                        MidiProgram.OverdrivenGuitar, 
                        MidiProgram.DistortionGuitar, 
                        MidiProgram.GuitarHarmonics, 
                    },
                    [MidiProgramGroup.Bass] = new MidiProgram[] 
                    {
                        MidiProgram.AcousticBass, 
                        MidiProgram.ElectricBassFinger, 
                        MidiProgram.ElectricBassPick, 
                        MidiProgram.FretlessBass, 
                        MidiProgram.SlapBass1, 
                        MidiProgram.SlapBass2, 
                        MidiProgram.SynthBass1, 
                        MidiProgram.SynthBass2, 
                    },
                    [MidiProgramGroup.Strings] = new MidiProgram[] 
                    {
                        MidiProgram.Violin, 
                        MidiProgram.Viola, 
                        MidiProgram.Cello, 
                        MidiProgram.Contrabass, 

                        MidiProgram.TremoloStrings, 
                        MidiProgram.PizzicatoStrings, 
                        MidiProgram.OrchestralStrings, 
                        MidiProgram.Timpani, 
                    },
                    [MidiProgramGroup.Ensemble] = new MidiProgram[] 
                    {
                        MidiProgram.StringEnsemble1, 
                        MidiProgram.StringEnsemble2, 
                        MidiProgram.SynthStrings1, 
                        MidiProgram.SynthStrings2, 
                        MidiProgram.ChoirAahs, 
                        MidiProgram.VoiceOohs, 
                        MidiProgram.SynthVoice, 
                        MidiProgram.OrchestraHit, 
                    },
                },



                [MidiProgramCategory.Wind] = new Dictionary<MidiProgramGroup, IEnumerable<MidiProgram>> 
                {
                    [MidiProgramGroup.Brass] = new MidiProgram[] 
                    {
                        MidiProgram.Trumpet, 
                        MidiProgram.Trombone, 
                        MidiProgram.Tuba, 
                        MidiProgram.MutedTrumpet, 
                        MidiProgram.FrenchHorn, 
                        MidiProgram.BrassSection, 
                        MidiProgram.SynthBrass1, 
                        MidiProgram.SynthBrass2, 
                    },
                    [MidiProgramGroup.Reed] = new MidiProgram[] 
                    {
                        MidiProgram.SopranoSax, 
                        MidiProgram.AltoSax, 
                        MidiProgram.TenorSax, 
                        MidiProgram.BaritoneSax, 
                        MidiProgram.Oboe, 
                        MidiProgram.EnglishHorn, 
                        MidiProgram.Bassoon, 
                        MidiProgram.Clarinet, 
                    },
                    [MidiProgramGroup.Pipe] = new MidiProgram[] 
                    {
                        MidiProgram.Piccolo, 
                        MidiProgram.Flute, 
                        MidiProgram.Recorder, 
                        MidiProgram.PanFlute, 
                        MidiProgram.BlownBottle, 
                        MidiProgram.Skakuhachi, 
                        MidiProgram.Whistle, 
                        MidiProgram.Ocarina, 
                    },
                },



                [MidiProgramCategory.Synth] = new Dictionary<MidiProgramGroup, IEnumerable<MidiProgram>> 
                {
                    [MidiProgramGroup.SynthLead] = new MidiProgram[] 
                    {
                        MidiProgram.Lead1Square, 
                        MidiProgram.Lead2Sawtooth, 
                        MidiProgram.Lead3Calliope, 
                        MidiProgram.Lead4Chiff, 
                        MidiProgram.Lead5Charang, 
                        MidiProgram.Lead6Voice, 
                        MidiProgram.Lead7Fifths, 
                        MidiProgram.Lead8BassLead, 
                    },
                    [MidiProgramGroup.SynthPad] = new MidiProgram[] 
                    {
                        MidiProgram.Pad1Newage, 
                        MidiProgram.Pad2Warm, 
                        MidiProgram.Pad3Polysynth, 
                        MidiProgram.Pad4Choir, 
                        MidiProgram.Pad5Bowed, 
                        MidiProgram.Pad6Metallic, 
                        MidiProgram.Pad7Halo, 
                        MidiProgram.Pad8Sweep, 
                    },
                    [MidiProgramGroup.SynthEffects] = new MidiProgram[] 
                    {
                        MidiProgram.FX1Rain, 
                        MidiProgram.FX2Soundtrack, 
                        MidiProgram.FX3Crystal, 
                        MidiProgram.FX4Atmosphere, 
                        MidiProgram.FX5Brightness, 
                        MidiProgram.FX6Goblins, 
                        MidiProgram.FX7Echoes, 
                        MidiProgram.FX8Scifi, 
                    },
                },



                [MidiProgramCategory.Special] = new Dictionary<MidiProgramGroup, IEnumerable<MidiProgram>> 
                {
                    [MidiProgramGroup.Ethnic] = new MidiProgram[] 
                    {
                        MidiProgram.Sitar, 
                        MidiProgram.Banjo, 
                        MidiProgram.Shamisen, 
                        MidiProgram.Koto, 
                        MidiProgram.Kalimba, 
                        MidiProgram.Bagpipe, 
                        MidiProgram.Fiddle, 
                        MidiProgram.Shanai, 
                    },
                    [MidiProgramGroup.Percussive] = new MidiProgram[] 
                    {
                        MidiProgram.TinkleBell, 
                        MidiProgram.Agogo, 
                        MidiProgram.SteelDrums, 
                        MidiProgram.Woodblock, 
                        MidiProgram.TaikoDrum, 
                        MidiProgram.MelodicTom, 
                        MidiProgram.SynthDrum, 
                        MidiProgram.ReverseCymbal, 
                    },
                    [MidiProgramGroup.SoundEffects] = new MidiProgram[]
                    {
                        MidiProgram.GuitarFretNoise,
                        MidiProgram.BreathNoise,
                        MidiProgram.Seashore,
                        MidiProgram.BirdTweet,
                        MidiProgram.TelephoneRing,
                        MidiProgram.Helicopter,
                        MidiProgram.Applause,
                        MidiProgram.Gunshot,
                    },
                },
            };
      
        #pragma warning restore format

    }
}