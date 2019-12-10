using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day05
{
    [OpCode(7)]
    public class LessThanInstruction : Instruction
    {
        private const int parameterCount = 3;
        private const int instructionStep = parameterCount + 1;

        public LessThanInstruction(int instructionAddress, IntCodeComputer host) : base(instructionAddress, host)
        {
        }

        public override void Execute()
        {
            var parameters = Parameter.From(this.Address, parameterCount, this.Host);

            var value = parameters[0].ReadValue() < parameters[1].ReadValue() ? 1 : 0;

            parameters[2].WriteValue(value);

            this.Host.MoveForward(instructionStep);
        }
    }
}
