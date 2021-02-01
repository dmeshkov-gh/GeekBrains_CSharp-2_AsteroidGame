using System;
using System.Collections.Generic;
using System.Linq;

namespace Leeson_4_ConsoleLINQTests
{
    //а) Свернуть обращение к OrderBy с использованием лямбда-выражения $.
    //б) * Развернуть обращение к OrderBy с использованием делегата Predicate<T>.
    class Program
    {
        static void Main(string[] args)
        {
            Predicate<KeyValuePair<string, int>> predicate = new Predicate<KeyValuePair<string, int>>(IsPostive);

            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"four", 4 },
                {"two", 2 },
                { "one", 1 },
                {"three", 3 },
            };

            var ordered_dict = dict.OrderBy(k => k.Value);

            // var new_list = ordered_dict.ToList().FindAll(k => k.Value > 0);

            var new_list = ordered_dict.OrderByDescending(k => k.Value).ToList().FindAll(predicate);

            foreach (var pair in new_list)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
        }

        static bool IsPostive(KeyValuePair<string, int> pair)
        {
            return pair.Value > 1;
        }
    }
}
