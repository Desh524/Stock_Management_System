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
    public partial class Order1 : Form
    {
        public Order1()
        {
            InitializeComponent();
            GetCustomer();
            clear();
           // UpdateStock();
        }

        private void clear() {
            PrNametxt.Text = String.Empty;
            ProPricetxt.Text = String.Empty;
            UDquantity.ResetText();
            txtBalance.Text = String.Empty;
            txtCash.Text = String.Empty;
        }

        
        private void UpdateStock()
        {
            try
            {
                MainClass.con.Open();

                string qury = "SELECT id FROM stock_details WHERE name = @Pname";
                SqlCommand geId = new SqlCommand(qury,MainClass.con);
                SqlCommand cmm = new SqlCommand("SELECT id FROM stock_details WHERE name = @Pname",MainClass.con);
                cmm.Parameters.AddWithValue("@Pname", PrNametxt.Text);

                cmm.ExecuteNonQuery();


                string productName = SearchCb.SelectedValue.ToString();
                string query = "SELECT id FROM stock_details WHERE name = @productName";
                SqlCommand getid = new SqlCommand(query, MainClass.con);
                getid.Parameters.AddWithValue("@productName", productName);
                string productID = getid.ExecuteScalar()?.ToString();

                SqlCommand cmd = new SqlCommand("update stock_details set quantity= (quantity-@qnty) where id=@pkey ", MainClass.con);
                SqlCommand scmd = new SqlCommand("");

                string gotqunty = UDquantity.Text;
                int.TryParse(gotqunty, out int intValue);

                cmd.Parameters.AddWithValue("@qnty", gotqunty);
                cmd.Parameters.AddWithValue("@pkey", productID);
                cmd.ExecuteNonQuery();
                MainClass.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally {
                MainClass.con.Close();
            }
        }
        
        private void GetCustomer()
        {
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

     
        private void txtResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void TotalLbl_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ProPricetxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void PrNametxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Printbtn_Click(object sender, EventArgs e)
        {

        }

        private void AddBillbtn_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BillDG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void productCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void userCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Canclbtn_Click(object sender, EventArgs e)
        {

        }

        private void Orderbtn_Click(object sender, EventArgs e)
        {

        }

        private void OrderDG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void orderdt_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CustomerCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void amounttxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void quantitxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Order1_Load(object sender, EventArgs e)
        {
            try
            {
                MainClass.con.Open();
                DataSet ds = new DataSet();
                String sto_Name = "Select name from stock_details";
                SqlDataAdapter sda = new SqlDataAdapter(sto_Name, MainClass.con);
                sda.Fill(ds);
                SearchCb.DataSource = ds.Tables[0];
                SearchCb.DisplayMember = ds.Tables[0].Columns[0].ToString();
                MainClass.con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in loading CB" + ex);
            }
            finally
            {
                MainClass.con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                MainClass.con.Open();
                SqlCommand comma = new SqlCommand("Select * from stock_details where (name = '" + SearchCb.Text + "')", MainClass.con);
                SqlDataReader reader;
                reader = comma.ExecuteReader();
                while (reader.Read())
                {
                    PrNametxt.Text = reader.GetValue(1).ToString();
                    ProPricetxt.Text = reader.GetValue(5).ToString();

                    if (reader.GetInt32(3)==0) {
                        MessageBox.Show("this product is out of stock..");
                    }
                    else {
                        int quantity = reader.GetInt32(3);
                        UDquantity.Maximum = quantity;
                        UDquantity.Value = quantity;
                    }
                    
                }
                MainClass.con.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                MainClass.con.Close();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int n =1;
        int caltotal = 0;
        int discount = 0;
        int discountedPrice = 0;
        private void BtnAdd_Click(object sender, EventArgs e)
        {
              MainClass.con.Open();
                String querry = "Select id from CustomerDetailsTbl where Cus_Name = @name";
                SqlCommand cmd = new SqlCommand(querry, MainClass.con);
                cmd.Parameters.AddWithValue("@name",CustomerCb.SelectedValue.ToString());
                String cus_id = cmd.ExecuteScalar().ToString();

            int total = Convert.ToInt32(UDquantity.Text) * Convert.ToInt32(ProPricetxt.Text);
            DataGridViewRow bill = new DataGridViewRow();
                    bill.CreateCells(BillDG);
                    bill.Cells[0].Value = n ;
                    bill.Cells[1].Value = cus_id;
                    bill.Cells[2].Value = PrNametxt.Text;
                    bill.Cells[3].Value = ProPricetxt.Text;
                    bill.Cells[4].Value = UDquantity.Text;
                    bill.Cells[5].Value = total;
                    BillDG.Rows.Add(bill);
                   // n++;
                    caltotal = caltotal + total;                                      
                    txtsubtotal.Text = caltotal + ".00";

            clear();

            // UpdateStock();

            //cal discount
            if (caltotal < 5000)
            {
                discount = caltotal * 3/100 ;

            } else if (caltotal < 10000)
            {
                discount = caltotal * 5/100;
            }
            else
            {
                discount = caltotal * 10/100;
            }
            discountedPrice = caltotal - discount;
            txtDiscount.Text = discountedPrice + ".00";


                    

            MainClass.con.Close();
            
        }

        private void UDquantity_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = new NumericUpDown
            {
                Location = new System.Drawing.Point(50, 50),
                Minimum = 0,
                Increment = 1,
                Value = 0
            };

            // Add the ValueChanged event handler
            numericUpDown.ValueChanged += new EventHandler(NumericUpDown_ValueChanged);

            // Add the control to the form
           // Controls.Add(numericUpDown);
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Handle the ValueChanged event
            NumericUpDown numericUpDown = sender as NumericUpDown;
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
        
        private void txtCash_Click(object sender, EventArgs e)
        {
            int cash = Int32.Parse(txtCash.Text);
            txtBalance.Text = cash - discountedPrice + ".00";
        }

        private void txtCash_Enter(object sender, EventArgs e)
        {
            //int cash = Int32.Parse(txtCash.Text);
           // txtBalance.Text = cash - discountedPrice + ".00";
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
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
            Order1 obj =new Order1();
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

        private void BtnClickHere_Click(object sender, EventArgs e)
        {
            CompleteOrders obj = new CompleteOrders();
            obj.Show();
        }

        private void Butbtn_Click(object sender, EventArgs e)
        {
            n++;
            try {
                MainClass.con.Open();
                foreach (DataGridViewRow row in BillDG.Rows)
                {
                    if (row.IsNewRow) continue;
                    String qury = "INSERT INTO BillTbl (Bill_Id,Customer_Id,Product_Name,Unit_Price,Quantity,Total_Price) VALUES(@Bill_Id,@Customer_Id,@Product_Name,@Unit_Price,@Quantity,@Total_Price)";
                    SqlCommand cmd = new SqlCommand(qury, MainClass.con);

                    cmd.Parameters.AddWithValue("@Bill_Id", Convert.ToInt32(row.Cells[0].Value));
                    cmd.Parameters.AddWithValue("@Customer_Id", Convert.ToInt32(row.Cells[1].Value));
                    cmd.Parameters.AddWithValue("@Product_Name", row.Cells[2].Value);
                    cmd.Parameters.AddWithValue("@Unit_Price", Convert.ToInt32(row.Cells[3].Value));
                    cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(row.Cells[4].Value));
                    cmd.Parameters.AddWithValue("@Total_Price", Convert.ToInt32(row.Cells[5].Value));

                    cmd.ExecuteNonQuery();
                }
                foreach (DataGridViewRow row in BillDG.Rows)
                {
                    if (row.IsNewRow) continue;
                    String query = "INSERT INTO Orders (Bill_id,Sub_total,Order_Date,Customer_Id) VALUES(@Bill_id,@Sub_total,@Order_Date,@Customer_Id)";
                    SqlCommand cmm = new SqlCommand(query, MainClass.con);

                    cmm.Parameters.AddWithValue("@Bill_id", Convert.ToInt32(row.Cells[0].Value));
                    cmm.Parameters.AddWithValue("@Sub_total", Convert.ToInt32(txtsubtotal.Text));
                    cmm.Parameters.AddWithValue("@Order_Date", Convert.ToInt32(orderdt.Text));
                    cmm.Parameters.AddWithValue("@Customer_Id", Convert.ToInt32(row.Cells[1].Value));

                    cmm.ExecuteNonQuery();
                }
                MainClass.con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                MainClass.con.Close();
            }
        }
    }
    }

