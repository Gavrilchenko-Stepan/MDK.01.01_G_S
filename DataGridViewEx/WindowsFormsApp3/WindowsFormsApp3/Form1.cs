using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private BindingList<Student> Students1 = new BindingList<Student>();
        public Form1()
        {
            InitializeComponent();

            Students1.Add(new Student { name = "SD" });

            dataGridView1.DataSource = Students1;

        }

        public void AddStudent(string name, string surname, int age)
        {
        //   Form1.GetObject();
 
        }
     

 

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
           

            AddEditStudent form = new AddEditStudent();//запросить данные у формы этой и добавить в список
            DialogResult result = form.ShowDialog();
  
            if (result == DialogResult.OK)
            {
           
                Student s = form.GetObject();
                Students1.Add(s);
            }

        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                // Получаем индекс выделенной строки
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;

                // Получаем объект студента из списка
                Student selectedStudent = Students1[selectedRowIndex];

                // Открываем форму редактирования с текущими данными студента
                AddEditStudent Form = new AddEditStudent();
                DialogResult result = Form.ShowDialog();
                Form.SetData(selectedStudent);

                if (result == DialogResult.OK)
                {
                    // Получаем обновленные данные
                    Student updatedStudent = Form.GetObject();

                    // Обновляем данные в списке
                    Students1[selectedRowIndex] = updatedStudent;

                    // Обновляем представление таблицы
                    dataGridView1.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int index111 = dataGridView1.SelectedRows[0].Index;

                    dataGridView1.Rows.RemoveAt(index111);

                }
                DialogResult result = MessageBox.Show(
           "Вы уверены, что хотите удалить эту строку?",
           "Подтверждение удаления",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question
       );

                if (result == DialogResult.Yes)
                {
                    // Удаляем выбранную строку
                    int index = dataGridView1.SelectedRows[0].Index;
                    dataGridView1.Rows.RemoveAt(index);
                }
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              //  MessageBox.Show("Выберите строку для удаления");
            }

        }
    }
}
