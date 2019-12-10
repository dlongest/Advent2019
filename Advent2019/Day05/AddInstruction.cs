using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day05
{
    [OpCode(1)]
    public class AddInstruction : Instruction
    {
        private const int parameterCount = 3;
        private const int instructionStep = parameterCount + 1;

        public AddInstruction(int instructionAddress, IntCodeComputer host) : base(instructionAddress, host)
        {
        }

        public override void Execute()
        {
            var parameters = Parameter.From(this.Address, parameterCount, this.Host);

            var result = parameters[0].ReadValue() + parameters[1].ReadValue();

            parameters[2].WriteValue(result);

            this.Host.MoveForward(instructionStep);
        }
    }
}
