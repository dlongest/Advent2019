using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Runner
{
    public class RunnerSelector
    {
        public Runner Newest()
        {
            var possibleRunners = System.Reflection.Assembly
                                                   .GetExecutingAssembly()
                                                    .GetTypes()
                                                    .Where(t => typeof(Runner).IsAssignableFrom(t) && !t.IsAbstract);

            var newestType = possibleRunners.OrderByDescending(r => r.Name).First();

            return (Runner)Activator.CreateInstance(newestType);
        }
    }
}
