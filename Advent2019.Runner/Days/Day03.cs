using Advent2019.Day03;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Runner.Days
{
    public class Day03A : Runner
    {
        protected override void DoRun()
        {
            var wires = LineReader.Read("day03-input.txt", line => Wire.FromCommaSeparated(line));

            var distance = wires.First().FindClosestCrossing(wires.Last()).Manhattan(XY.Origin);

            Console.WriteLine($"Closest crossing distance = {distance}");
        }
    }

    public class Day03B : Runner
    {
        protected override void DoRun()
        {
            var wires = LineReader.Read("day03-input.txt", line => Wire.FromCommaSeparated(line));

            var distance = wires.First().FindShortestSignalDelay(wires.Last());

            Console.WriteLine($"Shortest signal distance = {distance}");
        }
    }
}
