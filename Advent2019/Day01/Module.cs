using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Problem01
{
    public class Module
    {
        public Module(double mass)
        {
            this.Mass = mass;
        }

        public int HowMuchFuel()
        {
            var fuel = Math.Truncate(this.Mass / 3) - 2;

            return Convert.ToInt32(fuel);
        }

        public double Mass { get; private set; }

        public static int HowMuchFuel(IEnumerable<Module> modules)
        {
            return modules.Select(m => m.HowMuchFuel()).Sum();
        }
    }   
}
