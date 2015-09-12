using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Challenge.Core;

namespace Parade
{
    /// <summary>
    /// Given the problem domain, this is about the best approach one can take. However,
    /// in a production environment, I would seriously consider employing something like
    /// a Google OR-tools, or other Constraint Solver, towards this type of a problem.
    /// </summary>
    public class Budget : ChallengeBase
    {
        public IEnumerable<CityBlock> Blocks { get; private set; } 

        public Force[] Forces { get; private set; }

        public Budget(TextReader reader, TextWriter writer)
            : base(reader, writer)
        {
        }

        protected override string[] ReadLines(TextReader reader)
        {
            var lines = base.ReadLines(reader);

            if (lines.Length == 3)
                return lines;

            var message = string.Format("Expected three (3) lines but was {{{0}}}.",
                lines.Length);

            throw new ArgumentException(message, "reader");
        }

        private static IEnumerable<CityBlock> ReadCityBlocks(int count, params int[] threatLevels)
        {
            if (threatLevels.Length == count)
                return from x in threatLevels select new CityBlock(x);

            var message = string.Format("Expected {{{0}}} city block threat levels but was {{{1}}}.",
                count, threatLevels.Length);

            throw new ArgumentException(message, "threatLevels");
        }

        private static IEnumerable<Force> ReadForces(int summaryCount, params int[] values)
        {
            var expectedValueCount = 2*summaryCount;

            if (values.Length == expectedValueCount)
            {
                for (var i = 0; i < summaryCount; i++)
                {
                    var index = i*2;

                    var range = values[index];
                    var forceCount = values[index + 1];

                    foreach (var patrol in
                        from j in Enumerable.Range(0, forceCount)
                        select new Force(range))
                    {
                        yield return patrol;
                    }
                }

                yield break;
            }

            var message = string.Format("Expected {{{0}}} values but was {{{1}}}.",
                expectedValueCount, values.Length);

            throw new ArgumentException(message, "summaryCount");
        }

        protected override void Read(TextReader reader)
        {
            var lines = ReadLines(reader);

            const char space = ' ';

            var counts = (from x in lines[0].Split(space)
                select int.Parse(x)).ToArray();

            Blocks = ReadCityBlocks(counts[0],
                (from x in lines[1].Split(space)
                    select int.Parse(x)).ToArray()).ToArray();

            var forces = (from x in ReadForces(counts[1],
                (from x in lines[2].Split(space)
                    select int.Parse(x)).ToArray())
                orderby x.Range descending
                select x).ToList();

            var blockCount = Blocks.Count();

            // Fill in some gaps with "dummy" forces: this helps the combinations go smoother.
            while (forces.Sum(x => x.DummyRange) < blockCount)
                forces.Add(Force.Dummy);

            Forces = forces.ToArray();
        }

        /// <summary>
        /// Returns the Threat Level after assigning <see cref="Force"/> to
        /// <see cref="CityBlock"/>. No work is done here to remember state of any sort,
        /// only the allocated threat level is calculated and returned. It is not the most
        /// optimized thing that can be done, but given the timeframe and problem domain,
        /// this is pretty close to being good enough.
        /// </summary>
        /// <returns></returns>
        private int Allocate()
        {
            //var comparer = new ForceComparer();

            var threatLevel = Blocks.Sum(x => x.ThreatLevel);

            /* Really, really (REALLY) avoid the R# help here: that could
             * be potentially very, very (VERY) expensive. */

            var permutator = new Permutator<Force>(Forces);

            do
            {
                var localForces = (from x in permutator.Values select x.Clone())
                    .OfType<Force>().ToList();

                // ReSharper disable once PossibleMultipleEnumeration
                var localBlocks = (from x in Blocks.ToArray()
                    select (CityBlock) x.Clone()).ToArray();

                // Assign all the possible forces we can to the best possible locations.
                foreach (var force in localForces
                    .TakeWhile(x => x.TryPatrol(localBlocks))
                    .Where(x => localBlocks.All(y => y.IsPatrolled)))
                {
                    break;
                }

                threatLevel = Math.Min(threatLevel,
                    localBlocks.Sum(x => x.ThreatLevel));

                // This is as low as we can go, no point in continuing through the (many) other permutations.
                if (threatLevel == 0) break;

            } while (permutator.Next());

            //// Permute the forces themselves.
            //var forcePermutations = Forces.Permute().ToArray();

            //// ReSharper disable once LoopCanBePartlyConvertedToQuery
            //foreach (var forces in forcePermutations)
            //{
            //    var localForces = (from x in forces
            //        select x.Clone()).OfType<Force>().ToArray();

            //    // ReSharper disable once PossibleMultipleEnumeration
            //    var localBlocks = (from x in Blocks.ToArray()
            //        select (CityBlock) x.Clone()).ToArray();

            //    // Assign all the possible forces we can to the best possible locations.
            //    foreach (var force in localForces
            //        .TakeWhile(x => x.TryPatrol(localBlocks))
            //        .Where(x => localBlocks.All(y => y.IsPatrolled)))
            //    {
            //        break;
            //    }

            //    threatLevel = Math.Min(threatLevel,
            //        localBlocks.Sum(x => x.ThreatLevel));

            //    // This is as low as we can go, no point in continuing through the (many) other permutations.
            //    if (threatLevel == 0) break;
            //}

            return threatLevel;
        }

        private int AllocatedThreatLevel { get; set; }

        protected override void Run()
        {
            AllocatedThreatLevel = Allocate();
        }

        protected override void Report(TextWriter writer)
        {
            writer.WriteLine("{0}", AllocatedThreatLevel);
        }
    }
}
