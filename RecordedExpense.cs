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
    public partial class RecordedExpense : Form
    {
        String UID;
        public RecordedExpense(String ID)
        {
            InitializeComponent();
            label2.Text = getuserName(ID);
            UID = ID;

        }
        private String getuserName(String id)
        {
            string que = "SELECT [Name] FROM [supubana].[dbo].[users] WHERE userID=" + id;
            var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
            using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                String returnValue = cmd.ExecuteScalar().ToString();
                return returnValue;
            }

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            
            try
            {
                DateTime dateValue = DateTime.Parse(monthCalendar1.SelectionRange.Start.ToShortDateString());
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.supubanaConnectionString);
                string query = "SELECT  * FROM [supubana].[dbo].[expenses]U WHERE CAST(U.Date AS Date)='" + String.Format("{0:u}", dateValue) + "'";
                MessageBox.Show(query);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form c = new AccountantHome(UID);
            c.Show();
            this.Hide();
        }

        private void RecordedExpense_Load(object sender, EventArgs e)
        {
            try
            {

                SqlConnection connection = new SqlConnection(Properties.Settings.Default.supubanaConnectionString);
                string query = "SELECT  [expenseID],[Amount],[reference],[Date],[userID] FROM [supubana].[dbo].[expenses] WHERE datediff(day, date, GETDATE()) = 0";
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
