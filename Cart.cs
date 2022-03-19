using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supubana_stock_manager
{
    public partial class Cart : Form
    {
        String id;
        public Cart(String itemID)
        {
            InitializeComponent();
          id = itemID;
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            try
            {

                SqlConnection connection = new SqlConnection(Properties.Settings.Default.supubanaConnectionString);
                string query = "SELECT  [refID],[itemID],[Size],[SizeUnit],[Color],[Thickness],[unitPrice],[Quantity],[Subtotal],[Type] FROM [supubana].[dbo].[cart] where [refID]="+id;
                var dataAdapter = new SqlDataAdapter(query, connection);
                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];



              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
