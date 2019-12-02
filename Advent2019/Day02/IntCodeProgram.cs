using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day02
{
    public class IntCodeProgram
    {
        public IntCode[] Process(IntCode[] program)
        {
            int currentPosition = 0;

            while (!program[currentPosition].IsHalt)
            {
                var left = GetLeft(program, currentPosition);
                var right = GetRight(program, currentPosition);

                var result = Compute(program, currentPosition, left, right);

                StoreResult(program, currentPosition, result);

                currentPosition = AdvanceToNextInstruction(currentPosition);
            }

            return program;
        }

        public int AdvanceToNextInstruction(int currentPosition)
        {
            return currentPosition + 4;
        }

        public IntCode GetLeft(IntCode[] program, int currentPosition)
        {
            return program[program[currentPosition + 1].Value];
        }

        public IntCode GetRight(IntCode[] program, int currentPosition)
        {
            return program[program[currentPosition + 2].Value];
        }

        public IntCode Compute(IntCode[] program, int currentPosition, IntCode left, IntCode right)
        {
            return program[currentPosition].Operate(left, right);
        }

        public void StoreResult(IntCode[] program, int currentPosition, IntCode result)
        {
            program[program[currentPosition + 3].Value] = result;
        }
    }
}
