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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.Admin”中。您可以根据需要移动或移除它。
            this.adminTableAdapter.Fill(this.orderSystemDataSet.Admin);
            toolStripStatusLabel2.Text = "当前登录用户：" + Common.DataBase.userName;
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

        private void 订单查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Common.DataBase.userName != "")
            {
                OrderCheck check = new OrderCheck();
                check.Owner = this;
                check.Show();
            }
            else
            {
                留言ToolStripMenuItem.Enabled = false;
                MessageBox.Show("请先登录或者注册！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login log = new login();
            log.Owner = this;
            log.Show();
           
        }

        private void 留言ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Common.DataBase.userName != "")
            {
                liuyan ly = new liuyan();
                ly.Owner = this;
                ly.Show();
            }
            else
            {
                留言ToolStripMenuItem.Enabled = false;
                MessageBox.Show("请先登录或者注册！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void 订餐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Common.DataBase.userName != "")
            {
                Order order = new Order();
                order.Owner = this;
                order.Show();
            }
            else
            {
                订餐ToolStripMenuItem.Enabled = false;
                MessageBox.Show("请先登录或者注册！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void 结账ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Common.DataBase.userName != "")
            {
                liuyan ly = new liuyan();
                ly.Owner = this;
                ly.Show();
            }
            else
            {
                留言ToolStripMenuItem.Enabled = false;
                MessageBox.Show("请先登录或者注册！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void 系统管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
            if (Common.DataBase.userName !="" )
            {
               留言ToolStripMenuItem.Enabled = true;
            }
            else
            {
                留言ToolStripMenuItem.Enabled = false;
                MessageBox.Show("请先登录或者注册！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void adminBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.adminBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);

        }
    }
}
