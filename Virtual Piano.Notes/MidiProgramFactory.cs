using System.Collections.Generic;

namespace Virtual_Piano.Notes
{
    public static class MidiProgramFactory
    {

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
                    [MidiProgramGroup.Pianos] = new MidiProgram[]
                    {
                        MidiProgram.AcousticGrandPiano,
                        MidiProgram.BrightAcousticPiano,
                        MidiProgram.ElectricGrandPiano,
                        MidiProgram.HonkyTonkPiano,
                        MidiProgram.RhodesPiano,
                        MidiProgram.ChorusedPiano,
                        MidiProgram.Harpsichord,
                        MidiProgram.Clavinet,
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
                    [MidiProgramGroup.Organs] = new MidiProgram[] 
                    {
                        MidiProgram.HammondOrgan, 
                        MidiProgram.PercussiveOrgan, 
                        MidiProgram.RockOrgan, 
                        MidiProgram.ChurchOrgan, 
                        MidiProgram.ReedOrgan, 
                        MidiProgram.Accordion, 
                        MidiProgram.Harmonica, 
                        MidiProgram.TangoAccordion, 
                    },
                },



                [MidiProgramCategory.Strings] = new Dictionary<MidiProgramGroup, IEnumerable<MidiProgram>> 
                {
                    [MidiProgramGroup.Guitars] = new MidiProgram[] 
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
                    [MidiProgramGroup.Basses] = new MidiProgram[] 
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
                    [MidiProgramGroup.SoloStrings] = new MidiProgram[] 
                    {
                        MidiProgram.Violin, 
                        MidiProgram.Viola, 
                        MidiProgram.Cello, 
                        MidiProgram.Contrabass, 

                        MidiProgram.TremoloStrings, 
                        MidiProgram.PizzicatoStrings, 
                        MidiProgram.OrchestralHarp, 
                        MidiProgram.Timpani, 
                    },
                    [MidiProgramGroup.Ensembles] = new MidiProgram[] 
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
                        MidiProgram.BrassSectionDry, 
                        MidiProgram.SynthBrass1, 
                        MidiProgram.SynthBrass2, 
                    },
                    [MidiProgramGroup.Reeds] = new MidiProgram[] 
                    {
                        MidiProgram.SopranoSax, 
                        MidiProgram.AltoSax, 
                        MidiProgram.TenorSaxTenor, 
                        MidiProgram.BaritoneSax, 
                        MidiProgram.Oboe, 
                        MidiProgram.EnglishHorn, 
                        MidiProgram.Bassoon, 
                        MidiProgram.Clarinet, 
                    },
                    [MidiProgramGroup.Pipes] = new MidiProgram[] 
                    {
                        MidiProgram.Piccolo, 
                        MidiProgram.Flute, 
                        MidiProgram.Recorder, 
                        MidiProgram.PanFlute, 
                        MidiProgram.BottleBlow, 
                        MidiProgram.Shakuhachi, 
                        MidiProgram.Whistle, 
                        MidiProgram.Ocarina, 
                    },
                },



