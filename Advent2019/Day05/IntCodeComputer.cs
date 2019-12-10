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
        private Queue<int> input = new Queue<int>();
        private Queue<int> output = new Queue<int>();

        public IntCodeComputer(IEnumerable<int> registers)
        {
            this.Registers = registers?.ToArray() ?? new int[0];
        }

        public int[] Registers { get; private set; }

        public int CurrentRegister()
        {
            if (this.pointer >= this.Registers.Length)
            {
                throw new InvalidOperationException("Pointer is outside the register range.");
            }

            return this.Registers[pointer];
        }

        public void MoveTo(int address)
        {
            this.pointer = address;
        }

        public void MoveForward(int steps)
        {
            this.pointer += steps;
        }

        public bool IsRunning { get { return this.running && pointer < this.Registers.Length; } }

        public void Process()
        {
            while (IsRunning)
            {
                Instruction.From(this.pointer, this).Execute();
            }
        }

        public void Halt()
        {
            this.running = false;
        }
        
        public int ReadInput()
        {
            return this.input.Dequeue();
        }

        public void AddToInput(int item)
        {
            this.input.Enqueue(item);
        }

        public int ReadOutput()
        {
            return this.output.Dequeue();
        }

        public void WriteToOutput(int item)
        {
            this.output.Enqueue(item);
        }

        public int[] Input {  get { return this.input.ToArray();  } }

        public int[] Output { get { return this.output.ToArray(); } }

        public static IntCodeComputer FromCommaSeparated(string registers)
        {
            return new IntCodeComputer(registers.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                                 .Select(s => Int32.Parse(s)));
        }

        public static IntCodeComputer Empty = new IntCodeComputer(null);

        public static IntCodeComputer Halting = new IntCodeComputer(new int[] { 99 });
    }
}
