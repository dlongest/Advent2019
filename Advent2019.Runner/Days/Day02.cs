using Advent2019.Day02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Runner.Days
{
    public class Day02A : Runner
    {
        protected override void DoRun()
        {
            var commaSeparated = LineReader.Read("day02-input.txt").First();

            var input = IntCode.FromCommaSeparated(commaSeparated);

            input[1] = new IntCode(12);
            input[2] = new IntCode(2);

            var output = new IntCodeProgram().Process(input);

            Console.WriteLine($"Value at position 0 = {output[0]}");
        }
    }

    public class Day02B : Runner
    {
        protected override void DoRun()
        {
            var commaSeparated = LineReader.Read("day02-input.txt").First();

            var input = IntCode.FromCommaSeparated(commaSeparated);

            var searchResult = new GridSearch().Search(input, new SearchRange(1, 0, 99), new SearchRange(2, 0, 99), 19690720);

            Console.WriteLine($"Target achieved when Noun = {searchResult.Noun} and Verb = {searchResult.Verb} with combination = {100 * searchResult.Noun + searchResult.Verb}");
        }
    }
}
