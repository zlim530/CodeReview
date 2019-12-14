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

namespace TestConnectSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Server=.;database=demoone;Integrated Security=True";

                conn.Open();
                MessageBox.Show("已经正确建立连接","连接正确对话框 ");
            }
            catch (SqlException SQL)
            {
                MessageBox.Show(SQL.Message,"连接失败对话框");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = "data source=.;Initial Catalog=demoone;Integrated Security=true";
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Insert Into student values('2','liliy',19),('3','mark',20),('4','jayzhou',22),('5','jerry',20),('6','kat',19)";
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            MessageBox.Show("记录已插入！","提示");
            conn.Close();
        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }
    }
}
