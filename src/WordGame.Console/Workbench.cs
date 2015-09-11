using System;
using System.Linq;

namespace WordGame
{
    /// <summary>
    /// Represents the alchemists workbench. Manages the crystals, compounds, and their interactions.
    /// </summary>
    public class Workbench
    {
        private string _crystals;

        public string ElementalCrystals
        {
            get { return _crystals; }
            private set { _crystals = ElementalCompound.VerifyCrystals(value); }
        }

        private static string VerifyCrystalCount(int count, string crystals)
        {
            // (0 < C < 100)
            if (count < 0 || count > 99)
            {
                var message = string.Format("Expected between 0 and 99 crystals but was {{{0}}}.", count);

                throw new ArgumentException(message, "count")
                {
                    Data = {{"count", count}}
                };
            }

            if (crystals.Length == count)
                return crystals;

            {
                var message = string.Format("Expected {{{0}}} crystals.", count);

                throw new ArgumentException(message, "crystals")
                {
                    Data = {{"count", count}, {"crystals", crystals}}
                };
            }
        }

        public Workbench(int count, string elementalCrystals)
        {
            ElementalCrystals = VerifyCrystalCount(count, elementalCrystals);
        }

        private static bool TrySwap(string crystals, int swap, out string result)
        {
            // Nothing is swapped when a compound is not formed.
            result = crystals;

            var swapped = crystals.SwapChars(swap, swap + 1);

            do
            {
                var eligible
                    = (from i in Enumerable.Range(0, swapped.Length)
                        select new ElementalCompound(i, swapped)).ToArray();

                // Not all eligible compounds are actually compounds at all.
                var screened
                    = (from ec in eligible
                        where ec.IsMatch
                        select ec.RemoveMatch()).ToArray();

                // Careful not to yield a forever loop.
                if (!screened.Any()) break;

                // Only do this when there are any screened compounds.
                result = swapped = screened.MergeBlankMasks().ReduceMask();

            } while (true);

            return !result.Equals(crystals);
        }

        public void Evaluate(Swaps swaps)
        {
            foreach (var swap in swaps)
            {
                var crystals = ElementalCrystals;

                if (TrySwap(crystals, swap, out crystals))
                    ElementalCrystals = crystals;
            }
        }
    }
}
