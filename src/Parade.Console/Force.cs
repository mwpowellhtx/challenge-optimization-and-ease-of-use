namespace Parade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Force : ICloneable
    {
        public readonly Guid Id = Guid.NewGuid();

        public int Range { get; private set; }

        public bool HasRange
        {
            get { return Range > 0; }
        }

        public int DummyRange
        {
            get { return HasRange ? Range : 1; }
        }

        public static Force Dummy
        {
            get { return new Force(0); }
        }

        public Force(int range)
        {
            Range = range;
        }

        private Force(Force other)
        {
            Range = other.Range;
            Id = other.Id;
        }

        public bool TryPatrol(IEnumerable<CityBlock> blocks)
        {
            var eligible = blocks.SkipWhile(x => x.IsPatrolled).ToArray();

            if (eligible.Count() < DummyRange)
                return false;

            foreach (var block in eligible.Take(DummyRange))
                block.Force = this;

            return true;
        }

        public object Clone()
        {
            return new Force(this);
        }

        public override string ToString()
        {
            return string.Format("{0:D} ({1})", Id, DummyRange);
        }
    }
}
