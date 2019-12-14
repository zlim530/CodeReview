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
    public partial class OrderCheck1 : Form
    {
        public OrderCheck1()
        {
            InitializeComponent();
        }
        private SqlConnection conn;
        private SqlDataAdapter adapter;
        private DataTable dt2 = new DataTable();
        private void order1BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.order1BindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);

        }
        private void DisplaySelectedCourse()
        {
            string queryStr = "select * from Order1 where 客户姓名='" + Common.DataBase.userName + "'";
            adapter = new SqlDataAdapter(queryStr, conn);
            if (dt2 != null) dt2.Clear();
            adapter.Fill(dt2);
            order1DataGridView.DataSource = dt2;
        }
        private void OrderCheck1_Load(object sender, EventArgs e)
        {

            // this.order1TableAdapter.Fill(this.orderSystemDataSet.Order1);
            toolStripStatusLabel2.Text = "当前用户："+Common.DataBase.userName;
            toolStripStatusLabel3.Text = "当前时间：" + DateTime.Now.ToLocalTime();
            conn = Common.DataBase.getConnection();
            // private DataSet ds = new DataSet();
            Common.DataBase db = new Common.DataBase();
           // string sql5 = "select * from Order1 where 客户姓名='" + Common.DataBase.userName + "'";
           // db.RunNonSelect(sql5);
            DisplaySelectedCourse();
        }
    }
}
