using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day05
{
   
    public class IntCodeComputer
    {
        private int pointer = 0;
        private bool running = true;

        public IntCodeComputer(IEnumerable<int> registers)
        {
            this.Registers = registers?.ToArray() ?? new int[0];
        }

        public int[] Registers { get; private set; }

        public int[] Input { get; private set; }

        public string[] Output { get; private set; }

        
        public int ReadRegister()
        {
            return this.Registers[pointer];
        }

        public IntCodeComputer MoveNext()
        {
            if (this.pointer++ == this.Registers.Length)
            {
                this.running = false;
            }            

            return this;
        }

        public bool IsRunning { get { return this.running &&  pointer < this.Registers.Length; } }

        public void Process()
        {
            while (IsRunning)
            {
                Instruction.From(this).Execute();
            }
        }
              
        public void Halt()
        {
            this.running = false;
        }

        public int ReadInput()
        {
            return 0;
        }

        public void WriteOutput(string value)
        {
            this.Output = this.Output.Append(value).ToArray();
        }        

        public static IntCodeComputer FromCommaSeparated(string registers)
        {
            return new IntCodeComputer(registers.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                                 .Select(s => Int32.Parse(s)));
        }

        public static IntCodeComputer Empty = new IntCodeComputer(null);

        public static IntCodeComputer Halting = new IntCodeComputer(new int[] { 99 });
    }

    public abstract class Instruction
    {
        public Instruction(IntCodeComputer host)
        {
            this.OpCode = host.ReadRegister();
            host.MoveNext();
            this.Host = host;
        }

        public int OpCode  { get; private set; }

        private static IDictionary<Type, OpCodeAttribute[]> opCodes;

        static Instruction()
        {
            opCodes = System.Reflection.Assembly.GetAssembly(typeof(Instruction))
                                       .GetTypes()
                                       .ToDictionary(k => k,
                                                     v => v.GetCustomAttributes(typeof(OpCodeAttribute), false).Cast<OpCodeAttribute>().ToArray());
                                               
        }

        private static IDictionary<int, Func<Instruction>> CreateOpCodeFactory(IntCodeComputer host)
        {           
            Func<Type, IntCodeComputer, Func<Instruction>> f = (type, computer) => () => (Instruction)Activator.CreateInstance(type, new object[] { computer });

            return opCodes.Where(o => o.Value.Any())
                          .ToDictionary(k => k.Value.First().OpCode, v => f(v.Key, host));            

        }


        public static Instruction From(IntCodeComputer host)
        {
            var opCode = host.ReadRegister();

            var factory = CreateOpCodeFactory(host);

            return factory[opCode]();
        }

        public abstract void Execute();

        public IntCodeComputer Host { get; private set; }        
    }


    public class OpCodeAttribute : Attribute
    {
        public OpCodeAttribute(int opCode)
        {
            this.OpCode = opCode;
        }

        public int OpCode { get; private set; }
    }


    [OpCode(2)]
    public class MultiplyInstruction : Instruction
    {
        public MultiplyInstruction(IntCodeComputer host):base(host)
        {
        }

        public override void Execute()
        {
            
        }
    }

    [OpCode(1)]
    public class AddInstruction : Instruction
    {


        public AddInstruction(IntCodeComputer host) : base(host)
        {
        }

        

        public override void Execute()
        {
            var param1 = Parameter.InPositionMode(this.Host, this.Host.MoveNext().ReadRegister());
            var param2 = Parameter.InPositionMode(this.Host, this.Host.MoveNext().ReadRegister());

            var outputAddress = this.Host.MoveNext().ReadRegister();

            var sum = param1.Value + param2.Value;

            this.Host.Registers[outputAddress] = sum;

            this.Host.MoveNext();
        }
    }

    [OpCode(3)]
    public class InputInstruction : Instruction
    {
        public InputInstruction(IntCodeComputer host) : base(host)
        {
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }

    [OpCode(4)]
    public class OutputInstruction : Instruction
    {
        public OutputInstruction(IntCodeComputer host) : base(host)
        {
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }

    [OpCode(99)]
    public class HaltInstruction : Instruction
    {
        public HaltInstruction(IntCodeComputer host) : base(host)
        {
        }

        public override void Execute()
        {
            this.Host.Halt();
        }
    }

    public class Parameter
    {     
        public static Parameter InPositionMode(IntCodeComputer host, int address)
        {
            var registerValue = host.Registers[address];

            return new Parameter(address, host.Registers[registerValue]);
        }

        public static Parameter InImmediateMode(IntCodeComputer host, int address)
        {
            return new Parameter(address, host.Registers[address]);
        }

        private Parameter(int address, int value)
        {
            this.Address = address;
        }

        public int Address { get; private set; }

        public int Value { get; private set; }

       // public ParameterMode ParameterMode { get; private set; }
    }

    public enum ParameterMode
    {
        Position = 0, Immediate
    };
}
