using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Бытовая_техника
{
    internal class Sale
    {
        private int Day_;
        private Appliances appliances_;

        public  Sale(int Day, Appliances appliances)
        {
            Day_ = Day;
            appliances_ = appliances;
        }
        public Appliances GetAppliances() 
        {
            return appliances_;
        }
        public int GetDay() 
        { 
            return Day_; 
        }
    }
}
