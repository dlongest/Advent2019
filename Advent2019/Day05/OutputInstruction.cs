using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day05
{
    [OpCode(4)]
    public class OutputInstruction : Instruction
    {
        private const int parameterCount = 1;
        private const int instructionStep = parameterCount + 1;

        public OutputInstruction(int instructionAddress, IntCodeComputer host) : base(instructionAddress, host)
        {
        }

        public override void Execute()
        {
            var parameters = Parameter.From(this.Address, parameterCount, this.Host);

            var value = parameters.Single().ReadValue();

            this.Host.WriteToOutput(value);

            this.Host.MoveForward(instructionStep);
        }
    }
}
