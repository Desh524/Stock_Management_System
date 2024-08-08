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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            calCategory();
            calSupplier();
            calCustomer();
           // calMaxOrder();
            //BestCustomer();
          //  LatestOrder();
            CalProducts();
            FillChart();
             
        }

        private void FillChart() {
            MainClass.con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select quantity ,name From stock_details",MainClass.con);
            sda.Fill(dt);
            chart1.DataSource = dt;

            MainClass.con.Close();
            chart1.Series["Stock"].XValueMember = "name";
            chart1.Series["Stock"].YValueMembers = "quantity";
            chart1.Titles.Add("Quantity of Products");                        
        }
        private void calCategory() {
            MainClass.con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) From CategoryTbl", MainClass.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CatNumbrlbl.Text = dt.Rows[0][0].ToString();
            MainClass.con.Close();
        }

        private void calSupplier()
        {
            MainClass.con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) From SupplierTbl", MainClass.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SupNumbrlbl.Text = dt.Rows[0][0].ToString();
            MainClass.con.Close();
        }

        private void calCustomer()
        {
            MainClass.con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) From CustomerDetailsTbl", MainClass.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CustNumbrlbl.Text = dt.Rows[0][0].ToString();
            MainClass.con.Close();
        }
        private void CalProducts() {
            MainClass.con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From stock_details",MainClass.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ProductLbl.Text = dt.Rows[0][0].ToString();
            MainClass.con.Close();
        }
        private void calMaxOrder()
        {
            MainClass.con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("Select Max(Sub_total) From Orders", MainClass.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Maxlbl.Text = "Rs. "+dt.Rows[0][0].ToString();
            MainClass.con.Close();
        }
        /*
        private void BestCustomer()
        {
            MainClass.con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("Select CustomerDetailsTbl.Cus_Name From CustomerDetailsTbl join Orders on CustomerDetailsTbl.Id = Orders.Customer_Id where Orders.Sub_total=(select max(Sub_total)from Orders)", MainClass.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Bestlbl.Text = dt.Rows[0][0].ToString();
            MainClass.con.Close();
        }
        
        private void LatestOrder()
        {
            MainClass.con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("Select Max(Order_Date) From Orders", MainClass.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            String FullDate = dt.Rows[0][0].ToString();
            String ShortDate = FullDate.Substring(0,10);
            Latestlbl.Text = ShortDate;
            MainClass.con.Close();
        }
        */
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

        private void btnDash_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void CustNumbrlbl_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void bunifuGradientPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
