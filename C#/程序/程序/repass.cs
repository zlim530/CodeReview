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
            if (textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "")
                MessageBox.Show("请填写完整信息", "提示");
            else
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("", connection);
                string sql = "select * from Users where 用户名='" +Common.DataBase.userName + "' and 用户密码='" + textBox2.Text.Trim() + "'";
                cmd.CommandText = sql;
                if (null != cmd.ExecuteScalar())
                {
                    if (textBox3.Text.Trim() != textBox4.Text.Trim())
                        MessageBox.Show("两次密码输入不一致!", "警告");
                    else
                    {
                        sql = "update Users set 用户密码='" + textBox3.Text.Trim() + "'where 用户名='" + Common.DataBase.userName + "'";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("密码修改成功，请重新登陆！", "提示");
                        login login = new login();
                        login.Show();
                        this.Hide();

                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox2.Focus();
            MainForm main = new MainForm();
            this.Hide();
        }

        private void repass_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "当前用户：" + Common.DataBase.userName;
            toolStripStatusLabel3.Text = "当前时间：" + DateTime.Now.ToLocalTime();
        }

       
       
    }
}
