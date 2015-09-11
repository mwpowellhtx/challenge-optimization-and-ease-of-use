using System;
using System.IO;
using System.Linq;
using Challenge.Core;

namespace WordGame
{
    /// <summary>
    /// Represents the Game, knows how to read its input and report its output. Coordinates
    /// the top level <see cref="Workbench"/> and <see cref="Swaps"/> interactions.
    /// </summary>
    public class Game : ChallengeBase
    {
        private Workbench _workbench;

        private Swaps _swaps;

        public Game(TextReader reader, TextWriter writer)
            : base(reader, writer)
        {
        }

        protected override string[] ReadLines(TextReader reader)
        {
            var lines = base.ReadLines(reader);

            if (lines.Length != 4)
                throw new ArgumentException("Expected 4 lines of input.", "reader");

            return lines;
        }

        protected override void Read(TextReader reader)
        {
            var lines = ReadLines(reader);

            // TODO: could trim the string, should also check for caps, and/or just ToUpper
            _workbench = new Workbench(int.Parse(lines[0]), lines[1]);

            _swaps = new Swaps(int.Parse(lines[2]),
                (from x in lines[3].Split(' ') select int.Parse(x)).ToArray());
        }

        protected override void Run()
        {
            _workbench.Evaluate(_swaps);
        }

        protected override void Report(TextWriter writer)
        {
            writer.WriteLine(_workbench.ElementalCrystals);
        }
    }
}
