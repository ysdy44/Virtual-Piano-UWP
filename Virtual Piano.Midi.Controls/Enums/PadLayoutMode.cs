namespace Virtual_Piano.Midi.Core
{
    public enum PadLayoutMode // n =12 * width / height
    {
        L1x12, // n = 12 * 1 / 12 = 1 (n<2)
        L2x6, // n = 12 * 2 / 6 = 4 (n<7)
        L3x4, // n = 12 * 3 / 4 = 9 (n<12)
        L4x3, // n = 12 * 4 / 3 = 16 (n<26)
        L6x2, // n = 12 * 6 / 2 = 36 (n<54)
        L12x1, // n = 12 * 12 / 1 = 144
    }
}