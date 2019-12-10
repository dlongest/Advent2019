using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day05
{
    public abstract class ParameterMode
    {
        public abstract int ReadValue(int address, IntCodeComputer host);

        public abstract void WriteValue(int address, int value, IntCodeComputer host);

    }

    public class ImmediateMode : ParameterMode
    {
        public override int ReadValue(int parameterAddress, IntCodeComputer host)
        {
            return host.Registers[parameterAddress];
        }

        public override void WriteValue(int parameterAddress, int value, IntCodeComputer host)
        {
            throw new InvalidOperationException("Writes cannot be done in Immediate Mode");
        }
    }

    public class PositionMode : ParameterMode
    {
        public override int ReadValue(int parameterAddress, IntCodeComputer host)
        {
            var registerValue = host.Registers[parameterAddress];

            return host.Registers[registerValue];
        }

        public override void WriteValue(int parameterAddress, int value, IntCodeComputer host)
        {
            var registerValue = host.Registers[parameterAddress];

            host.Registers[registerValue] = value;
        }
    }
}
