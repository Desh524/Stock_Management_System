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

    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
            GetCustomer();
            GetProid();
            GetProName();
            ShowOrders();
            UpdateStock();

        }

        private void GetCustomer() {
            MainClass.con.Open();
            SqlCommand cmd = new SqlCommand("Select * from CustomerDetailsTbl", MainClass.con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Cus_Name", typeof(String));
            dt.Load(Rdr);
            CustomerCb.ValueMember = "Cus_Name";
            CustomerCb.DataSource = dt;
            MainClass.con.Close();
        }

        
        private void UpdateStock() {
            try {
                MainClass.con.Open();
                SqlCommand cmd = new SqlCommand("update stock_details set quantity= (quantity-@qnty) where id=@pkey ",MainClass.con);

                string gotqunty = quantitxt.Text;
                int.TryParse(gotqunty, out int intValue);

                cmd.Parameters.AddWithValue("@qnty",gotqunty);
                cmd.Parameters.AddWithValue("@pkey", productCb.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
                MainClass.con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetProid(){
            MainClass.con.Open();
            SqlCommand cmd = new SqlCommand("Select * from stock_details", MainClass.con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(String));
            dt.Load(Rdr);
            productCb.ValueMember = "id";
            productCb.DataSource = dt;
            MainClass.con.Close();
        }

        private void GetProName()
        {
            MainClass.con.Open();
            String qry="Select * from stock_details where id='"+productCb.SelectedValue.ToString()+"'";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows){
                PrNametxt.Text = dr["name"].ToString();
                ProPricetxt.Text = dr["sPrice"].ToString();
                quantitxt.Text = dr["quantity"].ToString();
            }
            
            MainClass.con.Close();
        }

        private void ShowOrders() {
            MainClass.con.Open();
            String qry = "Select * from OrderTbl";
            SqlDataAdapter sda = new SqlDataAdapter(qry, MainClass.con);
            SqlCommandBuilder scb = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            OrderDG.DataSource = ds.Tables[0];       
            MainClass.con.Close();
        }

        private void Orderbtn_Click(object sender, EventArgs e)
        {
             
            if (CustomerCb.SelectedIndex == -1 || userCb .SelectedIndex == -1 || amounttxt.Text == "")
            {
                guna2MessageDialog1.Show("Missing Any Data");
            }
            else {           
                try
                {
                    MainClass.con.Open();
                    
                    SqlCommand cmd = new SqlCommand("insert into OrderTbl(CusId,UserId,BillDate,BillAmount) values(@CID,@UID,@BDT,@BANT)", MainClass.con);
                    cmd.Parameters.AddWithValue("@CID", CustomerCb.SelectedIndex);
                    cmd.Parameters.AddWithValue("@UID", userCb.SelectedIndex);
                    cmd.Parameters.AddWithValue("@BDT",orderdt.Value.Date);
                    cmd.Parameters.AddWithValue("@BANT",amounttxt.Text);                   
                    cmd.ExecuteNonQuery();
                    MainClass.con.Close();

                    /*string qry = ("insert into OrderTbl (CusId,UserId,BillDate,BillAmount) values ('" + CustomerCb.SelectedIndex + "','" + userCb.SelectedIndex + "','" + orderdt.Value.Date + "','" + amounttxt.Text + "')");
                    SqlDataAdapter sda = new SqlDataAdapter(qry, MainClass.con);
                    sda.SelectCommand.ExecuteNonQuery();
                    */


                    guna2MessageDialog1.Show("Order added..!");
                    ShowOrders();                                      
                    
                }
                catch (Exception Ex){
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        //Nevigation bar start
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
            Order obj = new Order();
            obj.Show();
            this.Hide();
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
            
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

        //Nevigation bar finished

        int n = 0;
        int caltotal = 0;

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (PrNametxt.Text == "" || quantitxt.Text == "")
            {
                MessageBox.Show("Missing Information ...");
            }
            else {
                int total = Convert.ToInt32(quantitxt.Text) * Convert.ToInt32(ProPricetxt.Text);
                DataGridViewRow bill = new DataGridViewRow();
                bill.CreateCells(BillDG);
                bill.Cells[0].Value = n + 1;
                bill.Cells[1].Value = PrNametxt.Text;
                bill.Cells[2].Value = ProPricetxt.Text;
                bill.Cells[3].Value = quantitxt.Text;
                bill.Cells[4].Value = total;
                BillDG.Rows.Add(bill);
                n++;
                caltotal = caltotal + total;
                TotalLbl.Text = "Rs." + caltotal;
                amounttxt.Text = "" + caltotal;
                UpdateStock();
            }
        }

        private void productCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetProName();
        }

        private void CustomerCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Printbtn_Click(object sender, EventArgs e)
        {
            txtResult.Clear();
            txtResult.Text += "  ************************************\n";
            txtResult.Text += "  ****        Fees Reciept        ****\n";
            txtResult.Text += "  ************************************\n\n";

            txtResult.Text += "  Date :" + DateTime.Now + "\n\n";

            txtResult.Text += "  Customer Name :'"+CustomerCb.SelectedText+"' \n\n";
            txtResult.Text += "  Product    ";
            txtResult.Text += "  Quantity    ";
            txtResult.Text += "  Total ";
            txtResult.Text += "\n";
            txtResult.Text += PrNametxt.Text + quantitxt.Text + TotalLbl.Text ;
            txtResult.Text += "\n\n\n\n\n";
            txtResult.Text += "               You are Welcome            ";


            string[,] tableData = new string[,]
    {
        { "Product", "Quantity", "Total" },
        { "Book", "2", "150" },
        { "Cream Soda", "1", "350" },
        // Add more rows as needed
    };

            // Print table headers
            for (int i = 0; i < tableData.GetLength(1); i++)
            {
                txtResult.Text += tableData[0, i].PadRight(15);
            }
            txtResult.Text += "\n\n";

            // Print table rows
            for (int i = 1; i < tableData.GetLength(0); i++)
            {
                for (int j = 0; j < tableData.GetLength(1); j++)
                {
                    txtResult.Text += tableData[i, j].PadRight(15);
                }
                txtResult.Text += "\n";
            }
        }

        private void Canclbtn_Click(object sender, EventArgs e)
        {
            
                CustomerCb.Text = string.Empty;
                userCb.Text = string.Empty;
                orderdt.Text = string.Empty;
            
               /* 
                MainClass.con.Open();
                String qry = ("DELETE FROM OrderTbl WHERE OrNum=('" + OrderDG.SelectedRows[0].Cells[0].Value.ToString() + "')");
                SqlDataAdapter sda = new SqlDataAdapter(qry, MainClass.con);
                sda.SelectCommand.ExecuteNonQuery();
                MainClass.con.Close();
                ShowOrders();
            */


            

            
        }

        private void userCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void productCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Order_Load(object sender, EventArgs e)
        {
            productCb.Text = String.Empty;
            quantitxt.Text =string.Empty;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void OrderDG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
