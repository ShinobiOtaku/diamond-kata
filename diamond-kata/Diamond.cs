using System.Collections.Generic;
using System.Linq;

namespace diamond_kata
{
    public static class Diamond
    {
        private static char[] GetLetters(char seed)
        {
            var acc = new List<char>();
            for (var i = 'A'; i < seed; i++) acc.Add(i);
            for (var i = seed; i >= 'A'; i--) acc.Add(i);

            return acc.ToArray();
        }

        private static string GenerateTopOrBottomRow(char letter, int width)
        {
            var outerSpaces = new string(' ', (width - 1) / 2);
            return outerSpaces  + letter + outerSpaces;
        }

        private static string GenerateMiddleRow(char letter, int width)
        {
            var innerSpaces = (letter - 'A') * 2 - 1;
            var outerSpace = new string(' ', (width - 2 - innerSpaces) / 2);
            var innerSpace = new string(' ', innerSpaces);
            return outerSpace + letter + innerSpace + letter + outerSpace;
        }

        private static string GenerateRow(char letter, int width)
        {
            return letter == 'A' 
                ? GenerateTopOrBottomRow(letter, width) 
                : GenerateMiddleRow(letter, width);
        }

        public static string[] Generate(char seed)
        {
            var letters = GetLetters(seed);
            var rows = letters.Select(l => GenerateRow(l, letters.Length));

            return rows.ToArray();
        }
    }
}