using Advent2019.Problem01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Runner.Days
{
    public class Day01A : Runner
    {       
        protected override void DoRun()
        {
            var input = LineReader.Read("problem01-input.txt", line => new Module(Double.Parse(line))).ToList();

            Console.WriteLine($"Total Fuel Needed = {Module.HowMuchFuel(input)}");
        }
    }

    public class Day01B : Runner
    {

        protected override void DoRun()
        {
            var input = LineReader.Read("problem01-input.txt", line => Double.Parse(line)).ToList();

            var calculator = new RecursingFuelCalculator(new SimpleFuelCalculator());

            var total = input.Select(mass => calculator.Calculate(mass)).Sum();
            
            Console.WriteLine($"Total Fuel Needed = {total}");
        }
    }   
}