                [MidiProgramCategory.Synth] = new Dictionary<MidiProgramGroup, IEnumerable<MidiProgram>> 
                {
                    [MidiProgramGroup.SynthLeads] = new MidiProgram[] 
                    {
                        MidiProgram.Lead1Square, 
                        MidiProgram.Lead2Sawtooth, 
                        MidiProgram.Lead3Calliopelead, 
                        MidiProgram.Lead4Chifflead, 
                        MidiProgram.Lead5Charang, 
                        MidiProgram.Lead6Voice, 
                        MidiProgram.Lead7Fifths, 
                        MidiProgram.Lead8bassLead, 
                    },
                    [MidiProgramGroup.SynthPads] = new MidiProgram[] 
                    {
                        MidiProgram.Pad1Newage, 
                        MidiProgram.Pad2Warm, 
                        MidiProgram.Pad3Polysynth, 
                        MidiProgram.Pad4Choir, 
                        MidiProgram.Pad5Bowed, 
                        MidiProgram.Pad6Metallic, 
                        MidiProgram.Pad7Halo, 
                        MidiProgram.Pad8sweep, 
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
                        MidiProgram.SteelDrum, 
                        MidiProgram.Woodblock, 
                        MidiProgram.TaikoDrum, 
                        MidiProgram.MelodicTom, 
                        MidiProgram.SynthDrums, 
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
        
        public static IReadOnlyDictionary<string, string> English = new Dictionary<string, string>
        {
            [MidiProgramCategory.Keyboard.ToString()] = "Keyboard",
            [MidiProgramCategory.Strings.ToString()] = "Strings",
            [MidiProgramCategory.Wind.ToString()] = "Wind",
            [MidiProgramCategory.Synth.ToString()] = "Synth",
            [MidiProgramCategory.Special.ToString()] = "Special",



            [MidiProgramGroup.Pianos.ToString()] = "Pianos",
            [MidiProgramGroup.ChromaticPercussion.ToString()] = "Chromatic Percussion",
            [MidiProgramGroup.Organs.ToString()] = "Organs",

            [MidiProgramGroup.Guitars.ToString()] = "Guitars",
            [MidiProgramGroup.Basses.ToString()] = "Basses",
            [MidiProgramGroup.SoloStrings.ToString()] = "Solo Strings",
            [MidiProgramGroup.Ensembles.ToString()] = "Ensembles",

            [MidiProgramGroup.Brass.ToString()] = "Brass",
            [MidiProgramGroup.Reeds.ToString()] = "Reeds",
            [MidiProgramGroup.Pipes.ToString()] = "Pipes",

            [MidiProgramGroup.SynthLeads.ToString()] = "Synth Leads",
            [MidiProgramGroup.SynthPads.ToString()] = "Synth Pads",
            [MidiProgramGroup.SynthEffects.ToString()] = "Synth Effects",

            [MidiProgramGroup.Ethnic.ToString()] = "Ethnic",
            [MidiProgramGroup.Percussive.ToString()] = "Percussive",
            [MidiProgramGroup.SoundEffects.ToString()] = "Sound Effects",



            [MidiProgram.AcousticGrandPiano.ToString()] = "Acoustic Grand Piano",
            [MidiProgram.BrightAcousticPiano.ToString()] = "Bright Acoustic Piano",
            [MidiProgram.ElectricGrandPiano.ToString()] = "Electric Grand Piano",
            [MidiProgram.HonkyTonkPiano.ToString()] = "Honky Tonk Piano",
            [MidiProgram.RhodesPiano.ToString()] = "Rhodes Piano",
            [MidiProgram.ChorusedPiano.ToString()] = "Chorused Piano",
            [MidiProgram.Harpsichord.ToString()] = "Harpsichord",
            [MidiProgram.Clavinet.ToString()] = "Clavinet",

            [MidiProgram.Celesta.ToString()] = "Celesta",
            [MidiProgram.Glockenspiel.ToString()] = "Glockenspiel",
            [MidiProgram.MusicBox.ToString()] = "Music Box",
            [MidiProgram.Vibraphone.ToString()] = "Vibraphone",
            [MidiProgram.Marimba.ToString()] = "Marimba",
            [MidiProgram.Xylophone.ToString()] = "Xylophone",
            [MidiProgram.TubularBells.ToString()] = "Tubular Bells",
            [MidiProgram.Dulcimer.ToString()] = "Dulcimer",

            [MidiProgram.HammondOrgan.ToString()] = "Hammond Organ",
            [MidiProgram.PercussiveOrgan.ToString()] = "Percussive Organ",
            [MidiProgram.RockOrgan.ToString()] = "Rock Organ",
            [MidiProgram.ChurchOrgan.ToString()] = "Church Organ",
            [MidiProgram.ReedOrgan.ToString()] = "Reed Organ",
            [MidiProgram.Accordion.ToString()] = "Accordion",
            [MidiProgram.Harmonica.ToString()] = "Harmonica",
            [MidiProgram.TangoAccordion.ToString()] = "Tango Accordion",

            [MidiProgram.AcousticGuitarNylon.ToString()] = "Acoustic Guitar(nylon)",
            [MidiProgram.AcousticGuitarSteel.ToString()] = "Acoustic Guitar(steel)",
            [MidiProgram.ElectricGuitarJazz.ToString()] = "Electric Guitar(jazz)",
            [MidiProgram.ElectricGuitarClean.ToString()] = "Electric Guitar(clean)",
            [MidiProgram.ElectricGuitarMuted.ToString()] = "Electric Guitar(muted)",
            [MidiProgram.OverdrivenGuitar.ToString()] = "Overdriven Guitar",
            [MidiProgram.DistortionGuitar.ToString()] = "Distortion Guitar",
            [MidiProgram.GuitarHarmonics.ToString()] = "Guitar Harmonics",

            [MidiProgram.AcousticBass.ToString()] = "Acoustic Bass",
            [MidiProgram.ElectricBassFinger.ToString()] = "Electric Bass(finger)",
            [MidiProgram.ElectricBassPick.ToString()] = "Electric Bass(pick)",
            [MidiProgram.FretlessBass.ToString()] = "Fretless Bass",
            [MidiProgram.SlapBass1.ToString()] = "Slap Bass1",
            [MidiProgram.SlapBass2.ToString()] = "Slap Bass2",
            [MidiProgram.SynthBass1.ToString()] = "Synth Bass1",
            [MidiProgram.SynthBass2.ToString()] = "Synth Bass2",

            [MidiProgram.Violin.ToString()] = "Violin",
            [MidiProgram.Viola.ToString()] = "Viola",
            [MidiProgram.Cello.ToString()] = "Cello",
            [MidiProgram.Contrabass.ToString()] = "Contrabass",
            [MidiProgram.TremoloStrings.ToString()] = "Tremolo Strings",
            [MidiProgram.PizzicatoStrings.ToString()] = "Pizzicato Strings",
            [MidiProgram.OrchestralHarp.ToString()] = "Orchestral Harp",
            [MidiProgram.Timpani.ToString()] = "Timpani",

            [MidiProgram.StringEnsemble1.ToString()] = "String Ensemble1",
            [MidiProgram.StringEnsemble2.ToString()] = "String Ensemble2",
            [MidiProgram.SynthStrings1.ToString()] = "Synth Strings1",
            [MidiProgram.SynthStrings2.ToString()] = "Synth Strings2",
            [MidiProgram.ChoirAahs.ToString()] = "Choir Aahs",
            [MidiProgram.VoiceOohs.ToString()] = "Voice Oohs",
            [MidiProgram.SynthVoice.ToString()] = "Synth Voice",
            [MidiProgram.OrchestraHit.ToString()] = "Orchestra Hit",

            [MidiProgram.Trumpet.ToString()] = "Trumpet",
            [MidiProgram.Trombone.ToString()] = "Trombone",
            [MidiProgram.Tuba.ToString()] = "Tuba",
            [MidiProgram.MutedTrumpet.ToString()] = "Muted Trumpet",
            [MidiProgram.FrenchHorn.ToString()] = "French Horn",
            [MidiProgram.BrassSectionDry.ToString()] = "Brass Section Dry",
            [MidiProgram.SynthBrass1.ToString()] = "Synth Brass1",
            [MidiProgram.SynthBrass2.ToString()] = "Synth Brass2",

            [MidiProgram.SopranoSax.ToString()] = "Soprano Sax",
            [MidiProgram.AltoSax.ToString()] = "Alto Sax",
            [MidiProgram.TenorSaxTenor.ToString()] = "Tenor Sax(Tenor)",
            [MidiProgram.BaritoneSax.ToString()] = "Baritone Sax",
            [MidiProgram.Oboe.ToString()] = "Oboe",
            [MidiProgram.EnglishHorn.ToString()] = "English Horn",
            [MidiProgram.Bassoon.ToString()] = "Bassoon",
            [MidiProgram.Clarinet.ToString()] = "Clarinet",

            [MidiProgram.Piccolo.ToString()] = "Piccolo",
            [MidiProgram.Flute.ToString()] = "Flute",
            [MidiProgram.Recorder.ToString()] = "Recorder",
            [MidiProgram.PanFlute.ToString()] = "Pan Flute",
            [MidiProgram.BottleBlow.ToString()] = "Bottle Blow",
            [MidiProgram.Shakuhachi.ToString()] = "Shakuhachi",
            [MidiProgram.Whistle.ToString()] = "Whistle",
            [MidiProgram.Ocarina.ToString()] = "Ocarina",

            [MidiProgram.Lead1Square.ToString()] = "Lead 1 square",
            [MidiProgram.Lead2Sawtooth.ToString()] = "Lead 2 sawtooth",
            [MidiProgram.Lead3Calliopelead.ToString()] = "Lead 3 calliope lead",
            [MidiProgram.Lead4Chifflead.ToString()] = "Lead 4 chiff lead",
            [MidiProgram.Lead5Charang.ToString()] = "Lead 5 charang",
            [MidiProgram.Lead6Voice.ToString()] = "Lead 6 voice",
            [MidiProgram.Lead7Fifths.ToString()] = "Lead 7 fifths",
            [MidiProgram.Lead8bassLead.ToString()] = "Lead 8 bass + lead",

            [MidiProgram.Pad1Newage.ToString()] = "Pad 1 new age",
            [MidiProgram.Pad2Warm.ToString()] = "Pad 2 warm",
            [MidiProgram.Pad3Polysynth.ToString()] = "Pad 3 polysynth",
            [MidiProgram.Pad4Choir.ToString()] = "Pad 4 choir",
            [MidiProgram.Pad5Bowed.ToString()] = "Pad 5 bowed",
            [MidiProgram.Pad6Metallic.ToString()] = "Pad 6 metallic",
            [MidiProgram.Pad7Halo.ToString()] = "Pad 7 halo",
            [MidiProgram.Pad8sweep.ToString()] = "Pad 8 sweep",

            [MidiProgram.FX1Rain.ToString()] = "FX 1 rain",
            [MidiProgram.FX2Soundtrack.ToString()] = "FX 2 soundtrack",
            [MidiProgram.FX3Crystal.ToString()] = "FX 3 crystal",
            [MidiProgram.FX4Atmosphere.ToString()] = "FX 4 atmosphere",
            [MidiProgram.FX5Brightness.ToString()] = "FX 5 brightness",
            [MidiProgram.FX6Goblins.ToString()] = "FX 6 goblins",
            [MidiProgram.FX7Echoes.ToString()] = "FX 7 echoes",
            [MidiProgram.FX8Scifi.ToString()] = "FX 8 sci fi",

            [MidiProgram.Sitar.ToString()] = "Sitar",
            [MidiProgram.Banjo.ToString()] = "Banjo",
            [MidiProgram.Shamisen.ToString()] = "Shamisen",
            [MidiProgram.Koto.ToString()] = "Koto",
            [MidiProgram.Kalimba.ToString()] = "Kalimba",
            [MidiProgram.Bagpipe.ToString()] = "Bagpipe",
            [MidiProgram.Fiddle.ToString()] = "Fiddle",
            [MidiProgram.Shanai.ToString()] = "Shanai",

            [MidiProgram.TinkleBell.ToString()] = "Tinkle Bell",
            [MidiProgram.Agogo.ToString()] = "Agogo",
            [MidiProgram.SteelDrum.ToString()] = "Steel Drum",
            [MidiProgram.Woodblock.ToString()] = "Woodblock",
            [MidiProgram.TaikoDrum.ToString()] = "Taiko Drum",
            [MidiProgram.MelodicTom.ToString()] = "Melodic Tom",
            [MidiProgram.SynthDrums.ToString()] = "Synth Drums",
            [MidiProgram.ReverseCymbal.ToString()] = "Reverse Cymbal",

            [MidiProgram.GuitarFretNoise.ToString()] = "Guitar Fret Noise",
            [MidiProgram.BreathNoise.ToString()] = "Breath Noise",
            [MidiProgram.Seashore.ToString()] = "Seashore",
            [MidiProgram.BirdTweet.ToString()] = "Bird Tweet",
            [MidiProgram.TelephoneRing.ToString()] = "Telephone Ring",
            [MidiProgram.Helicopter.ToString()] = "Helicopter",
            [MidiProgram.Applause.ToString()] = "Applause",
            [MidiProgram.Gunshot.ToString()] = "Gunshot",
        };

        public static IReadOnlyDictionary<string, string> Chinese = new Dictionary<string, string>
        {
            [MidiProgramCategory.Keyboard.ToString()] = "键盘",
            [MidiProgramCategory.Strings.ToString()] = "弦乐",
            [MidiProgramCategory.Wind.ToString()] = "管乐",
            [MidiProgramCategory.Synth.ToString()] = "合成",
            [MidiProgramCategory.Special.ToString()] = "特殊",



            [MidiProgramGroup.Pianos.ToString()] = "钢琴",
            [MidiProgramGroup.ChromaticPercussion.ToString()] = "半音打击乐器",
            [MidiProgramGroup.Organs.ToString()] = "风琴",

            [MidiProgramGroup.Guitars.ToString()] = "吉他",
            [MidiProgramGroup.Basses.ToString()] = "贝司",
            [MidiProgramGroup.SoloStrings.ToString()] = "弦乐独奏",
            [MidiProgramGroup.Ensembles.ToString()] = "合唱合奏",

            [MidiProgramGroup.Brass.ToString()] = "铜管乐器",
            [MidiProgramGroup.Reeds.ToString()] = "哨片乐器",
            [MidiProgramGroup.Pipes.ToString()] = "吹管乐器",

            [MidiProgramGroup.SynthLeads.ToString()] = "合成主音",
            [MidiProgramGroup.SynthPads.ToString()] = "合成柔音",
            [MidiProgramGroup.SynthEffects.ToString()] = "合成特效",

            [MidiProgramGroup.Ethnic.ToString()] = "民族乐器",
            [MidiProgramGroup.Percussive.ToString()] = "打击乐",
            [MidiProgramGroup.SoundEffects.ToString()] = "声音特效",



            [MidiProgram.AcousticGrandPiano.ToString()] = "大钢琴(壮丽的)",
            [MidiProgram.BrightAcousticPiano.ToString()] = "亮音大钢琴(明亮的)",
            [MidiProgram.ElectricGrandPiano.ToString()] = "电子钢琴(壮丽的)",
            [MidiProgram.HonkyTonkPiano.ToString()] = "酒吧钢琴",
            [MidiProgram.RhodesPiano.ToString()] = "练习音钢琴",
            [MidiProgram.ChorusedPiano.ToString()] = "合唱加钢琴",
            [MidiProgram.Harpsichord.ToString()] = "拨弦古钢琴",
            [MidiProgram.Clavinet.ToString()] = "击弦古钢琴",

            [MidiProgram.Celesta.ToString()] = "钢片琴",
            [MidiProgram.Glockenspiel.ToString()] = "钟琴",
            [MidiProgram.MusicBox.ToString()] = "八音盒(音乐盒)",
            [MidiProgram.Vibraphone.ToString()] = "电颤琴",
            [MidiProgram.Marimba.ToString()] = "马林巴",
            [MidiProgram.Xylophone.ToString()] = "木琴",
            [MidiProgram.TubularBells.ToString()] = "管钟",
            [MidiProgram.Dulcimer.ToString()] = "扬琴",

            [MidiProgram.HammondOrgan.ToString()] = "击杆风琴(管风琴)",
            [MidiProgram.PercussiveOrgan.ToString()] = "打击型风琴(敲击管风琴)",
            [MidiProgram.RockOrgan.ToString()] = "摇滚风琴(摇滚管风琴)",
            [MidiProgram.ChurchOrgan.ToString()] = "教堂管风琴",
            [MidiProgram.ReedOrgan.ToString()] = "簧风琴",
            [MidiProgram.Accordion.ToString()] = "手风琴",
            [MidiProgram.Harmonica.ToString()] = "口琴",
            [MidiProgram.TangoAccordion.ToString()] = "探戈手风琴",

            [MidiProgram.AcousticGuitarNylon.ToString()] = "大厅尼龙弦吉他",
            [MidiProgram.AcousticGuitarSteel.ToString()] = "大厅钢弦吉他",
            [MidiProgram.ElectricGuitarJazz.ToString()] = "爵士乐电吉他",
            [MidiProgram.ElectricGuitarClean.ToString()] = "清音电吉他(干净)",
            [MidiProgram.ElectricGuitarMuted.ToString()] = "弱音电吉他(沙哑)",
            [MidiProgram.OverdrivenGuitar.ToString()] = "驱动音效电吉他(热烈)",
            [MidiProgram.DistortionGuitar.ToString()] = "失真音效电吉他(失真)",
            [MidiProgram.GuitarHarmonics.ToString()] = "泛音电吉他(和音)",

            [MidiProgram.AcousticBass.ToString()] = "原声贝司(大厅贝司)",
            [MidiProgram.ElectricBassFinger.ToString()] = "指拨电子贝司(手指奏)",
            [MidiProgram.ElectricBassPick.ToString()] = "拨片电子贝司(匹克奏)",
            [MidiProgram.FretlessBass.ToString()] = "无品贝司",
            [MidiProgram.SlapBass1.ToString()] = "击弦贝司1",
            [MidiProgram.SlapBass2.ToString()] = "击弦贝司2",
            [MidiProgram.SynthBass1.ToString()] = "合成贝司1",
            [MidiProgram.SynthBass2.ToString()] = "合成贝司2",

            [MidiProgram.Violin.ToString()] = "小提琴",
            [MidiProgram.Viola.ToString()] = "中提琴",
            [MidiProgram.Cello.ToString()] = "大提琴",
            [MidiProgram.Contrabass.ToString()] = "低音提琴",
            [MidiProgram.TremoloStrings.ToString()] = "弦乐震音(散弓)",
            [MidiProgram.PizzicatoStrings.ToString()] = "弦乐拨奏",
            [MidiProgram.OrchestralHarp.ToString()] = "竖琴",
            [MidiProgram.Timpani.ToString()] = "定音鼓",

            [MidiProgram.StringEnsemble1.ToString()] = "弦乐合奏1",
            [MidiProgram.StringEnsemble2.ToString()] = "弦乐合奏2",
            [MidiProgram.SynthStrings1.ToString()] = "合成弦乐1",
            [MidiProgram.SynthStrings2.ToString()] = "合成弦乐2",
            [MidiProgram.ChoirAahs.ToString()] = "合唱'啊'音",
            [MidiProgram.VoiceOohs.ToString()] = "人声'嘟'音",
            [MidiProgram.SynthVoice.ToString()] = "合成人声",
            [MidiProgram.OrchestraHit.ToString()] = "乐队打击乐(管弦合奏)",

            [MidiProgram.Trumpet.ToString()] = "小号",
            [MidiProgram.Trombone.ToString()] = "长号",
            [MidiProgram.Tuba.ToString()] = "大号(低音号)",
            [MidiProgram.MutedTrumpet.ToString()] = "弱音小号(弱音器)",
            [MidiProgram.FrenchHorn.ToString()] = "圆号(法国)",
            [MidiProgram.BrassSectionDry.ToString()] = "铜管组(铜管合奏)",
            [MidiProgram.SynthBrass1.ToString()] = "合成铜管1",
            [MidiProgram.SynthBrass2.ToString()] = "合成铜管2",

            [MidiProgram.SopranoSax.ToString()] = "高音萨克斯风",
            [MidiProgram.AltoSax.ToString()] = "中音萨克斯风",
            [MidiProgram.TenorSaxTenor.ToString()] = "次中音萨克斯风",
            [MidiProgram.BaritoneSax.ToString()] = "上低音萨克斯风",
            [MidiProgram.Oboe.ToString()] = "双簧管",
            [MidiProgram.EnglishHorn.ToString()] = "英国管",
            [MidiProgram.Bassoon.ToString()] = "巴松管(大管)",
            [MidiProgram.Clarinet.ToString()] = "单簧管",

            [MidiProgram.Piccolo.ToString()] = "短笛",
            [MidiProgram.Flute.ToString()] = "长笛",
            [MidiProgram.Recorder.ToString()] = "竖笛(合成笛(回忆的))",
            [MidiProgram.PanFlute.ToString()] = "排笛(排萧)",
            [MidiProgram.BottleBlow.ToString()] = "吹瓶口",
            [MidiProgram.Shakuhachi.ToString()] = "尺八(日本笛)",
            [MidiProgram.Whistle.ToString()] = "口哨",
            [MidiProgram.Ocarina.ToString()] = "洋埙",

            [MidiProgram.Lead1Square.ToString()] = "合成主音1(方波, 适度)",
            [MidiProgram.Lead2Sawtooth.ToString()] = "合成主音2(锯齿波)",
            [MidiProgram.Lead3Calliopelead.ToString()] = "合成主音3(汽笛风琴)",
            [MidiProgram.Lead4Chifflead.ToString()] = "合成主音4(吹管, 莺歌)",
            [MidiProgram.Lead5Charang.ToString()] = "合成主音5(吉他, 精灵)",
            [MidiProgram.Lead6Voice.ToString()] = "合成主音6(人声, 歌唱)",
            [MidiProgram.Lead7Fifths.ToString()] = "合成主音7(五度音)",
            [MidiProgram.Lead8bassLead.ToString()] = "合成主音8(低音加主音, 贝司)",

            [MidiProgram.Pad1Newage.ToString()] = "合成柔音1(新时代, 歌唱)",
            [MidiProgram.Pad2Warm.ToString()] = "合成柔音(暖音, 温情)",
            [MidiProgram.Pad3Polysynth.ToString()] = "合成柔音3(复合成, 多音)",
            [MidiProgram.Pad4Choir.ToString()] = "合成柔音4(合唱)",
            [MidiProgram.Pad5Bowed.ToString()] = "合成柔音5(弓弦, 温和)",
            [MidiProgram.Pad6Metallic.ToString()] = "合成柔音6(金属, 光泽)",
            [MidiProgram.Pad7Halo.ToString()] = "合成柔音7(光环, 多彩)",
            [MidiProgram.Pad8sweep.ToString()] = "合成柔音8(扫弦, 平静)",

            [MidiProgram.FX1Rain.ToString()] = "合成特效1(雨点)",
            [MidiProgram.FX2Soundtrack.ToString()] = "合成特效2(音轨, 多音素)",
            [MidiProgram.FX3Crystal.ToString()] = "合成特效3(水晶, 清澈)",
            [MidiProgram.FX4Atmosphere.ToString()] = "合成特效4(大气, 和谐)",
            [MidiProgram.FX5Brightness.ToString()] = "合成特效5(亮音, 明亮)",
            [MidiProgram.FX6Goblins.ToString()] = "合成特效6(小妖, 神秘)",
            [MidiProgram.FX7Echoes.ToString()] = "合成特效7(回声)",
            [MidiProgram.FX8Scifi.ToString()] = "合成特效8(科幻, 星光)",

            [MidiProgram.Sitar.ToString()] = "锡塔尔(印度锡塔琴)",
            [MidiProgram.Banjo.ToString()] = "班卓琴",
            [MidiProgram.Shamisen.ToString()] = "三味线(日本三味琴)",
            [MidiProgram.Koto.ToString()] = "筝",
            [MidiProgram.Kalimba.ToString()] = "卡林巴(非洲克林巴琴)",
            [MidiProgram.Bagpipe.ToString()] = "苏格兰风笛",
            [MidiProgram.Fiddle.ToString()] = "古提琴",
            [MidiProgram.Shanai.ToString()] = "唢呐(风笛)",

            [MidiProgram.TinkleBell.ToString()] = "铃铛(编钟)",
            [MidiProgram.Agogo.ToString()] = "拉丁打铃(铁皮角)",
            [MidiProgram.SteelDrum.ToString()] = "钢鼓",
            [MidiProgram.Woodblock.ToString()] = "木块(木鱼)",
            [MidiProgram.TaikoDrum.ToString()] = "太鼓",
            [MidiProgram.MelodicTom.ToString()] = "嗵鼓(排鼓)",
            [MidiProgram.SynthDrums.ToString()] = "合成鼓(电子鼓)",
            [MidiProgram.ReverseCymbal.ToString()] = "镲波形反转(钹(轮击))",

            [MidiProgram.GuitarFretNoise.ToString()] = "磨弦声(滑弦声)",
            [MidiProgram.BreathNoise.ToString()] = "呼吸声(吹气声)",
            [MidiProgram.Seashore.ToString()] = "海浪声(海涛声)",
            [MidiProgram.BirdTweet.ToString()] = "鸟鸣声(鸟叫声)",
            [MidiProgram.TelephoneRing.ToString()] = "电话铃声",
            [MidiProgram.Helicopter.ToString()] = "直升机声",
            [MidiProgram.Applause.ToString()] = "鼓掌声",
            [MidiProgram.Gunshot.ToString()] = "枪声(射击声)",
        };
    }
}