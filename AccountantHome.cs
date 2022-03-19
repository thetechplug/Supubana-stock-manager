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
    public partial class AccountantHome : Form
    {
        String UID;
        public AccountantHome(String ID)
        {
            InitializeComponent();
            label2.Text = getuserName(ID);
            UID = ID;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form g = new Bank(UID);
            g.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form s = new salesrecord(UID);
            s.Show();
            this.Hide();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Form p = new Inventoryrecords(UID);
            p.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form h = new Login();
            h.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form m = new AddExpense(UID);
            m.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form m = new RecordedExpense(UID);
            m.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form h = new DepositsRecords(UID);
            h.Show();
            this.Hide();
        }
    }
}
