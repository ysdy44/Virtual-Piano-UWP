namespace Virtual_Piano.Elements
{
    /// <summary>
    /// ◯ ① ◯ <para/>
    /// © ◯ ② <para/>
    /// ◯ ⑥ ◯ <para/>
    /// ⑤ ◯ ③ <para/>
    /// ◯ ④ ◯ <para/>
    /// </summary>
    internal enum DigitalType
    {
        None,

        Line0 = 1,
        Line1 = 2,
        Line2 = 4,
        Line3 = 8,
        Line4 = 16,
        Line5 = 32,
        Line6 = 64,

        N1 = Line2 | Line3,
        N2 = Line1 | Line2 | Line4 | Line5 | Line6,
        N3 = Line1 | Line2 | Line3 | Line4 | Line6,
        N4 = Line0 | Line2 | Line3 | Line6,
        N5 = Line0 | Line1 | Line3 | Line4 | Line6,
        N6 = Line0 | Line1 | Line3 | Line4 | Line5 | Line6,
        N7 = Line1 | Line2 | Line3,
        N8 = Line0 | Line1 | Line2 | Line3 | Line4 | Line5 | Line6,
        N9 = Line0 | Line1 | Line2 | Line3 | Line4 | Line6,
        N0 = Line0 | Line1 | Line2 | Line3 | Line4 | Line5,
    }
}