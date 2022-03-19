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
    public partial class salesrecord : Form
    {
        String UID;
        public salesrecord(String ID)
        {
            InitializeComponent();
            label17.Text = getuserName(ID);
            UID = ID;
            
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


        private void button1_Click(object sender, EventArgs e)
        {
            String acctype = getAccountType(UID);
            switch (acctype)
            {
                case "Accountant":
                    
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
        private int getofficetotal()
        {
            string que = "SELECT SUM([officeTotal]) FROM [supubana].[dbo].[sales_summary]  WHERE datediff(day, date, GETDATE()) = 0 AND OrderStatus = 'collected'";
            var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
            using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                int returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                return returnValue;
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
        private int getagenttotal()
        {
            string que = "SELECT SUM([agentTotal]) FROM [supubana].[dbo].[sales_summary]  WHERE datediff(day, date, GETDATE()) = 0 AND OrderStatus = 'collected'";
            var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
            using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                int returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                return returnValue;
            }

        }
        private int gettotalsales()
        {
            string que = "SELECT SUM([TotalPaid]) FROM [supubana].[dbo].[sales_summary]  WHERE datediff(day, date, GETDATE()) = 0 AND OrderStatus = 'collected'";
            var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
            using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                int returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                return returnValue;
            }

        }
        private int getcompanytotal()
        {
            string que = "SELECT SUM([companyTotal]) FROM [dbo].[sales_summary]  WHERE datediff(day, date, GETDATE()) = 0 AND OrderStatus = 'collected'";
            var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
            using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                int returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                return returnValue;
            }

        }
       

        private int getexpenseacounttotal()
        {
            string que = "SELECT SUM([officeExpense]) FROM [supubana].[dbo].[sales_summary]  WHERE datediff(day, date, GETDATE()) = 0 AND OrderStatus = 'collected'";
            var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
            using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                int returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                return returnValue;
            }

        }

        private void salesrecord_Load(object sender, EventArgs e)
        {
            try
            {

                SqlConnection connection = new SqlConnection(Properties.Settings.Default.supubanaConnectionString);
                string query = "SELECT [refID],[clientName],[PhoneNo],[Total],[discount],[TotalPaid],[Balance],[agentID],[agentTotal],[officeTotal],[officeExpense],[orderStatus],[userID],[Date] FROM [supubana].[dbo].[sales_summary] WHERE datediff(day, date, GETDATE()) = 0 AND OrderStatus = 'collected'";
                var dataAdapter = new SqlDataAdapter(query, connection);
                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];


                
                string query2 = "SELECT  [agents].[Name],SUM([agentTotal]) AS agent_fee FROM [supubana].[dbo].[sales_summary] Left outer Join  supubana.dbo.agents on sales_summary.agentID=agents.agentID  where datediff(day, date, GETDATE()) = 0 AND OrderStatus='collected' GROUP BY [sales_summary].[agentID],[agents].[Name]";
                var dataAdapter2 = new SqlDataAdapter(query2, connection);
                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                dataGridView2.ReadOnly = true;
                dataGridView2.DataSource = ds2.Tables[0];
                label9.Text = gettotalsales().ToString();
                int fnb = gettotalsales() - (getcompanytotal() + getofficetotal() + getagenttotal() + getexpenseacounttotal());
                label10.Text = fnb.ToString();
                label11.Text = getcompanytotal().ToString();
                label12.Text = getofficetotal().ToString();
                label14.Text = getagenttotal().ToString();
                label16.Text = getexpenseacounttotal().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows) {
                string i= item.Cells[0].Value.ToString();
                Form c = new Cart(i);
                c.ShowDialog();
            }
                
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
