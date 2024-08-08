using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_Management_System
{
    public partial class LoadForm : Form
    {
        int valuePro=0;
        public LoadForm()
        {
            InitializeComponent();
        }

        private void bunifuCircleProgress1_ProgressChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            valuePro += 1;
            bunifuCircleProgress1.Value = valuePro;
            if (bunifuCircleProgress1.Value == 100)
            {
                bunifuCircleProgress1.Value = 0;
                timer1.Stop();                
                Login obj = new Login();
                obj.Show();
                this.Hide();
            }
            
                
            
        }

        private void LoadForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
                
            