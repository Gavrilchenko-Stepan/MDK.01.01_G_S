using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Бытовая_техника
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SaleHistory history = new SaleHistory();

            history.Addsale(new Sale(1, new Appliances("Холодильник Haier", 25000.00, 5, Type_of_appliances.fridges)));
            history.Addsale(new Sale(2, new Appliances("Микроволновка BORK", 69000.00, 3, Type_of_appliances.microwaves)));
            history.Addsale(new Sale(3, new Appliances("Пылесос Tefal", 8999.00, 10, Type_of_appliances.vacuumcleaners)));
            history.Addsale(new Sale(1, new Appliances("Стиральная машина DEKO", 28904.00, 2, Type_of_appliances.washingmacchnes)));
            history.Addsale(new Sale(6, new Appliances("Стиральная машина Haier", 34999.00, 8, Type_of_appliances.washingmacchnes)));
            history.Addsale(new Sale(6, new Appliances("Холодильник Indesit", 30990.00, 5, Type_of_appliances.fridges)));
            history.Addsale(new Sale(7, new Appliances("Микроволновка Samsung", 11503.00, 15, Type_of_appliances.microwaves)));
            history.Addsale(new Sale(2, new Appliances("Пылесос Phillips", 12299.00, 6, Type_of_appliances.vacuumcleaners)));

            history.PrintSale_History();

            // Запрос на ввод типа бытовой техники от пользователя
            Console.WriteLine("Выберите тип бытовой техники для расчета общей стоимости:");
            Console.WriteLine("0 - Холодильники");
            Console.WriteLine("1 - Микроволновки");
            Console.WriteLine("2 - Пылесосы");
            Console.WriteLine("3 - Стиральные машины");

            // Чтение выбора пользователя
            if (int.TryParse(Console.ReadLine(), out int Choice) && Enum.IsDefined(typeof(Type_of_appliances), Choice))
            {
                Type_of_appliances selectedType = (Type_of_appliances)Choice;

                // Расчёт стоимости по выбранному типу
                double totalSales = history.CalculateTotalSales(selectedType);

                Console.WriteLine($"Общая стоимость проданных товаров типа {selectedType}: {totalSales} руб.");
            }
            else
            {
                Console.WriteLine("Неверный выбор. Пожалуйста, выберите номер от 0 до 3.");
            }

            Console.ReadKey();
        }
        
    }
}
