using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class AddEditStudent: Form
    {
        public AddEditStudent()
        {
            InitializeComponent();
        }

        private void AddEditStudent_Load(object sender, EventArgs e)
        {

        }
        public Student GetObject()
        {
            Student student22 = new Student();
            student22.name = textBox1.Text;
            student22.surname = textBox2.Text;
            student22.age = (int)numericUpDown1.Value;
            return student22;
        }
        public void SetData(Student student)
        {
            textBox1.Text = student.name;
            textBox2.Text = student.surname;
            numericUpDown1.Value = student.age;
        }


        private void ButtonCancel_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
