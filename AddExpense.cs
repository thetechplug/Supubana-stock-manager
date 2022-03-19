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
    public partial class AddExpense : Form
    {
        String UID;
        public AddExpense(String ID)
        {
            InitializeComponent();
            UID = ID;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form f = new AccountantHome(UID);
            f.Show();
            this.Hide();
        }
        private void updateofficeexpense(String amount)
        {

            try
            {

                SqlConnection connection = new SqlConnection(Properties.Settings.Default.supubanaConnectionString);
                string query = "UPDATE [dbo].[expenseBalance] SET [ExpBalance] = [ExpBalance]-" + amount + " WHERE EID=1";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private int getbalance()
        {
            string que = "SELECT [ExpBalance] FROM [supubana].[dbo].[expenseBalance] where EID=1";
            var sql = string.Format(que, @"Data Source=USER-PC\SUPUBANA; Initial Catalog=supubana; Integrated Security=True");
            using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                int returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                return returnValue;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String Amount = textBox1.Text;
            String Ref = textBox2.Text;

            if (Amount!="" &&Ref!="") {
                try
                {
                    int balance = getbalance();
                    var isnumeric = int.TryParse(Amount, out int a);
                    float rB = balance - a;
                    if (a <= balance)
                    {
                        SqlConnection connection = new SqlConnection(Properties.Settings.Default.supubanaConnectionString);
                        SqlDataAdapter cmd = new SqlDataAdapter();
                        string query = "INSERT INTO [dbo].[expenses]([Amount],[reference],[RBalance],[Date],[userID]) VALUES(" + Amount + ",'" + Ref +"',"+rB+ ",CURRENT_TIMESTAMP," + UID + ")";
                        SqlCommand command = new SqlCommand(query, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        updateofficeexpense(Amount);
                        MessageBox.Show("Recorded Successfully! ");
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                    else {
                        MessageBox.Show("Insufficient Funds!");
                    }
                }

                catch (Exception errore)
                {
                    MessageBox.Show(errore.Message);
                }

            } else {
                MessageBox.Show("Please Fill in all fields");
            }
        }
    }
}
