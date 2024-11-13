using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Бытовая_техника
{
    class SaleHistory
    {
       private List <Sale> sales_ = new List <Sale> ();
        public void AddSale(Sale sale)
        {
            sales_.Add (sale);
        }
    }
}

