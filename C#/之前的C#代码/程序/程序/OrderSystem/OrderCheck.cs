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

        private void OrderCheck_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.Order”中。您可以根据需要移动或移除它。
            
            //OrderReport1.SummaryInfo.ReportTitle = "订单查询";
            toolStripStatusLabel2.Text = "当前登录用户：" + Common.DataBase.userName;
            toolStripStatusLabel3.Text = "当前日期：" + DateTime.Now.ToLocalTime();

        }

        
    }
}
