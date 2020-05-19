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
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=OrderSystem;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Users where 用户名='" + textBox1.Text.Trim() + "'", conn);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            if (sdr.HasRows)
                MessageBox.Show("该用户已注册，请使用其他用户名", "提示");
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || comboBox1.Text.Trim() == "")
                MessageBox.Show("请填写完整信息", "提示");
            else
            {
                sdr.Close();
                string myinsert = "insert into Users(用户ID,用户名,性别,用户密码,联系电话,联系地址) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
                SqlCommand mycom = new SqlCommand(myinsert, conn);           //定义OleDbCommnad对象并连接数据库
                mycom.ExecuteNonQuery();                           //执行插入语句
                conn.Close();                //关闭对象并释放所占内存空间  
                conn.Dispose();
                MessageBox.Show("您已注册成功！");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            login f1 = new login();
            f1.ShowDialog();
            this.Close();
        }
    }
}
