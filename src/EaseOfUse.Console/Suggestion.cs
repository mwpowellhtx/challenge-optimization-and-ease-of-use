using System;

namespace EaseOfUse
{
    internal class Suggestion : IComparable<Suggestion>
    {
        public readonly string Tag;
        public readonly int Edits;
        public readonly int Count;

        public Suggestion(string tag, int edits, int count)
        {
            Tag = tag;
            Edits = edits;
            Count = count;
        }

        public int CompareTo(Suggestion suggestion)
        {
            var compare = Edits - suggestion.Edits;

            if (compare == 0)
                compare = suggestion.Count - Count;

            if (compare == 0)
            {
                compare = string.Compare(suggestion.Tag, Tag,
                    StringComparison.CurrentCultureIgnoreCase);
            }

            return compare;
        }
    }
}
