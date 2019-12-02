using Advent2019.Day02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent2019.Tests
{
    public class Day02Tests
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(99, false)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        [InlineData(1000, false)]
        public void IntCode_DeterminesIfCode_IsInstruction_Correctly(int intCode, bool expected)
        {
            Assert.Equal(expected, new IntCode(intCode).IsInstruction);
        }

        [Theory]
        [InlineData(1, 2, 3, 5)]
        [InlineData(1, 70, 2, 72)]
        [InlineData(2, 3, 6, 18)]
        [InlineData(2, 5, 21, 105)]
        public void IntCode_ExecutesOperation_Correctly(int opCode, int leftCode, int rightCode, int expectedValue)
        {
            var op = new IntCode(opCode);
            var left = new IntCode(leftCode);
            var right = new IntCode(rightCode);

            var actual = op.Operate(left, right);

            Assert.Equal(expectedValue, actual.Value);
        }
    }

    public class IntCodeProgramTests
    {
        [Theory]
        [InlineData("1,0,0,0,99", "2,0,0,0,99")]
        [InlineData("2,3,0,3,99", "2,3,0,6,99")]
        [InlineData("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [InlineData("1,1,1,4,99,5,6,0,99","30,1,1,4,2,5,6,0,99")]
        [InlineData("1,9,10,3,2,3,11,0,99,30,40,50", "3500,9,10,70,2,3,11,0,99,30,40,50")]
        public void IntCodeProgram_IsCorrect(string inputString, string expectedOutputString)
        {
            var sut = new IntCodeProgram();

            var intCodes = IntCode.FromCommaSeparated(inputString);

            var actual = sut.Process(intCodes);

            var actualOutputString = string.Join(",", actual.Select(a => a.Value));

            Assert.Equal(actualOutputString, expectedOutputString);
        }
    }

    public class SearchRangeTests
    {
        [Fact]
        public void SearchRange_Range_GeneratesInclusiveRange()
        {
            var actual = new SearchRange(0, 3, 5).Range();

            Assert.Equal(actual, new[] { 3, 4, 5 });
        }
    }
}
