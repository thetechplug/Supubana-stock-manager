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
    public partial class adminhome : Form
    {
        public adminhome()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Form newUser = new adduser();
            newUser.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form j = new Addagents();
            j.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form q = new admin();
            q.Show();
            this.Hide();
        }
    }
}
