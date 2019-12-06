using Advent2019.Day06;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Runner.Days
{
    public class Day06A : Runner
    {
        protected override void DoRun()
        {
            var orbits = LineReader.Read("day06-input.txt",
                                         line => line.Split(new string[] { ")" }, StringSplitOptions.RemoveEmptyEntries));

            var orbitMap = orbits.Aggregate(new OrbitMap(), (acc, orb) => acc.AddOrbit(orb[0], orb[1]), acc => acc);

            var checksum = orbitMap.Checksum();

            Console.WriteLine($"Orbit Checksum = {checksum}");
        }
    }

    public class Day06B : Runner
    {
        protected override void DoRun()
        {
            var orbits = LineReader.Read("day06-input.txt",
                                         line => line.Split(new string[] { ")" }, StringSplitOptions.RemoveEmptyEntries));

            var orbitMap = orbits.Aggregate(new OrbitMap(), (acc, orb) => acc.AddOrbit(orb[0], orb[1]), acc => acc);

            var transfers = orbitMap.Transfers("YOU", "SAN");

            Console.WriteLine($"Transfers Needed For YOU to SAN = {transfers}");
        }
    }
}
