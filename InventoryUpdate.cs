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
    
    public partial class InventoryUpdate : Form
    {
        string UID,j, i, ID;int id;
        public InventoryUpdate(String ID)
        {
            InitializeComponent();
            label3.Text = getuserName(ID);
            UID = ID;
        }

        private void button1_Click(object sender, EventArgs e)
        {

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue.ToString() != null)
            {
                // comboBox1.SelectedValue
                j = comboBox1.SelectedValue.ToString();
                bool result = int.TryParse(j, out id);
                i = id.ToString();
                String perUnit = perUnitType(i);
                if (perUnit == "yes")
                {
                    comboBox5.Visible = false;
                    textBox1.Visible = true;
                }
                else if (perUnit == "no")
                {
                    comboBox5.Visible = true;
                    textBox1.Visible = false;

                }
                label4.Text = getunit(i);

                bindsize(i);
                bindcolor(i);
                getthickness(i);
                gettype(i);

                ID = i;


            }
        }
        private String getunit(String ID)
        {
            string que = "SELECT [sizeUnit] FROM [supubana].[dbo].[inventory] WHERE [itemID]=" + ID;
            var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
            using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                String returnValue = cmd.ExecuteScalar().ToString();
                return returnValue;
            }

        }
        private String perUnitType(String ID)
        {
            string que = "SELECT [perUnitType] FROM [supubana].[dbo].[inventory] WHERE [itemID]=" + ID;
            var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
            using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                String returnValue = cmd.ExecuteScalar().ToString();
                return returnValue;
            }

        }
        private void bindcolor(string i)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            {
                try
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter("SELECT [Color] FROM [supubana].[dbo].[inventory_specs] WHERE itemID=" + i, conn))
                    {
                        //Fill the DataTable with records from Table.
                        DataTable dt = new DataTable();
                        sda.Fill(dt);


                        //Assign DataTable as DataSource.
                        comboBox2.DataSource = dt;
                        comboBox2.DisplayMember = "Color";
                        comboBox2.ValueMember = "Color";
                    }
                }
                catch (Exception ex)
                {
                    // write exception info to log or anything else
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }
        private void getthickness(string i)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            {
                try
                {



                    using (SqlDataAdapter sda = new SqlDataAdapter("SELECT [Thickness] FROM [supubana].[dbo].[inventory_specs] WHERE itemID=" + i, conn))
                    {
                        //Fill the DataTable with records from Table.
                        DataTable dt = new DataTable();
                        sda.Fill(dt);


                        //Assign DataTable as DataSource.
                        comboBox3.DataSource = dt;
                        comboBox3.DisplayMember = "Thickness";
                        comboBox3.ValueMember = "Thickness";
                    }


                }
                catch (Exception ex)
                {
                    // write exception info to log or anything else
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void bindsize(string i)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            {
                try
                {

                    using (SqlDataAdapter sda = new SqlDataAdapter("SELECT [Size] FROM [supubana].[dbo].[inventory_specs] WHERE itemID=" + i, conn))
                    {
                        //Fill the DataTable with records from Table.
                        DataTable dt = new DataTable();
                        sda.Fill(dt);


                        //Assign DataTable as DataSource.
                        comboBox5.DataSource = dt;
                        comboBox5.DisplayMember = "Size";
                        comboBox5.ValueMember = "Size";
                    }
                }
                catch (Exception ex)
                {
                    // write exception info to log or anything else
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void gettype(string i)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            {
                try
                {



                    using (SqlDataAdapter sda = new SqlDataAdapter("SELECT [Type] FROM [supubana].[dbo].[inventory_specs] WHERE itemID=" + i, conn))
                    {
                        //Fill the DataTable with records from Table.
                        DataTable dt = new DataTable();
                        sda.Fill(dt);


                        //Assign DataTable as DataSource.
                        comboBox6.DataSource = dt;
                        comboBox6.DisplayMember = "Type";
                        comboBox6.ValueMember = "Type";
                    }


                }
                catch (Exception ex)
                {
                    // write exception info to log or anything else
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
