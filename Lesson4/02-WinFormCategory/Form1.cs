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

namespace _02_WinFormCategory
{
    public partial class Form1 : Form
    {
        string conString = ConfigurationManager.ConnectionStrings["shop"].ConnectionString;
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataTable category;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(conString);
            adapter = new SqlDataAdapter("SELECT [CategoryId], [CategoryName] FROM dbo.Category", connection);
            adapter.RowUpdated += Adapter_RowUpdated;
            category = new DataTable("Category");

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            adapter.DeleteCommand = builder.GetDeleteCommand();
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.UpdateCommand = builder.GetUpdateCommand(true);

            adapter.Fill(category);

            dgvCategory.DataSource = category;
        }

        private void Adapter_RowUpdated(object sender, SqlRowUpdatedEventArgs e)
        {
            if (e.Errors == null && e.StatementType == StatementType.Insert)
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryId FROM dbo.Category WHERE CategoryId = IDENT_CURRENT('dbo.Category')", connection);

                if (connection.State != ConnectionState.Open) { 
                    connection.Open();
                }   
                int count = (int)cmd.ExecuteScalar();
                e.Row["CategoryId"] = count;

            }
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            adapter.Update(category);
            MessageBox.Show("Updated!", "Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
