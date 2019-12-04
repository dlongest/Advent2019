using Advent2019.Day04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Runner.Days
{
    public class Day04A : Runner
    {
        protected override void DoRun()
        {
            var start = 271973;
            var end = 785961;
            var count = end - start + 1;
            
            var validator = new CompositePasswordValidator(new LengthPasswordValidator(),
                                                        new IncreasingSequencePasswordValidator(),
                                                        new RepeatingGroupPasswordValidator());

            var valid = Enumerable.Range(start, count).Count(r => validator.Validate(r.ToString()));

            Console.WriteLine($"Number Valid Passwords = {valid}");
        }
    }

    public class Day04B : Runner
    {
        protected override void DoRun()
        {
            
            var start = 271973;
            var end = 785961;
            var count = end - start + 1;

            var validator = new CompositePasswordValidator(new LengthPasswordValidator(),
                                                        new IncreasingSequencePasswordValidator(),
                                                        new IsolatedRepeatingGroupPasswordValidator());

            var valid = Enumerable.Range(start, count).Count(r => validator.Validate(r.ToString()));
            
            Console.WriteLine($"Number Valid Passwords = {valid}");
        }
    }
}
