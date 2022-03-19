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
    public partial class Addagents : Form
    {
        public Addagents()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String name = textBox1.Text;
            string phone = textBox2.Text;
            String idtype = comboBox1.Text;
            string idnum = textBox3.Text;
            String rAddress = textBox4.Text;
            if (name == "" && phone == "" && idtype=="" && idnum=="" && rAddress=="")
            {
                MessageBox.Show("Please fill  in all fields");
            }
            else {
                try
                {
                    
                    SqlConnection connection = new SqlConnection(Properties.Settings.Default.supubanaConnectionString);
                    SqlDataAdapter cmd = new SqlDataAdapter();
                    string query = "INSERT INTO [dbo].[agents] ([Name],[EmailAddress],[IDtype],[IDnum],[ResidentialAddress],[AccStatus]) VALUES ('" + name+ "','" + phone + "','"+idtype+"','"+idnum+"','"+rAddress+"','active')";
                    MessageBox.Show(query);
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Agent Added");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();

                }

                catch (Exception errore)
                {
                    MessageBox.Show(errore.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form m = new adminhome();
            m.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
