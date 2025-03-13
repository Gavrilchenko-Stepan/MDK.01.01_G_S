using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using MyLibrary;
using MyLibrary.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace My_TreeView
{
    public partial class MainForm : Form
    {
        private List<College> treeData_;
        private EmployeeModel employeeModel;
        Employee employee1 = new Employee();
        public MainForm()
        {
            InitializeComponent();
            treeData_ = new List<College>();
            employeeModel = new EmployeeModel();
        }

        static private void FillTreeNodeCollection(List<College> sourceData, //данные источника - модели
                                                   TreeNodeCollection targetData) // данные приемника - представления
        {
            foreach (var node in sourceData)
            {
                var treeNode = new TreeNode(node.Name); // объект узла в представлении
                treeNode.Tag = node; // сохранить ссылку на объект college
                targetData.Add(treeNode); // добавили узел в дерево

                if (node.Children != null && node.Children.Count > 0)
                {
                    FillTreeNodeCollection(node.Children, treeNode.Nodes); //переносим дочерние элементы узла модели в дочерние элементы узла представления
                }
            }
        }

        private void AddEmployeeToTable(Employee employee)
        {
            // Предполагаем, что у вашего DataGridView три столбца: Имя, Фамилия и Дата рождения
            var row = new DataGridViewRow();
            row.CreateCells(Table, employee.Name, employee.Surname, employee.DateBirth.ToShortDateString());
              
            Table.Rows.Add(row);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            treeData_.Add(new College("ФГБОУ Колледж Росрезерва"));
            
            var collegeNode = treeData_[0];
            var бухгалтерия = collegeNode.AddChildNode("Бухгалтерия");
            var It = бухгалтерия.AddChildNode("IT-отдел");
            It.Employees.Add(new Employee { Name = "Иван", Surname = "Иванов", DateBirth = new DateTime(1996, 11, 1) });
            It.Employees.Add(new Employee { Name = "Петр", Surname = "Петров", DateBirth = new DateTime(1985, 05, 15) });
            var Kassa = бухгалтерия.AddChildNode("Касса");
            Kassa.Employees.Add(new Employee { Name = "Андрей", Surname = "Будов", DateBirth = new DateTime(2000, 12, 20) });
            Kassa.Employees.Add(new Employee { Name = "Ирина", Surname = "Крутая", DateBirth = new DateTime(1990, 09, 6) });
            var Admin = бухгалтерия.AddChildNode("Руководство");
            Admin.Employees.Add(new Employee { Name = "Наталья", Surname = "Морская", DateBirth = new DateTime(1994, 12, 12) });
            Admin.Employees.Add(new Employee { Name = "Александр", Surname = "Бубка", DateBirth = new DateTime(1997, 05, 9) });
            var кадры = collegeNode.AddChildNode("Отдел кадров");
            var Chancellery = кадры.AddChildNode("Заведующий канцелярией");
            Chancellery.Employees.Add(new Employee { Name = "Мария", Surname = "Соколова", DateBirth = new DateTime(1972, 01, 22) });
            var Inspector = кадры.AddChildNode("Инспектор по кадрам");
            Inspector.Employees.Add(new Employee { Name = "Роберт", Surname = "Патрик", DateBirth = new DateTime(1989, 10, 26) });
            var Archive = кадры.AddChildNode("Архивариус");
            Archive.Employees.Add(new Employee { Name = "Сара", Surname = "Коннор", DateBirth = new DateTime(1965, 11, 2) });

            Table.Columns.AddRange(
            new DataGridViewTextBoxColumn { HeaderText = "Имя", DataPropertyName = "Name" },
              new DataGridViewTextBoxColumn { HeaderText = "Фамилия", DataPropertyName = "Surname" },
              new DataGridViewTextBoxColumn { HeaderText = "Дата рождения", DataPropertyName = "DateBirth" });


            FillTreeNodeCollection(treeData_, treeView1.Nodes);

            treeView1.ExpandAll();
            
        }

        

        private void Table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            var selectedNode = e.Node;
            if (selectedNode != null && selectedNode.Tag is College college)
            {
                // Очищаем таблицу перед добавлением новых записей
                Table.Rows.Clear();

                foreach (var employee in college.Employees)
                {
                    AddEmployeeToTable(employee);
                }
            }
        }
       
    }
}
