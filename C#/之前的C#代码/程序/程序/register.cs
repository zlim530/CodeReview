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


        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=OrderSystem;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Users where 用户名='" + textBox2.Text.Trim() + "'", conn);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            if (sdr.HasRows)
                MessageBox.Show("该用户已注册，请使用其他用户名", "提示");
            if (textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || comboBox1.Text.Trim() == "")
                MessageBox.Show("请填写完整信息", "提示");
            else
            {
                sdr.Close();
                string myinsert = "insert into Users(用户名,性别,用户密码,用户类型,联系电话,联系地址) values ('" + textBox2.Text + "','" + comboBox1.Text + "','" + textBox3.Text + "','"+"非会员"+"','" + textBox4.Text + "','" + textBox5.Text + "')";
                SqlCommand mycom = new SqlCommand(myinsert, conn);           //定义OleDbCommnad对象并连接数据库
                mycom.ExecuteNonQuery();                           //执行插入语句
                conn.Close();                //关闭对象并释放所占内存空间  
                conn.Dispose();
                MessageBox.Show("您已注册成功！");
                login login = new login();
                login.Show();
                this.Hide();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = 0;
            textBox4.Clear();
            textBox3.Clear();
            textBox5.Clear();
            //textBox1.Focus();
            MainForm login = new MainForm();
            login.Show();
            this.Hide();


        }

        private void register_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            textBox2.Focus();
        }
    }
}
