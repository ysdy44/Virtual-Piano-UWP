namespace Virtual_Piano.Midi
{
    public enum MidiPercussionProgram
    {
        Standard = 0, // 0 - Drums (Standard Kit)
        RoomKit = 8, // 8 - Drums (Room Kit)
        PowerKit = 16, // 16 - Drums (Power Kit)
        Electronic = 24, // 24 - Drums (Electronic Kit)
        Analog = 25, // 25 - Drums (TR-808 Kit)
        Jazz = 32, // 32 - Drums (Jazz Kit)
        Brush = 40, // 40 - Drums (Brush Kit)
        Orchestral = 48, // 48 - Drums (Orchestra Kit)
        SpecialEffects = 56, // 56 - Drums (Sound Effect Kit)
        CM32L = 127, // 127 - Drums (CM-64/CM-32L Kit)
    }
}