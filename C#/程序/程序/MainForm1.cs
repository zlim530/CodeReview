using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OrderSystem
{
    public partial class MainForm1 : Form
    {
        public MainForm1()
        {
            InitializeComponent();
        }

        private void MainForm1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "当前用户：" + Common.DataBase.userName;
            toolStripStatusLabel3.Text = "当前日期：" + DateTime.Now.ToLocalTime();

        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Chuancai cc = new Chuancai();
            cc.Owner = this;
            cc.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Yuecai yc = new Yuecai();
            yc.Owner = this;
            yc.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Xiangcai xc = new Xiangcai();
            xc.Owner = this;
            xc.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Dongbeicai dbc = new Dongbeicai();
            dbc.Owner = this;
            dbc.Show();
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Common.DataBase.userName != "")
            {
                MessageBox.Show("您已经登录不能再次登录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {

                login log = new login();
                log.ShowDialog();
                this.Hide();
            }
           
        }

        private void 菜单管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
                MenuManage pass = new MenuManage();
                pass.Owner = this;
                pass.Show();
           
        }

        private void 会员管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
                VipManage vip = new VipManage();
                vip.Owner = this;
                vip.Show();
           
          
        }

       
       

     

        private void 用户管理ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
          
                UsersManage pass = new UsersManage();
                pass.Owner = this;
                pass.Show();
           
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
                OrderCheck ord = new OrderCheck();
                ord.Owner = this;
                ord.Show();
          
        }

        private void 销量查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaleManage sa = new SaleManage();
            sa.Owner = this;
            sa.Show();
        }
    }
}
