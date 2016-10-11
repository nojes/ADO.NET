using _02_HR.DbLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _02_HR
{
    public partial class Form1 : Form
    {
        BindingSource bs;
        HRContext context;

        public Form1()
        {
            InitializeComponent();
            bs = new BindingSource();
            context = new HRContext();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            context.Employees.Include(emp => emp.EmpPromotions).Load();
            bs.DataSource = context.Employees.Local;
            dgvEmployees.DataSource = bs;
        }
    }
}
