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

        [Theory]
        [InlineData("1,0,0,3,99", typeof(AddInstruction))]
        [InlineData("2,0,0,3,99", typeof(MultiplyInstruction))]
        [InlineData("3,0,0,3,99", typeof(InputInstruction))]
        [InlineData("4,0,0,3,99", typeof(OutputInstruction))]
        [InlineData("1001,0,0,3,99", typeof(AddInstruction))]
        [InlineData("0002,0,0,3,99", typeof(MultiplyInstruction))]
        [InlineData("1103,0,0,3,99", typeof(InputInstruction))]
        [InlineData("1004,0,0,3,99", typeof(OutputInstruction))]
        [InlineData("99,1,2,2,4", typeof(HaltInstruction))]
        public void OpCode_CreatesInstance_ForNumericOpCode(string registers, Type expectedType)
        {
            var actual = Instruction.From(0, IntCodeComputer.FromCommaSeparated(registers));

            Assert.IsType(expectedType, actual);
        }

        [Theory]
        [InlineData("99,1,2,2,4")]
        public void Halt_Instruction_StopsProgramFromRunning(string registers)
        {
            var expected = new[] { 99, 1, 2, 2, 4 };

            var host = IntCodeComputer.FromCommaSeparated(registers);

            Instruction.From(0, host).Execute();

            Assert.False(host.IsRunning);
            Assert.Equal(expected, host.Registers);
        }

        [Theory]
        [InlineData("1,0,0,0,99", "2,0,0,0,99")]
        [InlineData("2,3,0,3,99", "2,3,0,6,99")]
        [InlineData("1101,100,-1,4,0", "1101,100,-1,4,99")]
        [InlineData("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [InlineData("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        [InlineData("1,9,10,3,2,3,11,0,99,30,40,50", "3500,9,10,70,2,3,11,0,99,30,40,50")]
        public void IntCodeComputer_Process_EndsWithCorrectRegisters(string input, string expected)
        {
            var sut = IntCodeComputer.FromCommaSeparated(input);

            sut.Process();

            Assert.Equal(expected, string.Join(",", sut.Registers));
        }

        [Fact]
        public void IntCodeComputer_Input_AndOutput_LinkedTogether_Work()
        {
            var sut = IntCodeComputer.FromCommaSeparated("3,0,4,0,99");

            sut.AddToInput(42);

            sut.Process();

            Assert.Empty(sut.Input);
            Assert.Single(sut.Output);
            Assert.Equal(42, sut.Output[0]);
            Assert.Equal(new[] { 42, 0, 4, 0, 99 }, sut.Registers);
        }

        [Theory]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 8, "3,9,8,9,10,9,4,9,99,1,8", 1)]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 7, "3,9,8,9,10,9,4,9,99,0,8", 0)]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 9, "3,9,8,9,10,9,4,9,99,0,8", 0)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 8, "3,3,1108,1,8,3,4,3,99", 1)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 7, "3,3,1108,0,8,3,4,3,99", 0)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 9, "3,3,1108,0,8,3,4,3,99", 0)]
        public void EqualsInstruction_ComputesCorrectResult_WhenTrue(string inputRegisters, int input,
                                                                     string expectedRegisters, int expectedOutput)
        {
            var sut = IntCodeComputer.FromCommaSeparated(inputRegisters);

            sut.AddToInput(input);
            sut.Process();

            Assert.Equal(expectedRegisters, string.Join(",", sut.Registers));
            Assert.Equal(expectedOutput, sut.ReadOutput());
            Assert.Empty(sut.Output);
        }

        [Theory]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 7, "3,9,7,9,10,9,4,9,99,1,8", 1)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 8, "3,9,7,9,10,9,4,9,99,0,8", 0)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 9, "3,9,7,9,10,9,4,9,99,0,8", 0)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 7, "3,3,1107,1,8,3,4,3,99", 1)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 8, "3,3,1107,0,8,3,4,3,99", 0)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 9, "3,3,1107,0,8,3,4,3,99", 0)]
        public void LessInstruction_ComputesCorrectResult(string inputRegisters, int input,
                                                          string expectedRegisters, int expectedOutput)
        {
            var sut = IntCodeComputer.FromCommaSeparated(inputRegisters);

            sut.AddToInput(input);
            sut.Process();

            Assert.Equal(expectedRegisters, string.Join(",", sut.Registers));
            Assert.Equal(expectedOutput, sut.ReadOutput());
            Assert.Empty(sut.Output);
        }

        [Theory]
        [InlineData("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 0, "3,12,6,12,15,1,13,14,13,4,13,99,0,0,1,9", 0 )]
        [InlineData("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 1, "3,12,6,12,15,1,13,14,13,4,13,99,1,1,1,9", 1)]
        public void JumpInstructions_ComputeCorrectResult(string inputRegisters, int input,
                                                          string expectedRegisters, int expectedOutput)
        {
            var sut = IntCodeComputer.FromCommaSeparated(inputRegisters);

            sut.AddToInput(input);
            sut.Process();

            Assert.Equal(expectedRegisters, string.Join(",", sut.Registers));
            Assert.Equal(expectedOutput, sut.ReadOutput());
            Assert.Empty(sut.Output);
        }

        [Theory]
        [InlineData("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20," +
                    "1105, 1, 46, 104,999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99", 7, 999)]
        [InlineData("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20," +
                    "1105, 1, 46, 104,999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99", 8, 1000)]
        [InlineData("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20," +
                    "1105, 1, 46, 104,999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99", 9, 1001)]
        public void CompleteJumpTest(string inputRegisters, int input, int expectedOutput)
        {
            var sut = IntCodeComputer.FromCommaSeparated(inputRegisters);

            sut.AddToInput(input);

            sut.Process();

            Assert.Equal(expectedOutput, sut.ReadOutput());
            Assert.Empty(sut.Output);
        }

        [Fact]
        public void Parameter_From_CreatesParameters_With_Padded_0()
        {
            var host = IntCodeComputer.FromCommaSeparated("1101,100,-1,4,0");

            var parameters = Parameter.From(0, 3, host);

            Assert.Equal(1, parameters[0].Address);
            Assert.IsType<ImmediateMode>(parameters[0].ParameterMode);
            Assert.Equal(2, parameters[1].Address);
            Assert.IsType<ImmediateMode>(parameters[1].ParameterMode);
            Assert.Equal(3, parameters[2].Address);
            Assert.IsType<PositionMode>(parameters[2].ParameterMode);
        }

        [Fact]
        public void ImmediateMode_ReadsCorrectValue()
        {
            var host = IntCodeComputer.FromCommaSeparated("1,2,3,4");

            var sut = new ImmediateMode();

            Assert.Equal(1, sut.ReadValue(0, host));
            Assert.Equal(2, sut.ReadValue(1, host));
            Assert.Equal(3, sut.ReadValue(2, host));
            Assert.Equal(4, sut.ReadValue(3, host));
        }

        [Fact]
        public void ImmediateMode_Write_Value_ThrowsException()
        {
            var host = IntCodeComputer.FromCommaSeparated("1,2,3,4");

            var sut = new ImmediateMode();

            Assert.Throws<InvalidOperationException>(() => sut.WriteValue(0, 96, host));
        }

        [Fact]
        public void PositionMode_ReadsCorrectValue()
        {
            var host = IntCodeComputer.FromCommaSeparated("1,0,4,0,3");

            var sut = new PositionMode();

            Assert.Equal(0, sut.ReadValue(0, host));
            Assert.Equal(1, sut.ReadValue(1, host));
            Assert.Equal(3, sut.ReadValue(2, host));
        }

        [Fact]
        public void PositionMode_WritesValue_ToCorrectAddress()
        {
            var host = IntCodeComputer.FromCommaSeparated("1,2,3,4");

            var sut = new PositionMode();

            sut.WriteValue(0, 96, host);
            sut.WriteValue(2, 42, host);

            Assert.Equal(1, host.Registers[0]);
            Assert.Equal(96, host.Registers[1]);
            Assert.Equal(3, host.Registers[2]);
            Assert.Equal(42, host.Registers[3]);
        }
    }
}
