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
        private int quantity_;
        private Type_of_appliances type_;

        public double GetPrice()
        {
            return price_;
        }
        public string GetName()
        {
            return name_;
        }
        public int GetQuantity()
        {
            return quantity_;
        }

        public Type_of_appliances GetTypeAppliances()
        {
            return type_;
        }

        public Appliances(string Name, double Price, int quantity_, Type_of_appliances type_)
        {
            price_ = Price;
            name_ = Name;
            this.quantity_ = quantity_;
            this.type_ = type_;
        }
    }
}
