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
    public partial class Bank : Form
    {
        String UID;
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        public Bank(String ID)
        {
            InitializeComponent();
            label7.Text = getuserName(ID);
            UID = ID;

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
           
            //To where your opendialog box get starting location. My initial directory location is desktop.
            openFileDialog1.InitialDirectory = "C://Desktop";
            //Your opendialog box title name.
            openFileDialog1.Title = "Select image to be upload.";
            //which type image format you want to upload in database. just add them.
            openFileDialog1.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            //FilterIndex property represents the index of the filter currently selected in the file dialog box.
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        label8.Text = path;
                        pictureBox2.Image = new Bitmap(openFileDialog1.FileName);
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload image.");
                }
            }
            catch (Exception ex)
            {
                //it will give if file is already exits..
                MessageBox.Show(ex.Message);
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            String acctype = getAccountType(UID);
            switch (acctype)
            {
                case "Accountant":
                    MessageBox.Show("Don't be lazy my dear! use Excel lol");
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
            float amount = float.Parse(textBox1.Text);
            String Acc = comboBox1.Text;
            String By = textBox2.Text;
          
            byte[] imageArray = System.IO.File.ReadAllBytes(label8.Text);
            string pop = Convert.ToBase64String(imageArray);
            try
            {
                string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);
                if (filename == null)
                {
                    MessageBox.Show("Please select a valid image.");
                }
                else
                {
                    SqlConnection connection = new SqlConnection(Properties.Settings.Default.supubanaConnectionString);
                    SqlDataAdapter cmd = new SqlDataAdapter();
                    string query = "INSERT INTO [dbo].[BankRecords] ([Amount],[ProofOfPayment],[DepositedBy],[userID],[Date],[DepositedToACC]) VALUES (" + amount+",'"+ label8.Text + "','"+By+"',"+ UID + ",CURRENT_TIMESTAMP,'" + Acc+"')";
                    MessageBox.Show(query);
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Successful");
                    textBox1.Clear();
                    textBox2.Clear();
                    label8.Text = "File Path";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Occured!");
            }
        }
    }
}
