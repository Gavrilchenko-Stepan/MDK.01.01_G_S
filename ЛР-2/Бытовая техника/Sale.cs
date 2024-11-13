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

        public Sale(int check)
        {
            check_ = check;
        }

        public Sale SetSale(Appliances appliances, double count)
        {
            date_ = DateTime.Now;
            appliances_ = appliances;
            count_ = count;

            return this;
        }
    }
}
