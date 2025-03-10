using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_LR1
{
    public class Player
    {
        public string Name { get; set; }
        public int Goals { get; set; }

        public Player(string name, int goals)
        {
            Name = name;
            Goals = goals;
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
        }
    }
}
