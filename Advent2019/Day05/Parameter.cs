using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day05
{
    public class Parameter
    {
        public Parameter(int address, ParameterMode parameterMode, IntCodeComputer host)
        {
            this.Address = address;
            this.Host = host;
            this.ParameterMode = parameterMode;
        }

        public int Address { get; private set; }

        public int ReadValue()
        {
            return this.ParameterMode.ReadValue(this.Address, this.Host);
        }

        public void WriteValue(int value)
        {
            this.ParameterMode.WriteValue(this.Address, value, this.Host);
        }

        public ParameterMode ParameterMode { get; private set; }

        public IntCodeComputer Host { get; private set; }

        public override bool Equals(object obj)
        {
            var p = obj as Parameter;

            if (p == null)
            {
                return false;
            }

            return this.Address.Equals(p.Address);
        }

        public override int GetHashCode()
        {
            return this.Address.GetHashCode();
        }

        private static IDictionary<int, ParameterMode> parameterModeFactory = new Dictionary<int, ParameterMode>()
        {
            { 0, new PositionMode() },
            { 1, new ImmediateMode() }
        };

        public static Parameter[] From(int opCodeRegisterAddress, int parameterCount, IntCodeComputer host)
        {
            var opCodeRegisterValue = host.Registers[opCodeRegisterAddress].ToString();

            var specifiedModes = (opCodeRegisterValue.Length < 2) ? string.Empty : opCodeRegisterValue.Substring(0, opCodeRegisterValue.Length - 2);

            var parameterModes = specifiedModes.PadLeft(parameterCount, '0')
                                               .Select(ch => ch - '0')
                                               .Reverse()
                                               .Select(pm => parameterModeFactory[pm]);

            return parameterModes.Zip(Enumerable.Range(opCodeRegisterAddress + 1, parameterModes.Count()),
                               (pm, i) => new { Index = i, ParameterMode = pm })
                                .Select(a => new Parameter(a.Index, a.ParameterMode, host))
                                .ToArray();
        }
    }    
}
