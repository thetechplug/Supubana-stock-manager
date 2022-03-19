using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Supubana_stock_manager
{
    public partial class Inventoryrecords : Form
    {
        String UID;
        public Inventoryrecords(String ID)
        {
            InitializeComponent();
            label4.Text = getuserName(ID);
            UID = ID;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String acctype = getAccountType(UID);
            switch (acctype)
            {
                case "Accountant":
                    MessageBox.Show("Don't be lazy my dear! use Excel lol");
                    Form h = new AccountantHome(UID);
                    h.Show();
                    this.Hide();
                    break;
                case "Cashier":
                    Form C = new Home(UID);
                    C.Show();
                    this.Hide();
                    break;
                case "Admin":
                    Form m = new managerhome(UID);
                    m.Show();
                    this.Hide();
                    break;
                default:
                    MessageBox.Show("Under Maintenance!");
                    break;
            }
        }
        private String getAccountType(String LoginName)
        {
            string que = "SELECT AccountType FROM [dbo].[users] WHERE users.userID=" + LoginName;
            var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
            using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                String returnValue = cmd.ExecuteScalar().ToString();
                return returnValue;
            }

        }
        private String getuserName(String id)
        {
            string que = "SELECT Name FROM [dbo].[users] WHERE users.userID=" + id;
            var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
            using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                String returnValue = cmd.ExecuteScalar().ToString();
                return returnValue;
            }

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Inventoryrecords_Load(object sender, EventArgs e)
        {
            try
            {

                
                string query = "SELECT inventory.itemID,inventory.itemName,inventory_specs.Size,inventory.SizeUnit,inventory_specs.Color,inventory_specs.thickness,inventory_specs.quantity,inventory_specs.OOStockAlert,inventory.location FROM [supubana].[dbo].[inventory] RIGHT OUTER JOIN [supubana].[dbo].[inventory_specs] ON [supubana].[dbo].[inventory].[itemID]=[supubana].[dbo].[inventory_specs].[itemID]";
                var dataAdapter = new SqlDataAdapter(query, Properties.Settings.Default.supubanaConnectionString);
                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];



                string query2 = "SELECT inventory.itemID,inventory.itemName,inventory.location,inventory_specs.quantity FROM [supubana].[dbo].[inventory] RIGHT OUTER JOIN [supubana].[dbo].[inventory_specs] ON [supubana].[dbo].[inventory].[itemID]=[supubana].[dbo].[inventory_specs].[itemID] WHERE inventory_specs.quantity<inventory_specs.OOStockAlert";
                var dataAdapter2 = new SqlDataAdapter(query2, Properties.Settings.Default.supubanaConnectionString);
                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);


                dataGridView2.ReadOnly = true;
                dataGridView2.DataSource = ds2.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
