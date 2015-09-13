using System.Collections.Generic;
using System.Linq;

namespace Parade
{
    public static class CombinatorialExtensions
    {
        ////TODO: not using this one after all...
        //public static IEnumerable<T[]> Subsets<T>(this IEnumerable<T> values, int count)
        //{
        //    var local = values.ToArray();
        //    for (var i = 0; i < local.Length - count; i++)
        //        yield return local.Skip(i).Take(count).ToArray();
        //}

        /// <summary>
        /// Permutes the <paramref name="values"/>. Note that this is recursive in nature.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> values)
        {
            // ReSharper disable PossibleMultipleEnumeration
            for (var i = 0; i < values.Count(); i++)
            {
                // Focused on the value.
                var value = new[] {values.ElementAt(i)};

                // Read everything around the value.
                var taken = (i == 0 ? new List<T>() : values.Take(i)).ToArray();
                var skipped = (i == values.Count() - 1 ? new List<T>() : values.Skip(i + 1)).ToArray();

                // Get the permutations of everything around the value.
                var permutations = taken.Concat(skipped).Permute().ToArray();

                if (!permutations.Any())
                    yield return value;
                else
                {
                    foreach (var p in permutations)
                        yield return value.Concat(p).ToArray();
                }
            }
        }
    }

    public class Permutator<T>
    {
        private readonly T[] _values;

        private readonly IList<int> _indexes;

        private readonly IList<int> _permutation;

        //private int _begin;
        //private int _end;

        public Permutator(params T[] values)
        {
            _values = values.ToArray();
            _indexes = (from i in Enumerable.Range(0, _values.Length) select i).ToList();
            _permutation = _indexes.ToList();
            //_begin = 0;
            //_end = _permutation.Count - 1;
        }

        public IEnumerable<T> Values
        {
            get { return from i in _permutation.Take(_indexes.Count) select _values[i]; }
        }

        private static void Swap(ref int a, ref int b)
        {
            var x = a;
            a = b;
            b = a;
        }

        private static void Swap(IList<int> list, int a, int b)
        {
            var temp = list[a];
            list[a] = list[b];
            list[b] = temp;
        }

        private static void Reverse(IList<int> list, int i, int j)
        {
            for (; i < j; i++, j--) Swap(list, i, j);
        }

        private bool TryPermute()
        {
            int k;

            for (k = 0; k < _indexes.Count - 1; k++)
                if (_permutation[k] < _permutation[k + 1]) break;

            if (k >= _permutation.Count) return false;

            int l;

            for (l = k + 1; l < _permutation.Count - 1; l++)
                if (_permutation[k] < _permutation[l]) break;

            Swap(_permutation, k, l);

            Reverse(_permutation, k, _permutation.Count - 1);

            return true;
        }

        //private bool Permute()
        //{
        //    if (_begin == _end)
        //        return false;

        //    var i = _begin;
        //    ++i;
        //    if (i == _end)
        //        return false;

        //    i = _end;
        //    --i;

        //    while (true)
        //    {
        //        var j = i;
        //        --i;

        //        if (_permutation[i] < _permutation[j])
        //        {
        //            var k = _end;

        //            while (_permutation[i] >= _permutation[--k])
        //            {
        //                // Pass...
        //            }

        //            Swap(ref i, ref k);
        //            Reverse(_permutation, j, _end);
        //            return true;
        //        }

        //        if (i != _begin) continue;

        //        Reverse(_permutation, _begin, _end);
        //        return false;
        //    }
        //}

        public bool Next()
        {
            if (!TryPermute()) return false;
            return !_permutation.SequenceEqual(_indexes);
        }
    }
}
