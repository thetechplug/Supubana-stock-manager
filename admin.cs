using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supubana_stock_manager
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string LoginName, Password;
            LoginName = textBox1.Text;
            Password = textBox2.Text;
            if (LoginName == "admin" && Password == "1REFvLD8TP2F")
            {
                
                Form adminhome = new adminhome();
                adminhome.Show();
                this.Hide();
            }
            else
            {
                label4.Visible = true;
                textBox1.Focus();

            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form l = new Login();
            l.Show();
            this.Hide();
        }
    }
}
