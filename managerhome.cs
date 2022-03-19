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
    public partial class managerhome : Form
    {
        String I;
        public managerhome(String ID)
        {
            if (ID != "")
            {
                InitializeComponent();
                
                label2.Text = getuserName(ID);
                I = ID;


            }
            else
            {
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form addinventory = new addinventory(I);
            addinventory.Show();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form sales = new newsalesrecord(I);
            sales.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form s = new salesrecord(I);
            s.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form i = new Inventoryrecords(I);
            i.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form h = new Login();
            h.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form y = new NotCollected(I);
            y.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form i = new Balances(I);
            i.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form lp = new InventoryUpdate(I);
            lp.Show();
            this.Hide();
        }
    }
}
