using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counters
{
    class Program
    {
        static void Main(string[] args)
        {
            new PerformanceCounterCategoriCreator().Create();
            new PerformanceCounterIncrementer().Start();

            Console.Read();
        }
    }
}
