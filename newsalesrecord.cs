using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;

using System.Windows.Forms;
using Path = System.IO.Path;
namespace Supubana_stock_manager
{
    public partial class newsalesrecord : Form
    {
        float total,agentPrice, agenttotal, counterPrice, finaltotal, counterTotal = 0;
        float q, agent_fee, company_fee = 0, office_fee = 0, office_expense = 0, offexp, company, office;
        int refi;
        int agentID;
        string ID, j,sizee,siz, UID = ""; string i; int id;
        DataGridView table;
        

        public newsalesrecord(String ID)
        {
            InitializeComponent();
            label16.Text = getuserName(ID);
            UID = ID;


        }

        private void button1_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Button clicked");
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void newsalesrecord_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'supubanaDataSet2.inventory' table. You can move, or remove it, as needed.
            this.inventoryTableAdapter1.Fill(this.supubanaDataSet2.inventory);
            // TODO: This line of code loads data into the 'supubanaDataSet1.agents' table. You can move, or remove it, as needed.
            this.agentsTableAdapter.Fill(this.supubanaDataSet1.agents);
            // TODO: This line of code loads data into the 'supubanaDataSet.inventory' table. You can move, or remove it, as needed.
            //this.inventoryTableAdapter.Fill(this.supubanaDataSet.inventory);




        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    
        private void checkoutcart(int refId,String itemID,string type, string size,string unit, string color,string thickness, float price, float quantity,float subtotal)
        {

            try
            {

                SqlConnection connection = new SqlConnection(Properties.Settings.Default.supubanaConnectionString);
                string query = "INSERT INTO [dbo].[cart]([refID],[itemID],[Size],[SizeUnit],[Color],[Thickness],[unitPrice],[Quantity],[Subtotal],[Type]) VALUES (" + refId + "," + itemID + ",'" + size + "','"+unit+"','" + color + "',"+thickness+","+ price+"," + quantity + "," + total +",'"+type+ "');UPDATE supubana.dbo.inventory_specs SET quantity=quantity-" + quantity + " where itemID=" + itemID + " AND Type='"+type+"' and Size=" + size + " AND Color='" + color + "' and Thickness="+thickness;
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void checkoutcartpm(int refId, String itemID, string type, string size, string unit, string color, string thickness, float price, float quantity, float subtotal)
        {

            try
            {
                

                SqlConnection connection = new SqlConnection(Properties.Settings.Default.supubanaConnectionString);
                string query = "INSERT INTO [dbo].[cart]([refID],[itemID],[Size],[SizeUnit],[Color],[Thickness],[unitPrice],[Quantity],[Subtotal],[Type]) VALUES (" + refId + "," + itemID + ",'" + size + "','" + unit + "','" + color + "'," + thickness + "," + price + "," + quantity + "," + total + ",'" + type + "');UPDATE supubana.dbo.inventory_specs SET quantity=quantity-"+quantity+" where itemID="+itemID+" AND Type='"+type+"' and Size=1 AND Color='"+color+"' and Thickness=" + thickness;
               
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            String Item, s, s2, unit, thicknesss, color; string qty; float quantity;
            Item = comboBox1.Text;
            s = comboBox5.Text;
            s2 = textBox1.Text;
            unit = label4.Text;
            color = comboBox2.Text;
            thicknesss = comboBox3.Text;
            String Type = comboBox6.Text;
            qty = textBox5.Text;
            float stotalC,stotalA;
            float size;

            if (Item != "" || (s != "" && s2 != "") || unit != "" || color != "" || thicknesss != "" || qty != "")
            {
                //get index of last column
                int index = this.dataGridView1.Rows.Count;

                var t = float.TryParse(textBox5.Text.ToString(), out quantity);
                var x= float.TryParse(textBox1.Text, out size);
                String perUnit = perUnitType(ID);
                // 
                if (perUnit == "yes")
                {
                    q = getquantitypm(ID, Type, color, thicknesss);
                    if ((quantity*size) <=q)
                    {
                        float ttm = quantity * size;
                        
                        var o = float.TryParse(s2, out size);
                        stotalC = (size * counterPrice) * quantity;
                        stotalA = (size * agentPrice) * quantity;
                        this.dataGridView1.Rows.Add(ID, Item,Type, size, unit, color, thicknesss,counterPrice*size, ttm, stotalC);
                       

                        total = total + stotalA;
                        agenttotal = agenttotal + (agent_fee * ttm);
                        counterTotal = counterTotal + stotalC;
                        company_fee = company_fee + (company * ttm);
                        office_fee = office_fee + (office * ttm);
                        office_expense = office_expense + (offexp * ttm);
                        label9.Text = counterTotal.ToString();
                        label11.Text = agenttotal.ToString();
                        textBox5.Clear();
                    }
                    else {
                        MessageBox.Show("Not enough in stock: only"+q+" available");
                    }

                    
                } else if (perUnit == "no") {
                    q = getquantity(ID,Type, s, color, thicknesss);
                    if (quantity<=q)
                    {
                       
                        stotalC =  counterPrice * quantity;
                        stotalA = agentPrice * quantity;
                        this.dataGridView1.Rows.Add(ID, Item,Type, s, unit, color, thicknesss, counterPrice, quantity, stotalC);
                        

                        total = total + (agentPrice * quantity);
                        agenttotal = agenttotal + (agent_fee * quantity);
                        counterTotal = counterTotal + (counterPrice * quantity);
                        company_fee = company_fee + (company * quantity);
                        office_fee = office_fee + (office * quantity);
                        office_expense = office_expense + (offexp * quantity);
                        label9.Text = counterTotal.ToString();
                        label11.Text = agenttotal.ToString();
                        textBox5.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Not enough in stock: only" + q + " available");
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Please enter all information required!");
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
        private float getagentpricepm(String i,String Type, String Color, String Thickness)
        {
            try
            {
                string que = "SELECT  [agentPrice] FROM [supubana].[dbo].[inventory_specs] WHERE itemID=" + i + " and Type='" + Type + "' and Size=1 and Color='" + Color + "' and Thickness=" + Thickness;
                var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
                using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    int returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                    return returnValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return 0;
            }

        }
        private float getagentprice(String i, String Type, String size, String Color, String Thickness)
        {
            try
            {
                string que = "SELECT  [agentPrice] FROM [supubana].[dbo].[inventory_specs] WHERE itemID=" + i + " and Type='" + Type + "' and Size=" + size + " and Color='" + Color + "' and Thickness=" + Thickness;
                var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
                using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    int returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                    return returnValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return 0;
            }

        }


       

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedValue.ToString() != null) {
                String status = getAccountStatus(comboBox4.SelectedValue.ToString());
                if (status == "active")
                {
                    agentID = Int32.Parse(comboBox4.SelectedValue.ToString());
                }
                else
                {
                    agentID = 0;
                    MessageBox.Show("Agent account Status :"+status);
                }
            
            }
        }
        private String getAccountStatus(String ID)
        {
            try
            {
                string que = "SELECT [ACCstatus] FROM [supubana].[dbo].[agents] WHERE [agentID]=" + ID;
                var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
                using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    String returnValue = cmd.ExecuteScalar().ToString();
                    return returnValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return "error";
            }

        }
        private String perUnitType(String ID)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return "error";
            }

        }

        private String getunit(String ID)
        {
            try
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
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString());
                return "error";
            }

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

            String perUnit = perUnitType(i);
            if (perUnit=="yes") {
                sizee = textBox1.Text;
                agentPrice = getagentpricepm(i, comboBox6.Text, comboBox2.Text,comboBox3.Text);
                agent_fee = getagentfee(i);
                counterPrice = getcounterpricepm(i,comboBox6.Text, comboBox2.Text, comboBox3.Text);
                company = getcompany_feepm(i,comboBox6.Text, comboBox2.Text, comboBox3.Text);
                office = getoffice_fee(i);
                if (company == 10)
                {
                    offexp = 2;
                }
                else
                {
                    offexp = 0;
                } } 
            else if (perUnit == "no")
            {
                siz = comboBox5.Text;
                agentPrice = getagentprice(i,comboBox6.Text, siz, comboBox2.Text, comboBox3.Text);
                agent_fee = getagentfee(i);
                counterPrice = getcounterprice(i,comboBox6.Text, siz, comboBox2.Text, comboBox3.Text);
                company = getcompany_fee(i, comboBox6.Text, siz, comboBox2.Text, comboBox3.Text);
                office = getoffice_fee(i);
                if (company == 10)
                {
                    offexp = 2;
                }
                else
                {
                    offexp = 0;
                }
            }
        }

        private int getagentfee(string i)
        {
            string que = "SELECT[agent_fee] FROM[supubana].[dbo].[inventory] Where[itemID] =" + i;
            var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
            using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                int returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                return returnValue;
            }

        }
        private void addofficeexpense(float amount)
        {

            try
            {

                SqlConnection connection = new SqlConnection(Properties.Settings.Default.supubanaConnectionString);
                string query = "UPDATE [dbo].[expenseBalance] SET [expenseBalance] = [expenseBalance]+"+amount+" WHERE id=1";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private float getcounterprice(String i, String Type, String size, String Color, String Thickness)
        {
            try
            {
                string que = "SELECT  [counterPrice] FROM [supubana].[dbo].[inventory_specs] WHERE itemID=" + i + " and Type='" + Type + "' and Size=" + size + " and Color='" + Color + "' and Thickness=" + Thickness;

                var sql = string.Format(que, @"Data Source=USER-PC\SUPUBANA; Initial Catalog=supubana; Integrated Security=True");
                using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    float returnValue = float.Parse(cmd.ExecuteScalar().ToString());
                    return returnValue;
                }
            }
            catch (Exception x) {
                MessageBox.Show(x.Message);
                return 0;
            }
            

        }
        private float getcounterpricepm(String i,String Type, String Color, String Thickness)
        {
            try
            {
                string que = "SELECT  [counterPrice] FROM [supubana].[dbo].[inventory_specs] WHERE itemID=" + i + " and Type='" + Type + "' and Size=1 and Color='" + Color + "' and Thickness=" + Thickness;
               
                var sql = string.Format(que, @"Data Source=USER-PC\SUPUBANA; Initial Catalog=supubana; Integrated Security=True");
                using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    float returnValue = float.Parse(cmd.ExecuteScalar().ToString());
                    return returnValue;
                }

            }
            catch (Exception x) {
                MessageBox.Show(x.Message);
                return 0;
            }
           

        }
        private int getquantitypm(String i,String Type,String Color,String Thickness)
        {
            try {
                string que = "SELECT  [quantity] FROM [supubana].[dbo].[inventory_specs] WHERE itemID=" + i + " and Type='" + Type + "' and Size=1 and Color='" + Color + "' and Thickness=" + Thickness;
                var sql = string.Format(que, @"Data Source=USER-PC\SUPUBANA; Initial Catalog=supubana; Integrated Security=True");
                using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    int returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                    return returnValue;
                }
            }catch(Exception x){
                MessageBox.Show(x.Message);
                return 0;
            }

        }
        private int getquantity(String i, String Type, String size, String Color, String Thickness)
        {
            try {
                string que = "SELECT  [quantity] FROM [supubana].[dbo].[inventory_specs] WHERE itemID=" + i + " and Type='" + Type + "' and Size=" + size + " and Color='" + Color + "' and Thickness=" + Thickness;
                var sql = string.Format(que, @"Data Source=USER-PC\SUPUBANA; Initial Catalog=supubana; Integrated Security=True");
                using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    int returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                    return returnValue;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
                return 0;
            }

        }

