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

    struct Team
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public List<Footballer> Footballers { get; set; }
        public Team(string name, int points, List<Footballer> footballers)
        {
            Name=name;
            Points = points;
            Footballers = footballers;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
