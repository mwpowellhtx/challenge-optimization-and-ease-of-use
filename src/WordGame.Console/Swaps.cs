using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WordGame
{
    /// <summary>
    /// Represents a set of verified swap requests.
    /// </summary>
    public class Swaps : IReadOnlyCollection<int>
    {
        private readonly IList<int> _swaps;

        private static IEnumerable<int> VerifySwaps(int count, params int[] swaps)
        {
            // (0 <= S < 50)
            if (count < 0 || count >= 50)
            {
                var message = string.Format("Expected between 0 and 50 swaps but was {{{0}}}.",
                    count);

                throw new ArgumentException(message, "count")
                {
                    Data = {{"count", count}}
                };
            }

            if (swaps.Length == count)
                return swaps;

            {
                var message = string.Format("Incorrect number of swaps expected {{{0}}} but was {{{1}}}.",
                    count, swaps.Length);

                throw new ArgumentException(message, "count");
            }
        }

        public Swaps(int count, params int[] swaps)
        {
            _swaps = VerifySwaps(count, swaps).ToList();
        }

        public int Count
        {
            get { return _swaps.Count; }
        }

        public IEnumerator<int> GetEnumerator()
        {
            return _swaps.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
