using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Бытовая_техника
{
    public class Appliances
    {
       
        private double price_;
        private string name_;

        public void SetNames(string n)
        {
            name_ = n;
        }
        public string GetName()
        {
            return name_;
        }
        public void SetPrice(double p)
        {
            price_ = p;
        }
        public Appliances(double Price, string Name)
        {
            price_ = Price;
            name_ = Name;
        }
    }
}
