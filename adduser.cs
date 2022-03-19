using AesExample;
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
    public partial class adduser : Form
    {
        private string ORIGINAL = "";
        private const string SAMPLE_KEY = "gCjK+DZ/GCYbKIGiAt1qCA==";
        private const string SAMPLE_IV = "47l5QsSe1POo31adQ/u7nQ==";
        public adduser()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form admin = new adminhome();
            admin.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String name, email, account;
            name = textBox1.Text;
            email = textBox2.Text;
            account = comboBox1.Text;
            Aes aes = new Aes(SAMPLE_KEY, SAMPLE_IV);
            ORIGINAL = textBox3.Text;
            byte[] result= aes.EncryptToByte(ORIGINAL);
            var password = System.Text.Encoding.Default.GetString(result);
            if (name == "" && email == "" && password == "" && account == "")
            {
                MessageBox.Show("Please fill in all fields!");

            }
            else
            {
               
                
                try
                {
                    //Aes aes = new Aes();
                    //string encrypted = aes.Encrypt(password);
                    SqlConnection connection = new SqlConnection(Properties.Settings.Default.supubanaConnectionString);
                    SqlDataAdapter cmd = new SqlDataAdapter();
                    string query= "INSERT INTO dbo.users(EmailAddress,Name,Password,AccountType,AccStatus) VALUES ('" + email + "','" + name + "','" + password + "','" + account + "','active')";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("User Added ");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }

                catch (Exception errore)
                {
                    MessageBox.Show(errore.Message);
                }
            }
        }
    }
}
