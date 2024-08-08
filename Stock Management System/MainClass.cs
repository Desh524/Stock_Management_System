using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace Stock_Management_System
{
    class MainClass
    {
        public static readonly string con_string = @"Data Source=DESHAN\SQLEXPRESS;Initial Catalog = Stock_Management; Integrated Security = True";
        public static SqlConnection con = new SqlConnection(con_string);
        //SqlConnection Con = new SqlConnection(@"Data Source=DESHAN\SQLEXPRESS;Initial Catalog=Stock_Management;Integrated Security=True");
    }
}
