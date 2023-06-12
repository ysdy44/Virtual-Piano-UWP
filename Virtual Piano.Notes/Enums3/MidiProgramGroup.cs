namespace Virtual_Piano.Notes
{
    public enum MidiProgramGroup : byte
    {
        Pianos = 0,
        ChromaticPercussion = 1,
        Organs = 2,

        Guitars = 3,
        Basses = 4,
        SoloStrings = 5,
        Ensembles = 6,

        Brass = 7,
        Reeds = 8,
        Pipes = 9,

        SynthLeads = 10,
        SynthPads = 11,
        SynthEffects = 12,

        Ethnic = 13,
        Percussive = 14,
        SoundEffects = 15,
    }
}