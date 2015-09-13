using System;
using System.Collections.Generic;
using System.IO;

namespace Parade
{
    class Program
    {

#if DEVELOP

        private static IEnumerable<string> TestCases
        {
            get
            {
                yield return @"7 1
0 2 5 5 4 0 6
3 1";
                yield return @"7 1
0 3 5 5 5 1 0
3 2";
                yield return @"10 2
4 5 0 2 5 6 4 0 3 5
3 1 2 2";
                yield return @"15 1
1 2 4 3 8 0 0 1 2 4 6 5 4 0 0
6 1";
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
                    using (new Budget(reader, Console.Out))
                    {
                    }
                }
            }

#else

            using (new Budget(Console.In, Console.Out))
            {
                // Disposable object handles the details within.
            }

#endif

        }
    }
}
