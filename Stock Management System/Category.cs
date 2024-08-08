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
    public partial class Category : Form
    {
        int index;
        public Category()
        {
            InitializeComponent();
            ShowCategory();
            countcat();
        }
        private void ShowCategory()
        {
            MainClass.con.Open();
            String query = "select * from  CategoryTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, MainClass.con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            Categorygrid.DataSource = ds.Tables[0];
            MainClass.con.Close();
        }

       // SqlConnection Con = new SqlConnection(@"Data Source=DESHAN\SQLEXPRESS;Initial Catalog=Stock_Management;Integrated Security=True");

        private void savebtn_Click(object sender, EventArgs e)
        {

            if (CatNametxt.Text == "" )
            {
                guna2MessageDialog1.Show("Missing Any Data");
            }
            else
            {
                try
                {
                    MainClass.con.Open();
//                    Con.Open();
                    string qry = ("insert into CategoryTbl (CatName) values ('" + CatNametxt.Text + "')");
                    SqlDataAdapter sda = new SqlDataAdapter(qry,MainClass.con);
                    sda.SelectCommand.ExecuteNonQuery();

                    //guna2MessageDialog1.Show("Product Add Successfully...!");
                    //Con.Close();
                    MainClass.con.Close();
                    ShowCategory();
                    CatNametxt.Text = String.Empty;
                    countcat();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Categorygrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Stocks Obj = new Stocks();
            Obj.Show();
            this.Hide();
        }

        private void Category_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {

            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
            
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Supplier Obj = new Supplier();
            Obj.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Customer Obj = new Customer();
            Obj.Show();
            this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Category Obj = new Category();
            Obj.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
            Order1 Obj = new Order1();
            Obj.Show();
            this.Hide();
            
        }

        private void Lbl_Logout_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void guna2Button1_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {

            MainClass.con.Open();
                String qry = ("UPDATE CategoryTbl SET CatName =('" + CatNametxt.Text+"') WHERE id=('"+ Categorygrid.SelectedRows[0].Cells[0].Value.ToString()+ "')");
                SqlDataAdapter sda = new SqlDataAdapter(qry, MainClass.con);
                sda.SelectCommand.ExecuteNonQuery();
            MainClass.con.Close();
            ShowCategory();
            CatNametxt.Text = String.Empty;
        }

        private void newbtn_Click(object sender, EventArgs e)
        {
            CatNametxt.Text = String.Empty;
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {

            MainClass.con.Open();
            String qry = ("DELETE FROM CategoryTbl WHERE CatName = ('" + CatNametxt.Text + "') AND id=('"+ Categorygrid.SelectedRows[0].Cells[0].Value.ToString()+ "')");
            SqlDataAdapter sda = new SqlDataAdapter(qry, MainClass.con);
            sda.SelectCommand.ExecuteNonQuery();
            MainClass.con.Close();
            CatNametxt.Text = String.Empty;
            ShowCategory();
            countcat();
        }

        private void Categorygrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;           
            DataGridViewRow row = Categorygrid.Rows[index];
            CatNametxt.Text = row.Cells[1].Value.ToString();           
        }

        private void countcat() {
            MainClass.con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) From CategoryTbl", MainClass.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            catNumbrlbl.Text = dt.Rows[0][0].ToString();
            MainClass.con.Close();
        }
    }
}
