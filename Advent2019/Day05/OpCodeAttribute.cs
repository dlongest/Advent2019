using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day05
{
    public class OpCodeAttribute : Attribute
    {
        public OpCodeAttribute(int opCode)
        {
            this.OpCode = opCode;
        }

        public int OpCode { get; private set; }
    }
}
