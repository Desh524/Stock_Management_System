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
    public partial class Supplier : Form
    {
        int index;
        public Supplier()
        {
            InitializeComponent();
            ShowSupplier();
        }

        private void ShowSupplier()
        {
            Con.Open();
            String query = "select * from  SupplierTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SupplierDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=DESHAN\SQLEXPRESS;Initial Catalog=Stock_Management;Integrated Security=True");

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtSName.Text == "" || txtSNumber.Text == "" || txtSAddress.Text == "")
            {
                guna2MessageDialog1.Show("Missing Any Data");
            }
            else
            {
                try
                {
                    Con.Open();
                    string qry = ("insert into SupplierTbl (SName,SMobile_Number,SAddress) values ('" + txtSName.Text + "','" + txtSNumber.Text + "','" + txtSAddress.Text + "')");
                    SqlDataAdapter sda = new SqlDataAdapter(qry, Con);
                    sda.SelectCommand.ExecuteNonQuery();

                    guna2MessageDialog1.Show("Product Add Successfully...!");
                    Con.Close();
                    ShowSupplier();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataGridViewRow newdata = SupplierDGV.Rows[index];
            newdata.Cells[1].Value = txtSName.Text;
            newdata.Cells[2].Value = txtSNumber.Text;
            newdata.Cells[3].Value = txtSAddress.Text;
           
        }

        private void SupplierDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            DataGridViewRow row = SupplierDGV.Rows[index];
            txtSID.Text = row.Cells[0].Value.ToString();
            txtSName.Text = row.Cells[1].Value.ToString();
            txtSNumber.Text = row.Cells[2].Value.ToString();
            txtSAddress.Text = row.Cells[3].Value.ToString();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           
           index = SupplierDGV.CurrentCell.RowIndex;         
           SupplierDGV.Rows.RemoveAt(index);

        }

        private void Lbl_Logout_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnproduct_Click(object sender, EventArgs e)
        {
            Stocks obj = new Stocks();
            obj.Show();
            this.Hide();
        }

        private void btnCate_Click(object sender, EventArgs e)
        {
            Category obj = new Category();
            obj.Show();
            this.Hide();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            Customer obj = new Customer();
            obj.Show();
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

        private void btnDash_Click(object sender, EventArgs e) { 
       
            
            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
            
        }

        private void Supplier_Load(object sender, EventArgs e)
        {

        }
    }
}
