using Advent2019.Day05;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent2019.Tests
{
    public class Day05Tests
    {
        [Fact]
        public void IntCodeComputer_IsRunning_After_Initialization()
        {
            var host = IntCodeComputer.FromCommaSeparated("1,9,10,3");

            Assert.True(host.IsRunning);
        }

     //   [Theory]
        [InlineData(1, typeof(AddInstruction))]
        [InlineData(2, typeof(MultiplyInstruction))]
        [InlineData(3, typeof(InputInstruction))]
        [InlineData(4, typeof(OutputInstruction))]
        public void OpCode_CreatesInstance_ForNumericOpCode(int opCode, Type expectedType)
        {
            var actual = Instruction.From(IntCodeComputer.FromCommaSeparated("1,0,0,3,99"));

            Assert.IsType(expectedType, actual);
        }

    //    [Fact]
        public void OpCode_Execute_Produces_CorrectResult()
        {
            var expected = new int[] { 4, 0, 0, 0, 99 };
            var host = IntCodeComputer.FromCommaSeparated("1,0,0,0,99");

            new AddInstruction(host).Execute();

            Assert.Equal(expected, host.Registers);
        }

      //  [Fact]
        public void IntCodeComputer_Test()
        {
            var expected = new int[] { 4, 0, 0, 0, 99 };

            var sut = IntCodeComputer.FromCommaSeparated("1,0,0,0,99");

            sut.Process();

            Assert.Equal(expected, sut.Registers);
        }
    }
}
