namespace EaseOfUse
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class Program
    {
        private static IEnumerable<string> TestCases
        {
            get
            {
                yield return @"5
bake,apple pie
bake,bread
bake, cherry pie
buy,apples,Gold delicious
buy,apples,Red Delicious
3
aa
baker
bu";
                yield return @"8
bake,apple pie
bake,bread
bake, cherry pie
buy,apples,Gold delicious
buy,apples,Red Delicious
cook,chicken, stuffing
bake, cookie, sheet
bakery,cookies
9
aa
baker
bu
cpo
coo
cooi
cookk

cpo apl";
            }
        }

        private static void Main(string[] args)
        {

#if DEVELOP

            foreach (var testCase in TestCases)
            {
                using (var reader = new StringReader(testCase))
                {
                    using (new TagSuggester(reader, Console.Out))
                    {
                        // Disposable nature handles all the reading running and reporting.
                    }
                }
            }

#else

            using (new TagSuggester(Console.In, Console.Out))
            {
                // Disposable nature handles all the reading running and reporting.
            }

#endif

        }
    }
}
