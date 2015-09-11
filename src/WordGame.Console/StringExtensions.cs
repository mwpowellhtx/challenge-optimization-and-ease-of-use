using System.Collections.Generic;
using System.Linq;

namespace WordGame
{
    public static class StringExtensions
    {
        public static string ReverseText(this string text)
        {
            var reversed = text.Aggregate(string.Empty, (g, c) => c + g);
            return reversed;
        }

        public static string MaskText(this string text, int position, int count)
        {
            const char space = ' ';

            var min = position;
            var max = position + count - 1;

            return text.Aggregate(string.Empty,
                (g, c) => g + (g.Length >= min && g.Length <= max ? space : c));
        }

        public static string ReduceMask(this string text)
        {
            return text.Aggregate(string.Empty,
                (g, c) => char.IsWhiteSpace(c) ? g : g + c);
        }

        private static char VerifySwapIndex(this string text, int i)
        {
            return text[i];
        }

        public static string SwapChars(this string text, int i, int j)
        {
            text.VerifySwapIndex(i);
            text.VerifySwapIndex(j);

            var chars = text.ToCharArray();

            {
                var x = chars[i];
                chars[i] = chars[j];
                chars[j] = x;
            }

            return chars.Aggregate(string.Empty, (g, x) => g + x);
        }

        private static char GetMaskedChar(this IEnumerable<char> chars)
        {
            const char space = ' ';
            var local = chars.ToArray();
            return local.Any(x => x == space) ? space : local.First();
        }

        /// <summary>
        /// Returns the masked text merged keeping blanks where they occur. Otherwise,
        /// leaves the non-blank characters untouched in the merge result.
        /// </summary>
        /// <param name="masked"></param>
        /// <returns></returns>
        public static string MergeBlankMasks(this IEnumerable<string> masked)
        {
            // Verifies that masked has all the same lengths.
            // ReSharper disable PossibleMultipleEnumeration
            var length = (from x in masked select x.Length).Distinct().Single();

            var local = (from x in masked select x.ToCharArray()).ToArray();

            return (from i in Enumerable.Range(0, length)
                select local.Select(m => m[i]).GetMaskedChar())
                .Aggregate(string.Empty, (g, c) => g + c);
        }
    }
}
