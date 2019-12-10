using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day05
{
    public abstract class Instruction
    {
        public Instruction(int address, IntCodeComputer host)
        {
            this.Address = address;
            this.Host = host;
        }

        private static IDictionary<int, Func<int, IntCodeComputer, Instruction>> instructionFactory;

        static Instruction()
        {
            var opCodes = System.Reflection.Assembly.GetAssembly(typeof(Instruction))
                                       .GetTypes()
                                       .Where(t => typeof(Instruction).IsAssignableFrom(t) && !t.IsAbstract)
                                       .ToDictionary(k => k,
                                                     v => v.GetCustomAttributes(typeof(OpCodeAttribute), false).Cast<OpCodeAttribute>().ToArray());

            // Given an Instruction type and an IntCodeComputer, returns a function
            // that will produce an Instruction. 
            Func<Type, Func<int, IntCodeComputer, Instruction>> f =
                    type =>
                        (address, computer)
                            => (Instruction)Activator.CreateInstance(type, new object[] { address, computer });

            // For every Instruction type that had an OpCodeAttribute, return a dictionary
            // with key of the OpCode value and value of the generated Instruction instance
            instructionFactory = opCodes.Where(o => o.Value.Any())
                                        .ToDictionary(k => k.Value.First().OpCode, v => f(v.Key));

        }

        public static Instruction From(int address, IntCodeComputer host)
        {
            var registerValue = host.Registers[address];

            var opCode = registerValue % 100; // op code is least significant digits of the register value

            return instructionFactory[opCode](address, host);
        }

        public abstract void Execute();

        public IntCodeComputer Host { get; private set; }

        public int Address { get; private set; }
    }   
}
