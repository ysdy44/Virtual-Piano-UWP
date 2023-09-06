using System.Collections.Generic;
using System.Linq;
using Windows.Storage;

namespace Virtual_Piano.Midi
{
    public static class OrderExtenisons
    {
        public static IEnumerable<StorageFile> Order(this IEnumerable<StorageFile> collection)
        {
            return collection.OrderByDescending(OrderByName).Reverse();
        }
        public static IEnumerable<IStorageItem> Order(this IEnumerable<IStorageItem> collection)
        {
            return collection.OrderByDescending(OrderByName).Reverse();
        }

        public static int OrderByName(this StorageFile contentControl)
        {
            if (contentControl is null) return 0;

            if (string.IsNullOrEmpty(contentControl.DisplayName)) return 0;
            return OrderExtenisons.OrderByNote(contentControl.DisplayName);
        }
        public static int OrderByName(this IStorageItem contentControl)
        {
            if (contentControl is null) return 0;

            if (string.IsNullOrEmpty(contentControl.Name)) return 0;
            return OrderExtenisons.OrderByNote(contentControl.Name);
        }

        private static int OrderByNote(this string name)
        {
            string low = name.ToLower();

            if (low.Contains("do")) return name.Length + name.OrderByNumber() * 100;
            else if (low.Contains("re")) return name.Length + name.OrderByNumber() * 100 + 1 * 10;
            else if (low.Contains("mi")) return name.Length + name.OrderByNumber() * 100 + 2 * 10;
            else if (low.Contains("fa")) return name.Length + name.OrderByNumber() * 100 + 3 * 10;
            else if (low.Contains("so")) return name.Length + name.OrderByNumber() * 100 + 4 * 10;
            else if (low.Contains("la")) return name.Length + name.OrderByNumber() * 100 + 5 * 10;
            else if (low.Contains("si")) return name.Length + name.OrderByNumber() * 100 + 6 * 10;

            else if (name.Contains('C')) return name.Length + name.OrderByNumber() * 100;
            else if (name.Contains('D')) return name.Length + name.OrderByNumber() * 100 + 1 * 10;
            else if (name.Contains('E')) return name.Length + name.OrderByNumber() * 100 + 2 * 10;
            else if (name.Contains('F')) return name.Length + name.OrderByNumber() * 100 + 3 * 10;
            else if (name.Contains('G')) return name.Length + name.OrderByNumber() * 100 + 4 * 10;
            else if (name.Contains('A')) return name.Length + name.OrderByNumber() * 100 + 5 * 10;
            else if (name.Contains('B')) return name.Length + name.OrderByNumber() * 100 + 6 * 10;

            else return name.OrderByChar();
        }


        private static int OrderByNumber(this string s)
        {
            int num = 0;
            if (s.Contains('1')) num += 1;
            if (s.Contains('2')) num += 2;
            if (s.Contains('3')) num += 3;
            if (s.Contains('4')) num += 4;
            if (s.Contains('5')) num += 5;
            if (s.Contains('6')) num += 6;
            if (s.Contains('7')) num += 7;
            if (s.Contains('8')) num += 9;
            if (s.Contains('9')) num += 9;
            return num;
        }

        private static int OrderByChar(this string s)
        {
            int num = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int chars = s[i];
                int pow = s.Length - i;

                for (int j = 0; j < pow; j++)
                {
                    chars *= 10;
                }

                num += chars;
            }
            return num;
        }
    }
}