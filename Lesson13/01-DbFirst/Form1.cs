using _01_DbFirst.BusinessLayer;
using _01_DbFirst.DbLayer;
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

namespace _01_DbFirst
{
    public partial class Form1 : Form
    {

        ShopAdoEntities context = new ShopAdoEntities();

        BindingSource bsGood = new BindingSource();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //bsGood.DataSource = context.Goods.Local;

            context.Goods.Load();
            context.Manufacturers.Load();
            context.Categories.Load();
            Init();
        }

        private void Init()
        {
            List<GoodExt> goods = ManagerExt.GetGoods(context);
            bsGood.DataSource = goods;

            dgvGoods.DataSource = bsGood;
        }

        private void GoodEdit()
        {
            GoodExt goodExt = bsGood.Current as GoodExt;
            Good good = context.Goods.Local
                .Where(g => g.GoodId == goodExt.GoodId)
                .FirstOrDefault();

            FrmEditGood form = new FrmEditGood();
            form.inputGoodName.Text = good.GoodName;
            form.inputCount.Text = good.GoodCount.ToString();
            form.inputPrice.Text = good.Price.ToString();

            form.cbManufacturers.DisplayMember = "ManufacturerName";
            form.cbManufacturers.ValueMember = "ManufacturerId";
            form.cbManufacturers.DataSource = context.Manufacturers.Local;
            form.cbManufacturers.SelectedValue = good.ManufacturerId;

            form.cbCategories.DisplayMember = "CategoryName";
            form.cbCategories.ValueMember = "CategoryId";
            form.cbCategories.DataSource = context.Categories.Local;
            form.cbCategories.SelectedValue = good.CategoryId;

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                good.GoodName = form.inputGoodName.Text;
                good.GoodCount = Convert.ToDecimal(form.inputCount.Text);
                good.Price = Convert.ToDecimal(form.inputPrice.Text);

                context.SaveChanges();

                Init();
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.GoodEdit();
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.GoodEdit();
        }
    }
}
