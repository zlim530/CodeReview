using OrderSystem.Common;
using SpeakVoice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Windows.Forms;

namespace OrderSystem
{
    public partial class MainForm : Form
    {

        private bool isInitializeSpeechToText = false;

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
            toolStripStatusLabel2.Text = "当前用户：" + Common.DataBase.userName;
            toolStripStatusLabel3.Text = "当前时间：" + DateTime.Now.ToLocalTime();
           if (Common.DataBase.userName == "")
            {
                系统管理ToolStripMenuItem.Enabled = false;
                菜单管理ToolStripMenuItem.Enabled = false;
                会员管理ToolStripMenuItem.Enabled = false;
                用户管理ToolStripMenuItem.Enabled = false;
                订餐ToolStripMenuItem.Enabled = false;
                留言ToolStripMenuItem.Enabled = false;
                修改密码ToolStripMenuItem.Enabled = false;
                订单查询ToolStripMenuItem.Enabled = false;
                销量查询ToolStripMenuItem.Enabled = false;

            }

            if (Common.DataBase.userName != "")
            {
            }
         
           
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

      

        private void button1_Click(object sender, EventArgs e)
        {

            if (Common.DataBase.userName != "")
            {
                MessageBox.Show("您已经登录不能再次登录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                this.Hide();
                login log = new login();
                log.ShowDialog();
               
            }
           
        }

        private void 留言ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Common.DataBase.userName != "")
            {
                liuyan ly = new liuyan();
                ly.Owner = this;
                ly.Show();
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
           
          
        }

      

        private void 系统管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

           
            if (Common.DataBase.userName !="")
            {
                MessageBox.Show("您不是管理员，没有权限进入系统管理！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
       
        }

        private void adminBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.adminBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);

        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (Common.DataBase.userName != "")
            { 
                 repass pass = new repass();
                pass.Owner = this;
                pass.Show();
            }
         
        }

        private void 菜单管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (Common.DataBase.userName != "")
            {
                MessageBox.Show("您不是管理员，没有权限进入系统管理！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      
            }
        }

        private void 会员管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Common.DataBase.userName != "")
            {
                MessageBox.Show("您不是管理员，没有权限进入系统管理！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
        }

       

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (Common.DataBase.userName != "")
            {
                MessageBox.Show("您不是管理员，没有权限进入系统管理！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
           
        }

        private void 订单查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Common.DataBase.userName != "")
            {
                OrderCheck1 or = new OrderCheck1();
                or.Owner = this;
                or.Show();
            }
         
        }

        private void 销量查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Common.DataBase.userName != "")
            {
                SaleManage  che1 = new SaleManage ();
                che1.Owner = this;
                che1.Show();
            }

       
        }

        // 语音订餐按钮事件
        private void 语音叫餐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Common.DataBase.userName != "")
            {
                String[] sets = { "订餐", "下菜", "提交", "确认", "结账", "取消", "搜索", "玫瑰花茶", "客家酿豆腐", "川菜回锅肉", "红烧牛肉", "宫保鸡丁", "凉拌广东菜心", "客家酿豆腐", "鲜虾肠粉", "玫瑰花茶", "川菜回锅肉", "水煮鱼片" };
                SpeakDinner.TakeToSepak(sets);
                语音叫餐ToolStripMenuItem.Enabled = false;
            }
        }




        private void GetTextFromSpeech
        (object sender, EventArgs e)
        {

            Order order = new Order();
            var txt = sender as string;
            switch (txt)
            {

                case "订餐":
                order.Owner = this;
                order.Show();
                    break;
                case "下菜":

                    break;
                case "提交":
                    order.button3_Click(null, null);
                    break;
            }

            // new SearchResultWindow(textBox1.Text);
        }

      
    }
}