        private float getoffice_fee(string i)
        {
            try {
                string que = "SELECT[office_fee] FROM[supubana].[dbo].[inventory] Where[itemID] =" + i;
                var sql = string.Format(que, @"Data Source=USER-PC\SUPUBANA; Initial Catalog=supubana; Integrated Security=True");
                using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    float returnValue = float.Parse(cmd.ExecuteScalar().ToString());
                    return returnValue;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
                return 0;
            }

        }
        private float getcompany_fee(String i, String Type, String size, String Color, String Thickness)
        {
            try {
                string que = "SELECT  [company_fee] FROM [supubana].[dbo].[inventory_specs] WHERE itemID=" + i + " and Type='" + Type + "' and Size=" + size + " and Color='" + Color + "' and Thickness=" + Thickness;
                var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
                using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    float returnValue = float.Parse(cmd.ExecuteScalar().ToString());
                    return returnValue;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
                return 0;
            }

        }
        private float getcompany_feepm(String i, String Type, String Color, String Thickness)
        {
            try
            {
                string que = "SELECT  [company_fee] FROM [supubana].[dbo].[inventory_specs] WHERE itemID=" + i + " and Type='" + Type + "' and Size=1 and Color='" + Color + "' and Thickness=" + Thickness;
                var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
                using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    float returnValue = float.Parse(cmd.ExecuteScalar().ToString());
                    return returnValue;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
                return 0;
            }

        }


