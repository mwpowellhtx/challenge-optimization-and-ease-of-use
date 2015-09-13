using System;

namespace Parade
{
    public class CityBlock : ICloneable
    {
        private static int _count;

        public readonly int Number;

        /// <summary>
        /// Gets the MaxThreatLevel: Math.Pow(10, 7)
        /// </summary>
        public static readonly int MaxThreatLevel = (int) Math.Pow(10, 7);

        private static int VerifyThreatLevel(int value)
        {
            if (value >= 0 && value <= MaxThreatLevel)
                return value;

            var message = string.Format("Threat level {{{0}}} out of Range", value);

            throw new ArgumentException(message, "value");
        }

        public CityBlock(int threatLevel)
        {
            _threatLevel = VerifyThreatLevel(threatLevel);
            Number = _count++;
        }

        private CityBlock(CityBlock other)
            : this(other._threatLevel)
        {
            Number = other.Number;
        }

        public Force Force { get; set; }

        /// <summary>
        /// Gets whether the CityBlock IsPatrolled.
        /// </summary>
        public bool IsPatrolled
        {
            get { return Force != null; }
        }

        /// <summary>
        /// Gets whether not <see cref="Parade.Force.HasRange"/>.
        /// </summary>
        private bool IsPatrolDummy
        {
            get { return Force!= null && !Force.HasRange; }
        }

        private readonly int _threatLevel;

        public int ThreatLevel
        {
            get { return IsPatrolled && !IsPatrolDummy ? 0 : _threatLevel; }
        }

        public object Clone()
        {
            return new CityBlock(this);
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} ({2})", Number, ThreatLevel, _threatLevel);
        }
    }
}
