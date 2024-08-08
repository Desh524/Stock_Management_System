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
    public partial class Customer : Form
    {
        int index;
        public Customer()
        {
            InitializeComponent();
            ShowCustomer();
        }

        private void ShowCustomer()
        {
            MainClass.con.Open();
            String query = ("select * from  CustomerDetailsTbl");
            SqlDataAdapter sda = new SqlDataAdapter(query, MainClass.con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            customerDGV.DataSource = ds.Tables[0];
            MainClass.con.Close();
        }

              

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtCusName.Text == "" || txtContact.Text == "" || txtAddress.Text == "" || GenderCb.SelectedIndex == -1)
            {
                guna2MessageDialog1.Show("Missing Any Data");
            }
            else
            {
                try
                {
                    MainClass.con.Open();
                    string qry = ("insert into CustomerDetailsTbl (Cus_Name,Gender,Contact,Address) values ('" + txtCusName.Text + "','" + GenderCb.Text + "','" + txtContact.Text + "','" + txtAddress.Text + "')");
                    SqlDataAdapter sda = new SqlDataAdapter(qry, MainClass.con);
                    sda.SelectCommand.ExecuteNonQuery();
                    
                    guna2MessageDialog1.Show("Product Add Successfully...!");
                    MainClass.con.Close();
                    ShowCustomer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void customerDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            index = e.RowIndex;
            DataGridViewRow row = customerDGV.Rows[index];
            txtID.Text = row.Cells[0].Value.ToString();
            txtCusName.Text = row.Cells[1].Value.ToString();
            GenderCb.Text = row.Cells[2].Value.ToString();
            txtContact.Text = row.Cells[3].Value.ToString();
            txtAddress.Text = row.Cells[4].Value.ToString();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataGridViewRow newdata = customerDGV.Rows[index];
            newdata.Cells[1].Value = txtCusName.Text;
            newdata.Cells[2].Value = GenderCb.Text;
            newdata.Cells[3].Value = txtContact.Text;
            newdata.Cells[4].Value = txtAddress.Text;
        }
        int key = 0;
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                MainClass.con.Open();
                
                SqlCommand command = new SqlCommand("Delete CustomerDetailsTbl Where Id = '"+txtID.Text+"'", MainClass.con);
                
                command.ExecuteNonQuery();
               
                MainClass.con.Close();
                guna2MessageDialog1.Show("Delete Successfully...");
                ShowCustomer();
            }
            catch (Exception ex) {
                guna2MessageDialog1.Show(ex.Message);
            }
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            customerDGV.Columns[0].Visible = false;
        }

        private void btnproduct_Click(object sender, EventArgs e)
        {
            Stocks obj = new Stocks();
            obj.Show();
            this.Hide();

        }

        private void btnCate_Click(object sender, EventArgs e)
        {
            Category Obj = new Category();
            Obj.Show();
            this.Hide();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            Customer Obj = new Customer();
            Obj.Show();
            this.Hide();
        }

        private void btnsupplier_Click(object sender, EventArgs e)
        {
            Supplier obj = new Supplier();
            obj.Show();
            this.Hide();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            
            Order1 obj = new Order1();
            obj.Show();
            this.Hide();
            
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Lbl_Logout_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void customerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
