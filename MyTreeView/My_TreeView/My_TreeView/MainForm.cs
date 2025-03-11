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
            бухгалтерия.AddChildNode("IT-отдел");
            бухгалтерия.AddChildNode("Касса");
            бухгалтерия.AddChildNode("Руководство");

            var кадры = collegeNode.AddChildNode("Отдел кадров");
            кадры.AddChildNode("Заведующий канцелярией");
            кадры.AddChildNode("Инспектор по кадрам");
            кадры.AddChildNode("Архивариус");
            кадры.AddChildNode("Начальник отдела");
            


            FillTreeNodeCollection(treeData_, treeView1.Nodes);

            treeView1.ExpandAll();
            
        }

        

        private void Table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            // Найдите сотрудника в модели по имени
            Employee foundEmployee = employeeModel.Employees().FirstOrDefault(emp => emp.Name == employee1.Name);

            if (foundEmployee != null)
            {
                AddEmployeeToTable(foundEmployee);
            }
            else
            {
                MessageBox.Show("Сотрудник не найден.");
            }
        }
       
    }
}
