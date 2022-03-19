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
    public partial class Login : Form
    {

        private const string SAMPLE_KEY = "gCjK+DZ/GCYbKIGiAt1qCA==";
        private const string SAMPLE_IV = "47l5QsSe1POo31adQ/u7nQ==";
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string LoginName, Password;
            LoginName = textBox1.Text;
            Password = textBox2.Text;
            if (LoginName != "" && Password != "")
            {
                try
                {
                    Aes aes = new Aes(SAMPLE_KEY, SAMPLE_IV);
                    String O = Password;
                    byte[] result = aes.EncryptToByte(O);
                    var passwrd = System.Text.Encoding.Default.GetString(result);
                    string query = "SELECT COUNT(*) FROM [dbo].[users] WHERE users.userID=" + LoginName + " AND users.Password='" + passwrd + "'";
                    var sql = string.Format(query, Properties.Settings.Default.supubanaConnectionString);
                    using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        con.Open();
                        int r = Convert.ToInt32(cmd.ExecuteScalar());
                        if (r == 1)
                        {
                            
                            String userid = textBox1.Text;
                            String userAcc=getAccountType(LoginName, passwrd);

                            switch (userAcc)
                            {
                                case "Accountant":
                                    Form h = new AccountantHome(userid);
                                    h.Show();
                                    this.Hide();
                                    break;
                                case "Cashier":
                                    Form C = new Home(userid);
                                    C.Show();
                                    this.Hide();
                                    break;
                                case "Admin":
                                    Form m = new managerhome(userid);
                                    m.Show();
                                    this.Hide();
                                    break;
                                default:
                                    MessageBox.Show("Under Maintenance!");
                                    break;
                            }





                        }
                        else
                        {
                            label4.Visible = true;
                            textBox1.Focus();
                        }

                    }


                }
                catch(Exception errore)
                {
                    MessageBox.Show(errore.Message);
                }
            }
            else
            {

                MessageBox.Show("Please fill in all fields!");
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {

            Form admin = new admin();
            admin.Show();
            this.Hide();
        }

       
        private String getAccountType(String LoginName, String passwrd)
        {
            string que = "SELECT AccountType FROM [dbo].[users] WHERE users.userID=" + LoginName + " AND users.Password='" + passwrd + "'";
            var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
            using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                String returnValue = cmd.ExecuteScalar().ToString();
                return returnValue;
            }

        }
    }
    }

