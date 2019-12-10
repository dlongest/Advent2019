using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day05
{
    [OpCode(3)]
    public class InputInstruction : Instruction
    {
        private const int parameterCount = 1;
        private const int instructionStep = parameterCount + 1;

        public InputInstruction(int instructionAddress, IntCodeComputer host) : base(instructionAddress, host)
        {
        }

        public override void Execute()
        {
            var parameters = Parameter.From(this.Address, parameterCount, this.Host);

            var input = this.Host.ReadInput();

            parameters.Single().WriteValue(input);

            this.Host.MoveForward(instructionStep);
        }
    }
}
