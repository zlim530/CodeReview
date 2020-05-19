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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            register f2 = new register();
            f2.ShowDialog();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("提示：用户名和密码不能为空！", "警告");
                textBox1.Focus();
                return;
            }
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("用户名不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox1.Focus();
                return;
            }
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("密码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox2.Focus();
                return;
            }
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=OrderSystem;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Users where 用户名='" + textBox1.Text.Trim() + "' and 用户密码='" + textBox2.Text.Trim() + "'", conn);
            SqlDataReader sdr = cmd.ExecuteReader();                    
            sdr.Read();
               if (sdr.HasRows)
            {
                Common.DataBase.userName = textBox1.Text.Trim();
                Common.DataBase.userPassword = textBox2.Text.Trim();
                this.Hide();
               // MessageBox.Show("登录成功!", "提示");
                MainForm m1 = new MainForm();
                m1.ShowDialog();
                // 登录成功后显示主界面 
              
                
            }
               else {
                   MessageBox.Show("用户名或密码错误!", "提示");
                   textBox1.Clear();
                   textBox2.Clear();
                   textBox1.Focus();
               }
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("管理员姓名和密码不能为空！", "警告");
                textBox1.Focus();
                return;
            }
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("管理员姓名不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox1.Focus();
                return;
            }
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("密码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox2.Focus();
                return;
            }
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=OrderSystem;Integrated Security=True");
            conn.Open();
            SqlCommand cmd1 = new SqlCommand("select * from Admin where 姓名='" + textBox1.Text.Trim() + "' and 密码='" + textBox2.Text.Trim() + "'", conn);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            sdr1.Read();
            if (sdr1.HasRows)
            {

                Common.DataBase.userName = textBox1.Text.Trim();
                Common.DataBase.userPassword = textBox2.Text.Trim();
                this.Close();
                //MessageBox.Show("登录成功!", "提示");

                MainForm1 m2 = new MainForm1();
                m2.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("管理员姓名或密码错误!", "提示");
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }
            conn.Close();
           
        }

     
     
       
    }
}
