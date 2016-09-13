using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _02_WinFormDataSet
{
    public partial class Form1 : Form
    {
        string conString = ConfigurationManager.ConnectionStrings["Region"].ConnectionString;
        SqlConnection connect = new SqlConnection();
        DataSet ds = new DataSet();

        DataTable region = new DataTable("Region");
        DataTable city = new DataTable("City");

        SqlDataAdapter regionAdapter;
        SqlDataAdapter cityAdapter;

        BindingSource regionBinding = new BindingSource();
        BindingSource cityBinding = new BindingSource();

        private void InitSchema()
        {
            connect.ConnectionString = conString;

            regionAdapter = new SqlDataAdapter("SELECT * FROM dbo.Region", connect);
            regionAdapter.FillSchema(region, SchemaType.Mapped);
            SqlCommandBuilder cRegion = new SqlCommandBuilder(regionAdapter);
            regionAdapter.InsertCommand = cRegion.GetInsertCommand();
            regionAdapter.UpdateCommand = cRegion.GetUpdateCommand();
            regionAdapter.DeleteCommand = cRegion.GetDeleteCommand();

            cityAdapter = new SqlDataAdapter("SELECT * FROM dbo.City", connect);
            cityAdapter.FillSchema(city, SchemaType.Mapped);
            SqlCommandBuilder cCity = new SqlCommandBuilder(cityAdapter);
            cityAdapter.InsertCommand = cCity.GetInsertCommand();
            cityAdapter.UpdateCommand = cCity.GetUpdateCommand();
            cityAdapter.DeleteCommand = cCity.GetDeleteCommand();

            ds.Tables.Add(region);
            ds.Tables.Add(city);

            DataRelation relation = new DataRelation("RegionCity",
                ds.Tables[0].Columns["RegionId"],
                ds.Tables[1].Columns["RegionId"]
            );

            ForeignKeyConstraint fKey = new ForeignKeyConstraint("CityForeignKey", 
                ds.Tables[0].Columns["RegionId"],
                ds.Tables[1].Columns["RegionId"]
            );
            fKey.DeleteRule = Rule.Cascade;
            fKey.UpdateRule = Rule.Cascade;
            ds.Tables[1].Constraints.Add(fKey);
            ds.EnforceConstraints = true;
            ds.Relations.Add(relation);


            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["RegionId"] };
            ds.Tables[0].Columns["RegionId"].AutoIncrement = true;
            ds.Tables[0].Columns["RegionId"].Unique = true;
            ds.Tables[0].Columns["RegionId"].AutoIncrementStep = -1;
            ds.Tables[0].Columns["RegionId"].AutoIncrementSeed = -1;

            ds.Tables[1].PrimaryKey = new DataColumn[] { ds.Tables[1].Columns["CityId"] };
            ds.Tables[1].Columns["CityId"].AutoIncrement = true;
            ds.Tables[1].Columns["CityId"].Unique = true;
            ds.Tables[1].Columns["CityId"].AutoIncrementStep = -1;
            ds.Tables[1].Columns["CityId"].AutoIncrementSeed = -1;
        }

        private void FormBinding()
        {
            regionBinding.DataSource = ds;
            regionBinding.DataMember = "Region";
            dgvRegions.DataSource = regionBinding;

            cityBinding.DataSource = regionBinding;
            cityBinding.DataMember = "RegionCity";
            dgvCities.DataSource = cityBinding;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitSchema();
            regionAdapter.Fill(region);
            cityAdapter.Fill(city);
            FormBinding();

            dgvCities.UserDeletingRow += DgvCities_UserDeletingRow;
            regionAdapter.RowUpdated += RegionAdapter_RowUpdated;
        }

        private void RegionAdapter_RowUpdated(object sender, SqlRowUpdatedEventArgs e)
        {
            if (e.Errors == null && e.StatementType == StatementType.Insert)
            {
                SqlCommand cmd = new SqlCommand("SELECT RegionId FROM Region WHERE RegionId = IDENT_CURRENT('dbo.Region')", connect);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    e.Row["RegionId"] = reader["RegionId"];
                }
                reader.Close();

            }
        }

        private void DgvCities_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;
            DialogResult res = MessageBox.Show(" You remove the record?", "Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                foreach (DataGridViewRow r in this.dgvCities.SelectedRows)
                {
                    dgvCities.Rows.RemoveAt(r.Index);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            regionAdapter.Update(ds.Tables[0].Select(null, null, DataViewRowState.Added));
            cityAdapter.Update(ds.Tables[1].Select(null, null, DataViewRowState.Added));

            cityAdapter.Update(ds.Tables[1].Select(null, null, DataViewRowState.Deleted));
        }
    }
}
