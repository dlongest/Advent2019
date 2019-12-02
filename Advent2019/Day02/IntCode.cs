using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day02
{
    public class IntCode
    {       
        private static IDictionary<IntCode, Func<IntCode, IntCode, IntCode>> instructions;
        private static int[] halt = new int[] { 99 };

        static IntCode()
        {
            instructions = new Dictionary<IntCode, Func<IntCode, IntCode, IntCode>>()
            {
                { new IntCode(1), (left, right) => new IntCode(left.Value + right.Value) },
                { new IntCode(2), (left, right) => new IntCode(left.Value * right.Value) }
            };
        }

        public IntCode(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }

        public bool IsInstruction => instructions.ContainsKey(this);

        public bool IsHalt => halt.Contains(this.Value);

        public IntCode Operate(IntCode left, IntCode right)
        {
            if (!this.IsInstruction)
            {
                throw new InvalidOperationException($"Cannot perform operate using the specified Intcode: {this.Value}");
            }

            return instructions[this](left, right);

            throw new InvalidOperationException("There appears to be a mismatch in the definition of instructions and how Operate is programmed.");
        }


        public static IntCode[] FromCommaSeparated(string intCodes)
        {
            return intCodes.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => FromString(s))
                            .ToArray();
        }

        public static IntCode FromString(string intCode)
        {
            return new IntCode(Int32.Parse(intCode));
        }

        public override bool Equals(object obj)
        {
            var c = obj as IntCode;

            if (c == null)
            {
                return false;
            }

            return c.Value == this.Value;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
