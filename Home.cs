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
    public partial class Home : Form
    {
        String UID;
        public Home(String ID)
        {
            
            InitializeComponent();
            label2.Text = getuserName(ID);
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
            
            Form newsalesrecord = new newsalesrecord(UID);
            newsalesrecord.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form s = new salesrecord(UID);
            s.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form i = new Inventoryrecords(UID);
            i.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form r = new Login();
            r.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form y = new NotCollected(UID);
            y.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form i = new Balances(UID);
            i.Show();
            this.Hide();
        }
    }
}
