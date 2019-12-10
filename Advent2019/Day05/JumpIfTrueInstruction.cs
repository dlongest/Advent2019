using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day05
{

    [OpCode(5)]
    public class JumpIfTrueInstruction : Instruction
    {
        private const int parameterCount = 2;
        private const int instructionStep = parameterCount + 1;

        public JumpIfTrueInstruction(int instructionAddress, IntCodeComputer host) : base(instructionAddress, host)
        {
        }

        public override void Execute()
        {
            var parameters = Parameter.From(this.Address, parameterCount, this.Host);

            if (parameters[0].ReadValue() != 0)
            {
                this.Host.MoveTo(parameters[1].ReadValue());
            }
            else
            {
                this.Host.MoveForward(instructionStep);
            }
        }
    }
}
