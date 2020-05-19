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
    public partial class VipManage : Form
    {
        public VipManage()
        {
            InitializeComponent();
        }

        private void vipBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.vipBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);
            MessageBox.Show("保存成功", "提示");

        }

        private void VipManage_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.Vip”中。您可以根据需要移动或移除它。
            this.vipTableAdapter.Fill(this.orderSystemDataSet.Vip);
            toolStripStatusLabel2.Text = "当前用户：" + Common.DataBase.userName;
            toolStripStatusLabel3.Text = "当前时间：" + DateTime.Now.ToLocalTime();

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (vipDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先单击最左边的空白处，选择要删除的行，" + "按住CTRL或Shift键可同时选择多行");
            }
            else
            {
                if (MessageBox.Show("确定要删除选定的行吗？", "小心", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    for (int i = 0; i < vipDataGridView.SelectedRows.Count; i++)
                    {
                        vipBindingSource.RemoveAt(vipDataGridView.SelectedRows[i].Index);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conditionStr = "";
            if (textBox1.Text.Trim() != "")
            {
                if (conditionStr == "")
                    conditionStr = "会员号 LIKE'" + textBox1.Text.Trim() + "%'";
                else
                    conditionStr += "AND 会员号 LIKE'" + textBox1.Text.Trim() + "%'";
            }
            if (textBox2.Text.Trim() != "")
                conditionStr = "姓名 LIKE'" + textBox2.Text.Trim() + "%'";
            vipBindingSource.Filter = conditionStr;
        }

      
    }
}
