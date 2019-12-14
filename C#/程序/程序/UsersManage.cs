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
    public partial class UsersManage : Form
    {
        public UsersManage()
        {
            InitializeComponent();
        }
        Common.DataBase db = new Common.DataBase();
        private void usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            string m1 = 用户名TextBox.Text.Trim();
            string m2 = 性别ComboBox.Text.Trim();
            string m3 = 用户密码TextBox.Text.Trim();
            string m4 = 用户类型ComboBox.Text.Trim();
            string m5 = 联系电话TextBox.Text.Trim();
            string m6 = 联系地址TextBox.Text.Trim();
            
            string sqlstr1 = "insert into Users (用户名,性别,用户密码,用户类型,联系电话,联系地址) values ('" + m1 + "','" + m2 + "','" + m3 + "','" + m4 +"','" + m5 + "','" + m6 + "')";

            db.RunNonSelect(sqlstr1);
            this.Validate();
            usersBindingSource.EndEdit();



            //string sqlstr1 = "insert into Users(用户名,性别,用户密码,用户类型,联系电话,联系地址) values (用户名,性别,用户密码,用户类型,联系电话,联系地址)";
            //db.RunNonSelect(sqlstr1);
            //this.Validate();
            //usersBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);




            //this.Validate();
            //this.usersBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);
            MessageBox.Show("保存成功", "提示");


        }

        private void UsersManage_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.Users”中。您可以根据需要移动或移除它。
            this.usersTableAdapter.Fill(this.orderSystemDataSet.Users);
            toolStripStatusLabel2.Text = "当前用户：" + Common.DataBase.userName;
            toolStripStatusLabel3.Text = "当前时间：" + DateTime.Now.ToLocalTime();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conditionStr = "";
           
            if (textBox2.Text.Trim() != "")
                conditionStr = "用户名 LIKE'" + textBox2.Text.Trim() + "%'";
            if (comboBox1.Text.Trim() != "")
            {
                string str = comboBox1.Text.Trim();
                string[] lxStr=str.Split(' ');
                if(lxStr[0]!="")
                {
                 if(conditionStr!="")
                     conditionStr+=" AND 用户类型'" + lxStr[0]+"'";
                 else
                     conditionStr="用户类型='"+lxStr[0]+"'";
                }

            }

            
           usersBindingSource.Filter = conditionStr;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先单击最左边的空白处，选择要删除的行，" + "按住CTRL或Shift键可同时选择多行");
            }
            else
            {
                if (MessageBox.Show("确定要删除选定的行吗？", "小心", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    for (int i = 0; i < usersDataGridView.SelectedRows.Count; i++)
                    {
                        usersBindingSource.RemoveAt(usersDataGridView.SelectedRows[i].Index);
                    }
                }
            }
        }
    }
}
