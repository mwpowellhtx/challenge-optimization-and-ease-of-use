using System;
using System.Linq;

namespace WordGame
{
    /// <summary>
    /// Represents the ability to analyze, match, and reduce any one compound
    /// in a given string of elemental <see cref="Crystals"/>.
    /// </summary>
    public class ElementalCompound
    {
        private static readonly char[] ValidCrystals;

        private static readonly int CompoundLength;

        private static readonly string[] BasicCompounds = {"FIZ", "BAR", "BAZ"};

        private static readonly string[] ReversedCompounds;

        public static string VerifyCrystals(string crystals)
        {
            if (!crystals.ToCharArray().Except(ValidCrystals).Any())
                return crystals;

            var message = string.Format("Invalid crystals {{{0}}} expected to be among {{{1}}}.",
                crystals, string.Join(", ", from x in ValidCrystals select string.Format("'{0}'", x)));

            throw new ArgumentException(message, "crystals");
        }

        static ElementalCompound()
        {
            ValidCrystals = new[] {'F', 'I', 'Z', 'B', 'A', 'R'};
            ReversedCompounds = (from x in BasicCompounds select x.ReverseText()).ToArray();
            CompoundLength = (from x in BasicCompounds select x.Length).Distinct().Single();
        }

        public int Position { get; private set; }

        public string Crystals { get; private set; }

        private string _candidate;

        public string Candidate
        {
            get
            {
                // Focus on only that text which should be considered, notwithstanding end of string.
                return _candidate ?? (_candidate = Crystals.Substring(Position,
                    Math.Min(CompoundLength, Crystals.Length - Position)));
            }
        }

        /// <summary>
        /// Returns whether <paramref name="text"/> exists in any of the <paramref name="compounds"/>.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="compounds"></param>
        /// <returns></returns>
        private static bool Evaluate(string text, params string[] compounds)
        {
            return compounds.Any(text.Equals);
        }

        private bool? _matched;

        public bool IsMatch
        {
            get
            {
                /* Herein lies the crux of the matter. While "overlapping" is an interesting
                 * observation, it is unnecessary in order to achieve the desired outcome,
                 * indeed would only serve to needlessly complicate the matter. */

                return (_matched ?? (_matched
                    = Evaluate(Candidate, BasicCompounds)
                      || Evaluate(Candidate, ReversedCompounds))).Value;
            }
        }

        public string RemoveMatch()
        {
            var removed = !IsMatch ? Crystals : Crystals.MaskText(Position, CompoundLength);
            return removed;
        }

        public ElementalCompound(int position, string crystals)
        {
            Position = position;
            Crystals = crystals;
        }
    }
}
