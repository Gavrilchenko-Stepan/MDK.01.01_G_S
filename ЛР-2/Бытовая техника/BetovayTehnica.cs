using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Бытовая_техника
{
    public class BetovayTehnica
    {
       
        private double price;
        private string name;

        public void Setnames(string n)
        {
            name = n;
        }
        public string Getvaluestring()
        {
            return name;
        }
        public void Setprise(double p)
        {
            price = p;
        }
        public BetovayTehnica(double PR, string NM)
        {
            price = PR;
            name = NM;
        }
    }
}
