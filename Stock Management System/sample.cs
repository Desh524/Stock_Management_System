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
    public partial class sample : Form
    {
        public sample()
        {
            InitializeComponent();
        }

        private void sample_Load(object sender, EventArgs e)
        {
            try {
                MainClass.con.Open();
                DataSet ds = new DataSet();
                String sto_Name = "Select name from stock_details";
                SqlDataAdapter sda = new SqlDataAdapter(sto_Name, MainClass.con);
                sda.Fill(ds);
                SearchCb.DataSource = ds.Tables[0];
                SearchCb.DisplayMember = ds.Tables[0].Columns[0].ToString();
                MainClass.con.Close();


            }
            catch (Exception ex) {
                MessageBox.Show("Error in loading CB"+ex);
            }
            finally {
                MainClass.con.Close();
            }
            SearchCb.ResetText();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                MainClass.con.Open();
                SqlCommand comma = new SqlCommand("Select * from stock_details where (name = '" + SearchCb.Text + "')", MainClass.con);
                SqlDataReader reader;
                reader = comma.ExecuteReader();
                while (reader.Read()) {
                    txtName.Text = reader.GetValue(1).ToString();
                    txtPrice.Text = reader.GetValue(5).ToString();
                    int quantity = reader.GetInt32(3);
                    numericUpDown1.Maximum = quantity;
                    numericUpDown1.Value = quantity;
                }
                MainClass.con.Close();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                MainClass.con.Close();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = new NumericUpDown
            {
                Location = new System.Drawing.Point(50, 50),
                Minimum = 0,               
                Increment = 1,
                Value = 0
            };

            // Add the ValueChanged event handler
         //   numericUpDown.ValueChanged += new EventHandler(NumericUpDown_ValueChanged);

            // Add the control to the form
           // Controls.Add(numericUpDown);
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Handle the ValueChanged event
          //  NumericUpDown numericUpDown = sender as NumericUpDown;
            
        }
    }
    }

