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
    public partial class NotCollected : Form
    {
        String UID;
        public NotCollected(String ID)
        {
            InitializeComponent();
            label3.Text = getuserName(ID);
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

        private void NotCollected_Load(object sender, EventArgs e)
        {
             try
            {

              

                SqlConnection connection = new SqlConnection(Properties.Settings.Default.supubanaConnectionString);
                string query = "SELECT [clientName],[PhoneNo],[Total],[discount],[TotalPaid],[Balance],[agentID],[agentTotal],[officeTotal],[officeExpense],[orderStatus],[userID],[Date],[companyTotal] FROM [supubana].[dbo].[sales_summary] WHERE [OrderStatus]='not collected'";
                var dataAdapter = new SqlDataAdapter(query, connection);
                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];



            }
            catch (Exception ex)
            {
                MessageBox.Show("No Records!");
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.Text != "")
            {

                try
                {
                    int refID = Int32.Parse(textBox1.Text);
                    int status = Int32.Parse(comboBox1.Text);
                    SqlConnection connection = new SqlConnection(Properties.Settings.Default.supubanaConnectionString);
                    SqlDataAdapter cmd = new SqlDataAdapter();
                    string query = "UPDATE [dbo].[sales_summary] SET [orderStatus] = " + status + " and Date=CURRENT_TIMESTAMP WHERE [refID]=" + refID;
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully!");
                    textBox1.Clear();
                    /*
                    string query2 = "";
                    var dataAdapter2 = new SqlDataAdapter(query2, Properties.Settings.Default.supubanaConnectionString);
                    var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                    var ds2 = new DataSet();
                    dataAdapter2.Fill(ds2);
                    */
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }




            }
            else
            {
                MessageBox.Show("Please provide all information required!");
            }
        }
    }
    }

