using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.OleDb;
namespace OrderSystem
{
    public partial class repass : Form
    {
        public repass()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String conn = "Data Source=.;Initial Catalog=OrderSystem;Integrated Security=True";//（使用本地数据库，我的数据库名为XKGL）
            SqlConnection connection = new SqlConnection(conn);
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "")
                MessageBox.Show("请填写完整信息", "提示");
            else
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("", connection);
                string sql = "select * from Users where 用户ID='" + textBox1.Text.Trim() + "' and 用户密码='" + textBox2.Text.Trim() + "'";
                cmd.CommandText = sql;
                if (null != cmd.ExecuteScalar())
                {
                    if (textBox3.Text.Trim() != textBox4.Text.Trim())
                        MessageBox.Show("两次密码输入不一致!", "警告");
                    else
                    {
                        sql = "update Users set 用户密码='" + textBox3.Text.Trim() + "'where 用户ID='" + textBox1.Text.Trim() + "'";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("密码修改成功，请重新登陆！", "提示");

                        //  Form1 f1 = new Form1();
                        //  f1.ShowDialog();

                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            login f1 = new login();
            f1.ShowDialog();
            this.Hide();
            this.Close();
        }
    }
}
