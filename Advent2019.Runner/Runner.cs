using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Runner
{
    public abstract class Runner
    {
        public void Run()
        {
            PrintHeader();
            DoRun();
            PrintFooter();
        }


        protected abstract void DoRun();

        protected virtual void PrintHeader()
        {
            var name = this.GetType().Name;

            Console.WriteLine(name);
            Console.WriteLine("------------------------------");
        }

        protected virtual void PrintFooter()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
