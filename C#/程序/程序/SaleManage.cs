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
    public partial class SaleManage : Form
    {
        private SqlConnection conn;
        private SqlDataAdapter adapter;
        private DataTable dt2 = new DataTable();
        public SaleManage()
        {
            InitializeComponent();
        }
       
        private void salesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.salesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);

        }

        private void SaleManage_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.sales”中。您可以根据需要移动或移除它。
            this.salesTableAdapter.Fill(this.orderSystemDataSet.sales);
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.sales”中。您可以根据需要移动或移除它。
            this.salesTableAdapter.Fill(this.orderSystemDataSet.sales);
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.sales”中。您可以根据需要移动或移除它。
            this.salesTableAdapter.Fill(this.orderSystemDataSet.sales);
            conn = Common.DataBase.getConnection();
            Common.DataBase db = new Common.DataBase();
            toolStripStatusLabel2.Text = "当前用户：" + Common.DataBase.userName;
            toolStripStatusLabel3.Text = "当前时间：" + DateTime.Now.ToLocalTime();
        }

        private void salesBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.salesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string conditionStr = "";

            if (textBox1.Text.Trim() != "")
                conditionStr = "编号 ='" + textBox1.Text.Trim() + "'";
            if (textBox2.Text.Trim() != "")
            {
                    if (conditionStr != "")
                        conditionStr += " AND 菜名 LIKE'" + textBox2.Text.Trim() +"%'";
                    else
                        conditionStr = "菜名 LIKE'" + textBox2.Text.Trim() +"%'";
                }
             salesBindingSource.Filter=conditionStr;
            }

        }
    }