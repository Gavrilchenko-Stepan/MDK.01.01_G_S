using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Бытовая_техника
{
    class SaleHistory
    {
       private List <Sale> Sales;

        public SaleHistory()
        {
            Sales = new List<Sale>();
        }
        public void Addsale(Sale Sale)
        {
            Sales.Add(Sale);
        }

        public void Sale_History()
        {
            double TotalAmount = 0;
            foreach (var Sale in Sales)
            {
                var appliance = Sale.GetAppliances();

                Console.WriteLine($"День {Sale.GetDay()}," +
                    $" Наименование бытовой техники {appliance.GetName()}, " +
                    $"Тип бытовой техники {appliance.GetType()}, " +
                    $"Цена {appliance.GetPrice()}," +
                    $"Количество {appliance.GetQuantity()}," +
                    $"Прибыль {appliance.GetPrice() * appliance.GetQuantity()}");
            }
            if (TotalAmount != 0) 
            {
                Console.WriteLine($"Вся прибыль {TotalAmount}");
            }
            if (TotalAmount == 0)
            {
                Console.WriteLine("Не совершалось продаж в данный день");
            }
            Console.WriteLine($"Вся прибыль {TotalAmount}");
        }
       
    }
}

