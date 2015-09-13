using System;
using System.Collections.Generic;
using System.IO;

namespace WordGame
{
    class Program
    {

#if DEVELOP

        private static IEnumerable<string> TestCases
        {
            get
            {
                yield return @"10
BFIBZRAFBF
2
3 4";
                // Third request should swap BBFF -> BFBF which should be yield BBFF after being rejected.
                yield return @"10
BFIBZRAFBF
3
3 4 2";
            }
        }

#endif

        static void Main(string[] args)
        {

#if DEVELOP

            foreach (var testCase in TestCases)
            {
                using (var reader = new StringReader(testCase))
                {
                    using (var game = new Game(reader, Console.Out))
                    {
                        // Everything is handled internal to the game.
                    }
                }
            }

#else

            using (var game = new Game(Console.In, Console.Out))
            {
                // Everything is handled internal to the game.
            }

#endif

        }
    }
}
