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
    public partial class Login : Form
    {
        internal static Login obj;

        public Login()
        {
            InitializeComponent();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUname.Text == "Admin" && txtPass.Text == "1234")
                {
                    Dashboard Obj = new Dashboard();
                    Obj.Show();
                    this.Hide();
                    guna2MessageDialog1.Show("You Login As "+ txtUname.Text);
                }
                else
                {
                    guna2MessageDialog1.Show("please check your login info...!");
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }


        }

        private void bunifuPictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
