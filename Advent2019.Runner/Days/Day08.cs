using Advent2019.Day08;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Runner.Days
{
    public class Day08A : Runner
    {
        protected override void DoRun()
        {
            var digits = LineReader.Read("day08-input.txt");

            var image = new Image(digits.First(), 25, 6);

            var checksum = image.Checksum();

            Console.WriteLine($"Checksum = {checksum}");
        }
    }

    public class Day08B: Runner
    {
        protected override void DoRun()
        {
            var digits = LineReader.Read("day08-input.txt");

            var image = new Image(digits.First(), 25, 6);

            var decoded = image.RawDecode();

            foreach (var row in Enumerable.Range(0, decoded.Height))
            {
                foreach (var col in Enumerable.Range(0, decoded.Width))
                {
                    var value = decoded.Rows[row][col];

                    var output = (value == 1) ? Convert.ToChar(value) : ' ';

                    Console.Write(output);
                }

                Console.WriteLine();
            }                     
        }
    }
}
