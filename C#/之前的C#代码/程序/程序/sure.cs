using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace OrderSystem
{
    public partial class sure : Form
    {
        public sure()
        {
            InitializeComponent();
        }
        Common.DataBase db = new Common.DataBase();
        private void insert5()
        {
            string sqlstrr = "delete from DetailMenu ";
            db.RunNonSelect(sqlstrr);
            MessageBox.Show("订单已确认", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MainForm  sa = new MainForm ();
            sa.Owner = this;
            sa.Show();
        }

        private void detailMenuBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.detailMenuBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);

        }

        private void sure_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.DetailMenu”中。您可以根据需要移动或移除它。
          //  this.detailMenuTableAdapter.Fill(this.orderSystemDataSet.DetailMenu);
            //conn = Common.DataBase.getConnection();
            toolStripStatusLabel2.Text = "当前用户：" + Common.DataBase.userName;
            toolStripStatusLabel3.Text = "当前时间：" + DateTime.Now.ToLocalTime();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            insert5();
        }
    }
}
