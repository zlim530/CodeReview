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
    public partial class OrderCheck : Form
    {
        public OrderCheck()
        {
            InitializeComponent();
        }

        private void order1BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.order1BindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);

        }

        private void OrderCheck_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.Order1”中。您可以根据需要移动或移除它。
            this.order1TableAdapter.Fill(this.orderSystemDataSet.Order1);
            toolStripStatusLabel2.Text = "当前用户：" + Common.DataBase.userName;
            toolStripStatusLabel3.Text = "当前时间：" + DateTime.Now.ToLocalTime();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conditionStr = "";
           
            if (textBox2.Text.Trim() != "")
                if (conditionStr=="")
                conditionStr = "客户姓名 LIKE'" + textBox2.Text.Trim() + "%'";
               
            order1BindingSource.Filter = conditionStr;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sure  saa = new sure ();
            saa.Owner = this;
            saa.Show();
        }
    }
}
