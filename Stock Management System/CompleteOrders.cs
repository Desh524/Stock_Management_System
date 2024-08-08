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

namespace Stock_Management_System
{
    public partial class CompleteOrders : Form
    {
        public CompleteOrders()
        {
            
            InitializeComponent();
        }

        private void GetOrders()
        {          
                MainClass.con.Open();
                String qry = "Select * from Orders";
                SqlDataAdapter sda = new SqlDataAdapter(qry, MainClass.con);
                SqlCommandBuilder scb = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                OrdersDG.DataSource = ds.Tables[0];
                MainClass.con.Close();
            
        }

        private void CompleteOrders_Load(object sender, EventArgs e)
        {

        }
    }
}
