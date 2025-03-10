using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public double CalculateTotalSales(Type_of_appliances type)
        {
            double total = 0;
            foreach (var sale in Sales)
            {
                var appliance = sale.GetAppliances();
                if (appliance.GetTypeAppliances() == type)
                {
                    total += appliance.GetPrice() * appliance.GetQuantity();
                }
            }
            return total;
        }

        public void PrintSale_History()
        {
            double TotalAmount = 0;
            foreach (var Sale in Sales)
            {
                var appliance = Sale.GetAppliances();

                Console.WriteLine($"День: {Sale.GetDay()}," +
                    $" Наименование: {appliance.GetName()}, " +
                    $"Тип: {appliance.GetTypeAppliances()}, " +
                    $"Цена: {appliance.GetPrice()}, " +
                    $"Количество: {appliance.GetQuantity()}, " +
                    $"Прибыль: {appliance.GetPrice() * appliance.GetQuantity()} ");
                TotalAmount += appliance.GetQuantity() * appliance.GetPrice();
            }
            Console.WriteLine($"Вся прибыль {TotalAmount}");
        }
       
    }
}

