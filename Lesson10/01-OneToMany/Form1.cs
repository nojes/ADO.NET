using _01_OneToMany.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace _01_OneToMany
{
    public partial class Form1 : Form
    {
        CustomerContext context = new CustomerContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // lazy load
            context.Customers.Include(c => c.Orders).Load();

            var customers = context.Customers.Local
                .Select(customer => new { customer.CustomerId, FullName = customer.FirstName + " " + customer.LastName })
                .ToList();

            //var orders = context.Orders;
            //labelOrder.Text = orders.ToString();

            cbCustomer.DisplayMember = "FullName";
            cbCustomer.ValueMember = "CustomerId";
            cbCustomer.DataSource = customers;

            labelInfo.Text = cbCustomer.SelectedValue.ToString();
            dgvOrders.DataSource = getOrders();
        }

        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelInfo.Text = cbCustomer.SelectedValue.ToString();
            dgvOrders.DataSource = getOrders();
            dgvOrders.Columns["OrderId"].Visible = false;
            dgvOrders.Columns["CustomerId"].Visible = false;
            dgvOrders.Columns["Customer"].Visible = false;
        }

        private List<Order> getOrders()
        {
            int customerId = Convert.ToInt32(cbCustomer.SelectedValue);
            return context.Orders
               .Where(order => order.CustomerId == customerId)
               .ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            context.SaveChanges();
        }

        //private List<Customer> getCustomers()
        //{
        //    return context.Customers
        //        .Select(customer => new { customer.CustomerId, FullName = customer.FirstName + " " + customer.LastName })
        //        .ToList();
        //}
    }
}
