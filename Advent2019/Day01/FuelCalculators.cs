using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Problem01
{
    public interface IFuelCalculator
    {
        int Calculate(double mass);
    }

    public class SimpleFuelCalculator : IFuelCalculator
    {
        public int Calculate(double mass)
        {
            var fuel = Math.Truncate(mass / 3) - 2;

            return Convert.ToInt32(fuel);
        }
    }

    public class RecursingFuelCalculator : IFuelCalculator
    {
        private readonly IFuelCalculator calculator;

        public RecursingFuelCalculator(IFuelCalculator calculator)
        {
            this.calculator = calculator;
        }

        public int Calculate(double mass)
        {
            var fuel = this.calculator.Calculate(mass);

            var extraWeight = this.calculator.Calculate(fuel);

            while (extraWeight >= 0)
            {
                fuel += extraWeight;

                extraWeight = this.calculator.Calculate(extraWeight);
            }

            return fuel;
        }
    }
}
