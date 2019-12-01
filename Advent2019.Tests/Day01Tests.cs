using Advent2019.Problem01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent2019.Tests
{
    public class Day01Tests
    {
        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void Module_ComputesCorrectFuelNeeded(double mass, int expected)
        {
            var sut = new SimpleFuelCalculator();

            var actual = sut.Calculate(mass);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void RecursingFuelCalculator_Correctly_AdjustsForFuelWeight(double mass, int expected)
        {
            var sut = new RecursingFuelCalculator(new SimpleFuelCalculator());

            var actual = sut.Calculate(mass);

            Assert.Equal(expected, actual);
        }
    }
}
