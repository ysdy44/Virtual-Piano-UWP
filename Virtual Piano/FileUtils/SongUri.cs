using System;

namespace Virtual_Piano.FileUtils
{
    public sealed class SongUri : Uri
    {
        public int Index { get; }

        public string Text { get; }

        private SongUri(int index, string name) : base($"ms-appx:///FileUtils/{name}.mid")
        {
            this.Index = index;
            this.Text = $"{index}. {name}";
        }

        public SongUri(SongType type) : this((int)type, GetName(type))
        {
        }

        public override string ToString() => this.Text;

        //@Static
        public static Uri GetUri(SongType type) => new SongUri(type);

        public static string GetName(SongType type)
        {
            switch (type)
            {
                case SongType.CanCanJacquesOffenbach: return "Can Can-Jacques Offenbach";
                case SongType.CanoninDJohannPachelbel: return "Canon in D-Johann Pachelbel";
                case SongType.KatushaRussianFolkSong: return "Katusha-Russian Folk Song";
                case SongType.CroatianRhapsody: return "Croatian Rhapsody";
                case SongType.しんかいしょうじょ: return "しんかいしょうじょ";
                default: return default;
            }
        }
    }
}