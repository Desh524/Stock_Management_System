using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Stock_Management_System
{
    public partial class Stocks : Form

    {
        int index;
        public Stocks()
        {
            InitializeComponent();
            ShowProduct();
            GetCategory();
            GetSupplier();
           
            //updatebtn.Click += editbtn_Click;
        }
        private void ShowProduct() {
            MainClass.con.Open();
            String query = ("select * from stock_details");
            SqlDataAdapter sda = new SqlDataAdapter(query, MainClass.con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            prodatagrid.DataSource = ds.Tables[0];
            MainClass.con.Close();
        }
        public void Empty() {
            ProNametxt.Text = String.Empty;
            CateCb.Text = string.Empty;
            quantxt.Text = String.Empty;
            BPricetxt.Text = String.Empty;
            SPricetxt.Text = String.Empty;
            ProdDate.Text = String.Empty;
            SuppCb.Text = String.Empty;
        }
        

       // SqlConnection Con = new SqlConnection(@"Data Source=DESHAN\SQLEXPRESS;Initial Catalog=Stock_Management;Integrated Security=True");
        private void Stocks_Load(object sender, EventArgs e)
        {
            ShowProduct();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Category Obj = new Category();
            Obj.Show();
            this.Hide();
        }


        private void bunifuPictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            
            if (ProNametxt.Text == "" || quantxt.Text == "" || BPricetxt.Text == "" || SPricetxt.Text == "" || SuppCb.SelectedIndex == -1 || CateCb.SelectedIndex == -1)
            {
                guna2MessageDialog1.Show("Missing Any Data");
            }
            else {
                int gain = Convert.ToInt32(SPricetxt.Text) - Convert.ToInt32(BPricetxt.Text);
                try
                {
                    MainClass.con.Open();
                    /*
                     SqlCommand cmd = new SqlCommand("Insert into productdetail values(@Name,@Category,@Quantity,@BPrice,@SPrice,@Pro_Date,@Supplier,@Gain)", Con);
                    cmd.Parameters.AddWithValue("@Name",ProNametxt.Text);
                    cmd.Parameters.AddWithValue("@Category",CateCb.SelectedIndex.ToString());
                    cmd.Parameters.AddWithValue("@Quantity",quantxt.Text);
                    cmd.Parameters.AddWithValue("@BPrice",BPricetxt.Text);
                    cmd.Parameters.AddWithValue("@SPrice",SPricetxt.Text);
                    cmd.Parameters.AddWithValue("@Pro_Date",ProdDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Supplier",SuppCb.SelectedIndex);
                    cmd.Parameters.AddWithValue("@Gain",Gain);
                    cmd.ExecuteNonQuery();
                    */
                    string qry = ("insert into stock_details (name,category,quantity,bPrice,sPrice,pro_Date,supplier,gain) values ('" + ProNametxt.Text + "','" + CateCb.Text + "','" + quantxt.Text + "','" + BPricetxt.Text + "','" + SPricetxt.Text + "','" + ProdDate.Text + "','" + SuppCb.Text + "','" + gain + "')");
                    SqlDataAdapter sda = new SqlDataAdapter(qry, MainClass.con);
                    sda.SelectCommand.ExecuteNonQuery();

                    guna2MessageDialog1.Show("Product Add Successfully...!");
                    MainClass.con.Close();
                    ShowProduct();                                      
                    Empty();
                }
                catch (Exception Ex){
                    MessageBox.Show(Ex.Message);
                }
            }
            
        }
        int key = 0 ;
        private void prodatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void editbtn_Click(object sender, EventArgs e)
        {

            //DataGridViewRow newdata = prodatagrid.Rows[index];
            //newdata.Cells[1].Value = ProNametxt.Text;
            //newdata.Cells[2].Value = CateCb.Text;
            // newdata.Cells[3].Value = quantxt.Text;
            // newdata.Cells[4].Value = BPricetxt.Text;
            // newdata.Cells[5].Value = SPricetxt.Text;
            // newdata.Cells[6].Value = ProdDate.Text;
            // newdata.Cells[7].Value = SuppCb.Text;

           /* MainClass.con.Open();
            String qry = ("UPDATE stock_details SET name =('"+ ProNametxt.Text + "'),category,quantity,bPrice,sPrice,pro_Date,supplier =('" + ProNametxt.Text + "','"+ CateCb.Text + "','"+ quantxt.Text + "','"+ BPricetxt.Text + "','"+ SPricetxt.Text + "','"+ ProdDate.Text + "','"+ SuppCb.Text + "') WHERE id=('" + prodatagrid.SelectedRows[0].Cells[0].Value.ToString() + "')");
            SqlDataAdapter sda = new SqlDataAdapter(qry, MainClass.con);
            sda.SelectCommand.ExecuteNonQuery();
            MainClass.con.Close();
            ShowProduct();
            */

            MainClass.con.Open();
            String qery = "UPDATE stock_details SET name = @name, category = @category, quantity = @quantity, bPrice = @bPrice, sPrice = @sPrice, pro_Date = @pro_Date, supplier = @supplier WHERE id = @id";

            SqlCommand cmd = new SqlCommand(qery, MainClass.con);
            cmd.Parameters.AddWithValue("@name", ProNametxt.Text);
            cmd.Parameters.AddWithValue("@category", CateCb.Text);
            cmd.Parameters.AddWithValue("@quantity", quantxt.Text);
            cmd.Parameters.AddWithValue("@bPrice", BPricetxt.Text);
            cmd.Parameters.AddWithValue("@sPrice", SPricetxt.Text);
            cmd.Parameters.AddWithValue("@pro_Date", ProdDate.Text);
            cmd.Parameters.AddWithValue("@supplier", SuppCb.Text);
            cmd.Parameters.AddWithValue("@id", prodatagrid.SelectedRows[0].Cells[0].Value.ToString());

            cmd.ExecuteNonQuery();

            MainClass.con.Close();
            ShowProduct();
            Empty();
            
        }

        private void prodatagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            DataGridViewRow row = prodatagrid.Rows[index];
            ProNametxt.Text = row.Cells[1].Value.ToString();
            CateCb.Text = row.Cells[2].Value.ToString();
            quantxt.Text = row.Cells[3].Value.ToString();
            BPricetxt.Text = row.Cells[4].Value.ToString();
            SPricetxt.Text = row.Cells[5].Value.ToString();
            ProdDate.Text = row.Cells[6].Value.ToString();
            SuppCb.Text = row.Cells[7].Value.ToString();

        }

        private void newbtn_Click(object sender, EventArgs e)
        {
            Empty();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            //index = prodatagrid.CurrentCell.RowIndex;
            //prodatagrid.Rows.RemoveAt(index);
            
                MainClass.con.Open();
                String qry =("DELETE FROM stock_details WHERE name=('"+ ProNametxt.Text+ "') AND id =('" + prodatagrid.SelectedRows[0].Cells[0].Value.ToString()+"')");
                SqlDataAdapter sda = new SqlDataAdapter(qry,MainClass.con);
                sda.SelectCommand.ExecuteNonQuery();        
                MainClass.con.Close();
                Empty();
                ShowProduct();     

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Supplier obj = new Supplier();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Customer obj = new Customer();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CateCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SuppCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ProdDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void SPricetxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void BPricetxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void quantxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void ProNametxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void btnproduct_Click(object sender, EventArgs e)
        {
            Stocks Obj = new Stocks();
            Obj.Show();
            this.Hide();
        }

        private void btnsupplier_Click(object sender, EventArgs e)
        {
            Supplier Obj = new Supplier();
            Obj.Show();
            this.Hide();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            
            Order1 Obj = new Order1();
            Obj.Show();
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

        private void btnDash_Click(object sender, EventArgs e)
        {
            
            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void GetCategory() {
            MainClass.con.Open();
            SqlCommand cmd = new SqlCommand("Select * from CategoryTbl", MainClass.con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(String));
            dt.Load(Rdr);
            CateCb.ValueMember = "CatName";
            CateCb.DataSource = dt;
            MainClass.con.Close();
        }
        private void GetSupplier()
        {
            MainClass.con.Open();
            SqlCommand cmd = new SqlCommand("Select * from SupplierTbl", MainClass.con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SName", typeof(String));
            dt.Load(Rdr);
            SuppCb.ValueMember = "SName";
            SuppCb.DataSource = dt;
            MainClass.con.Close();
        }
    }
}
