using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supubana_stock_manager
{
    public partial class addinventory : Form
    {
        
        String UID;
        int agent_fee;
        int office_fee, offExp;
        string perUnit="no";
        int count = 0;


        public addinventory(String ID)
        {
            InitializeComponent();
            label15.Text = getuserName(ID);
            UID = ID;
            
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form manager = new managerhome(UID);
            manager.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != ""  && textBox8.Text != "" && textBox9.Text != "" && textBox10.Text != "" && textBox11.Text != "" )
            {
                string type,color ;
                float size, thickness, cmpnyFee, cntrPrice, AgntPrce, qty,oos;
                type = textBox3.Text;
                var s = float.TryParse(textBox4.Text, out size);
                color = textBox5.Text;
                var t= float.TryParse(textBox6.Text, out thickness);
                var c= float.TryParse(textBox7.Text, out cmpnyFee);
                var cp= float.TryParse(textBox8.Text, out cntrPrice);
                var ap= float.TryParse(textBox9.Text, out AgntPrce);
                var q= float.TryParse(textBox10.Text, out qty);
                var o= float.TryParse(textBox11.Text, out oos);



                this.dataGridView1.Rows.Add(type,size,color,thickness,cmpnyFee,cntrPrice,AgntPrce,qty,oos);
               
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
                textBox11.Clear();
                count++;

            }
            else {
                MessageBox.Show("Incomplete!");
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
           
            }

        private void label13_Click(object sender, EventArgs e)
        {

        }
        private void insertsize(String type,float size,String color,float thickness,float comFee,float counterPrice,float Aprice,float qty,float oos) { 

            try
            {

                SqlConnection connection = new SqlConnection(@"Data Source=USER-PC\SUPUBANA; Initial Catalog=supubana; Integrated Security=True");
                string query = "INSERT INTO [dbo].[inventory_specs]([itemID],[Type],[Size],[Color],[Thickness],[company_fee],[counterPrice],[agentPrice],[quantity],[OOStockAlert]) VALUES(IDENT_CURRENT ('inventory'),'" + type + "','" + size + "','" + color + "'," + thickness + "," + comFee + "," + counterPrice + "," + Aprice + "," + qty + "," + oos + ")";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            if ( textBox1.Text != "" && comboBox1.Text != ""&& comboBox2.Text!="" && (radioButton1.Checked==true||radioButton2.Checked==true)&& count!=0)
            {
                String name,unit, location;
                
                

                
                name = textBox1.Text;
                location = comboBox1.Text;
                unit = comboBox2.Text;


                if (radioButton1.Checked == true)
                {
                    agent_fee = 2;
                    office_fee = 1;
                    offExp = 0;
                    
                }
                if (radioButton2.Checked == true)
                {
                    agent_fee = 0;
                    office_fee = 0;
                    offExp = 0;
                }

                try
                {
                    //Aes aes = new Aes();
                    //string encrypted = aes.Encrypt(password);
                    SqlConnection connection = new SqlConnection(@"Data Source=USER-PC\SUPUBANA; Initial Catalog=supubana; Integrated Security=True");
                    SqlDataAdapter cmd = new SqlDataAdapter();
                    // DateTime dt1 = DateTime.Now;
                    string query = "INSERT INTO [dbo].[inventory]([itemName],[Location],[Date],[agent_fee],[office_fee],[expense_fee],[userID],[perUnitType],[SizeUnit]) VALUES('" +
                        name +"','"+location + "',CURRENT_TIMESTAMP,'" + agent_fee + "',"+office_fee+",'" + offExp + "',"+UID+",'" + perUnit + "','" + unit + "');DECLARE @ID int;set @ID = SCOPE_IDENTITY();";
                    
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    foreach (DataGridViewRow Datarow in dataGridView1.Rows)
                    {
                        if (Datarow.Cells[0].Value != null && Datarow.Cells[1].Value != null)
                        {
                            String type= Datarow.Cells[0].Value.ToString();
                            float size = float.Parse(Datarow.Cells[1].Value.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                            String color = Datarow.Cells[2].Value.ToString();
                            float th= float.Parse(Datarow.Cells[3].Value.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                            float cmf= float.Parse(Datarow.Cells[4].Value.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                            float Cprc= float.Parse(Datarow.Cells[5].Value.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                            float AP= float.Parse(Datarow.Cells[6].Value.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                            float qt= float.Parse(Datarow.Cells[7].Value.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                            float os= float.Parse(Datarow.Cells[8].Value.ToString(), CultureInfo.InvariantCulture.NumberFormat);

                            insertsize(type,size, color,th,cmf,Cprc,AP,qt, os);

                        }

                    }
                    MessageBox.Show("Inventory Added ");
                    textBox1.Clear();
                    //textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    textBox8.Clear();
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();
                    



                }

                catch (Exception errore)
                {
                    MessageBox.Show(errore.Message);
                }
            }
            else {
                MessageBox.Show("Please fill in all fields");
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                
                dataGridView1.Rows.RemoveAt(item.Index);
            }
        }

        private void addinventory_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) {
                perUnit = "yes";
            }
            else if (checkBox1.Checked==false) {
                perUnit = "no";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
        
        }

        
    

