using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Бытовая_техника
{
    internal class Sale
    {
        private int check_;
        private Appliances appliances_;
        private double count_;
        private DateTime date_;
        private int quantity_;

        public  Sale(int check, Appliances appliances, double count, int quantity)
        {
            date_ = DateTime.Now;
            appliances_ = appliances;
            count_ = count;
            check_ = check;
            quantity_ = quantity;
        }
    }
}