        private int getrefID()
        {
            try {
                string que = "SELECT IDENT_CURRENT('sales_summary')";
                var sql = string.Format(que, Properties.Settings.Default.supubanaConnectionString);
                using (var con = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    int returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                    return returnValue;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
                return 0;
            }

        }


        private void button4_Click(object sender, EventArgs e)
        {
            string clientName, phone, orderstatus = "", salestatus = "";
            float amountPaid, balance;
            
            clientName = textBox2.Text;
            phone = textBox3.Text;
            int userID = Int32.Parse(UID);
            

            if (clientName == "" || phone == "" ||  (radioButton1.Checked != true && radioButton2.Checked != true) || (radioButton4.Checked != true && radioButton3.Checked != true))
            {
                MessageBox.Show("Please fill in all fields!");
            }
            else
            {
                amountPaid = float.Parse(textBox4.Text);
                balance = finaltotal - amountPaid;

                
                //agentID = Int32.Parse(comboBox4.Text);


                try
                {
                    if (radioButton1.Checked == true)
                    {
                        orderstatus = "Quotation";
                    }
                    if (radioButton2.Checked == true)
                    {
                        orderstatus = "Invoice";
                    }
                    if (radioButton4.Checked == true)
                    {
                        salestatus = "not collected";

                    }
                    if (radioButton3.Checked == true)
                    {
                        salestatus = "collected";
                    }

                    
                        SqlConnection connection = new SqlConnection(Properties.Settings.Default.supubanaConnectionString);
                        SqlDataAdapter cmd = new SqlDataAdapter();
                        // DateTime dt1 = DateTime.Now;
                        string query = "INSERT INTO [dbo].[sales_summary] ([clientName],[PhoneNo],[Total],[discount],[TotalPaid],[Balance],[agentID],[agentTotal],[officeTotal],[officeExpense],[orderStatus],[userID],[Date],[companyTotal]) VALUES ('" + clientName + "','" + phone + "'," + total + ",0," + amountPaid + "," + balance + "," + agentID + "," + agenttotal + "," + office_fee + "," + office_expense + ",'" + salestatus + "'," + userID + ",CURRENT_TIMESTAMP,"+company_fee+")";
                        //MessageBox.Show(query);
                        SqlCommand command = new SqlCommand(query, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        refi = getrefID();
                        foreach (DataGridViewRow Datarow in dataGridView1.Rows)
                        {
                            if (Datarow.Cells[0].Value != null && Datarow.Cells[1].Value != null)
                            {
                                String itemID = Datarow.Cells[0].Value.ToString();
                                String Type = Datarow.Cells[2].Value.ToString();
                                String Size = Datarow.Cells[3].Value.ToString();
                                String Unit = Datarow.Cells[4].Value.ToString();
                                String Color = Datarow.Cells[5].Value.ToString();
                                String thickness = Datarow.Cells[6].Value.ToString();
                                float price = float.Parse(Datarow.Cells[7].Value.ToString());
                                float quantity = float.Parse(Datarow.Cells[8].Value.ToString());
                                float stotal = float.Parse(Datarow.Cells[9].Value.ToString());

                                if (radioButton1.Checked == true || (radioButton2.Checked == true && radioButton4.Checked == true))
                                {

                                    MessageBox.Show("System Quotations will not be stored!");

                                }
                                else if (radioButton2.Checked == true && radioButton3.Checked == true)
                                {
                                    string r = perUnitType(itemID);
                                    if (r == "no") {
                                        checkoutcart(refi, itemID, Type, Size, Unit, Color, thickness, price, quantity, stotal);
                                        if (offexp > 0)
                                        {
                                            addofficeexpense(offexp);
                                        }

                                    } else if (r=="yes") {
                                        checkoutcartpm(refi, itemID, Type, Size, Unit, Color, thickness, price, quantity, stotal);
                                        if (offexp > 0)
                                        {
                                            addofficeexpense(offexp);
                                        }
                                    }
                                    
                                }


                            }

                        }
                    
                   
                    //clear input
                   
                    
                    
                    label9.Text = total.ToString();
                    label11.Text = agenttotal.ToString();
                    
                    MessageBox.Show("Added successfully! ");

                    //print here
                    try
                    {

                        //added
                        // select printer and get printer settings
                        //string filePathReceipt = AppDomain.CurrentDomain.BaseDirectory + @"receipt.txt";
                        string filePathReceipt = Path.GetTempFileName();

                        File.WriteAllText(filePathReceipt, "printing");

                        ProcessStartInfo psi = new ProcessStartInfo(filePathReceipt);
                        psi.Verb = "PRINT";

                        try
                        {
                            PrintService ps = new PrintService();
                            //ps.StartPrint("33333","txt");//Print text
                           
                            ps.StartPrint(WriteTxt(refi.ToString()), "txt");
                            

                            dataGridView1.Rows.Clear();
                                dataGridView1.Refresh();
                                
                            total = 0;
                            agenttotal = 0;
                            finaltotal = 0;
                            counterTotal = 0;
                            company_fee = 0;
                            office_fee = 0;
                            office_expense=0;
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                            label9.Text = 0.ToString();
                            label11.Text = 0.ToString();

                            //    }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }


                        //if (File.Exists(filePathReceipt))
                        {
                            //Delete the file after it has been printed
                           // File.Delete(filePathReceipt);
                        }

                        //added end

                    }

                    catch (Exception errore)
                    {
                        MessageBox.Show(errore.Message);
                    }
                }
                finally
                {
                   // streamToPrint.Close();
                }

            }
        }
       
        //startb here
        public string WriteTxt(string refID)
        { 
            StringBuilder sb = new StringBuilder();
            String tou = "Supubana Enterprise";
            String address ="Plot No.2858 Nakatindi Road,"+"\r\n" + "opposite st Francis";
            string TPIN = "1003916133";
            sb.Append("            " + tou + "     \r\n");
            sb.Append("-----------------------------------------------------------------\r\n");
            sb.Append("date:" + DateTime.Now.ToShortDateString() + " " + "Receipt Number:" + refID + "\r\n");
            sb.Append("Client:" + textBox2.Text + " " + "Sales Agent:" +comboBox4.Text + "\r\n");
            sb.Append("-----------------------------------------------------------------\r\n");
            sb.Append("Product    Description" + "\t" + "Qty" + "\t" + "price" +  "\r\n");
                table = dataGridView1;
            
                foreach (DataGridViewRow Datarow in table.Rows)
                {
                    if (Datarow.Cells[0].Value != null && Datarow.Cells[1].Value != null)
                    {
                        String item = Datarow.Cells[1].Value.ToString();
                        String Type = Datarow.Cells[2].Value.ToString();
                        String Size = Datarow.Cells[3].Value.ToString();
                        String Unit = Datarow.Cells[4].Value.ToString();
                        String Color = Datarow.Cells[5].Value.ToString();
                        String thickness = Datarow.Cells[6].Value.ToString();
                        float prce = float.Parse(Datarow.Cells[7].Value.ToString());
                        float quantity = float.Parse(Datarow.Cells[8].Value.ToString());
                        float stotal = float.Parse(Datarow.Cells[9].Value.ToString());

                        sb.Append(item +" "+ Type+" "+Size+Unit+" "+thickness + "\t" + quantity + "\t" + prce );
                        sb.Append("\r\n");
                    }
                }
                //ends here

            
            sb.Append("-----------------------------------------------------------------\r\n");
            sb.Append("TPIN: " + TPIN + "\r\n");
            sb.Append("Total: " + total + "\r\n");
            sb.Append("Cash received" + " " + textBox4.Text + "\r\n");
            sb.Append("-----------------------------------------------------------------\r\n");
            sb.Append("Address:" + address + "\r\n");
            sb.Append("phone:+260963282867,+260978715586 \r\n");
            sb.Append(" Thank you! Call again!");
            return sb.ToString();

        }
        //ends here
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                string i = item.Cells[0].Value.ToString();
                string type = item.Cells[2].Value.ToString();
                string size= item.Cells[3].Value.ToString();
                string color = item.Cells[5].Value.ToString();
                string thickness = item.Cells[6].Value.ToString();


                agentPrice = getagentprice(i,type,size,color,thickness);
                agent_fee = getagentfee(i);
                counterPrice = getcounterprice(i, type, size, color, thickness);
                company = getcompany_fee(i, type, size, color, thickness);
                office = getoffice_fee(i);
                if (company == 10)
                {
                    offexp = 2;
                }
                else
                {
                    offexp = 0;
                }
                int qty = Int32.Parse(item.Cells[8].Value.ToString());
               
                total = total - (agentPrice * qty);
                agenttotal = agenttotal - (agent_fee * qty);
                counterTotal = counterTotal - (counterPrice * qty);
                company_fee = company_fee - (company * qty);
                office_fee = office_fee - (office * qty);
                office_expense = office_expense - (offexp * qty);
          
                label9.Text = total.ToString();
                label11.Text = agenttotal.ToString();
                dataGridView1.Rows.RemoveAt(item.Index);
                int indx = item.Index;
                MessageBox.Show(indx.ToString());
                

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form manager = new managerhome(UID);
            manager.Show();
            this.Hide();
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
        private void getthickness(string i)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.supubanaConnectionString))
            {
                try
                {
                    
                   
                    
                        using (SqlDataAdapter sda = new SqlDataAdapter("SELECT [Thickness] FROM [supubana].[dbo].[inventory_specs] WHERE itemID="+i, conn))
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
                else if(perUnit=="no")
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
    }
}
