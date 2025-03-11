using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyLibrary.Models
{
    public class EmployeeModel
    {
        Employee employee = new Employee();
        List<Employee> emp_;
        public EmployeeModel()
        {
            emp_ = new List<Employee>();

            emp_.Add(new Employee
            {
                Name = "Иван",
                Surname = "Иванов",
                DateBirth = new System.DateTime(1996, 11, 1)
            });

        }
        public string FullInfo() // метод для получения информации о сотруднике
        {
            return $"ФИО: {employee.Name} {employee.Surname} {employee.DateBirth}";
        }

        public List<Employee> Employees ()
        {
            return emp_;
        }

        
    }
}
