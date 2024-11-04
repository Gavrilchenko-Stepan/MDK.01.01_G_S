using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_LR_1
{
    struct Footballer
    {
        public string Name { get; set; }
        public int Goals { get; set; }

        public Footballer(string name, int goals)
        {
            Name = name;
            Goals = goals;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
