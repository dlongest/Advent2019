using Advent2019.Day05;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Runner.Days
{
    public class Day05A: Runner
    {
        protected override void DoRun()
        {
            var registers = LineReader.Read("day05-input.txt");

            var host = IntCodeComputer.FromCommaSeparated(registers.First());

            host.AddToInput(1);

            host.Process();

            host.Output.ToList().ForEach(h => Console.WriteLine(h));

            
        }
    }

    public class Day05B : Runner
    {
        protected override void DoRun()
        {
            var registers = LineReader.Read("day05-input.txt");

            var host = IntCodeComputer.FromCommaSeparated(registers.First());

            host.AddToInput(5);

            host.Process();

            host.Output.ToList().ForEach(h => Console.WriteLine(h));


        }
    }
}
