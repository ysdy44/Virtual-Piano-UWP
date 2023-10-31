namespace Virtual_Piano.Midi
{
    public enum MidiChannelMessageType : byte
    {
        ProgramChange,
        Edit,

        UnMute,
        Mute,

        UnSolo,
        Solo,

        UnRecord,
        Record,
    }
}