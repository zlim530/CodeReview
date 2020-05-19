using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace OrderSystem
{
    public partial class liuyan : Form
    {
        String connStr = Properties.Settings.Default.OrderSystemConnectionString;
        SqlConnection conn;
        SqlDataAdapter adapter;
        DataTable table = new DataTable();
        private DataSet ds = new DataSet();
        Common.DataBase db = new Common.DataBase();
        private DataTable dt1 = new DataTable();
        public liuyan()
        {
            InitializeComponent();
        }
        
        private void liuyanBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.liuyanBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);

        }

        private void liuyan_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.Liuyan”中。您可以根据需要移动或移除它。
            this.liuyanTableAdapter.Fill(this.orderSystemDataSet.Liuyan);
            toolStripStatusLabel2.Text =  "当前用户："+Common.DataBase.userName;
            toolStripStatusLabel3.Text = "当前时间：" + DateTime.Now.ToLocalTime();
            conn = Common.DataBase.getConnection();
            星级ComboBox.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            留言RichTextBox.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (留言RichTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("留言不能为空！！", "提示");
            }
            else
            {
                string client = Common.DataBase.userName;
                DateTime time = DateTime.Now;
                string time1 = time.ToString("yyyy-MM-dd HH:mm:ss");
                string liuyan = 留言RichTextBox.Text.Trim();
                string xingji = 星级ComboBox.Text.Trim();
                string sqlstr = "insert into Liuyan(用户名,留言,星级,留言时间)" + "values('" + client + "','" + liuyan + "','" + xingji + "','" + time1 + "')";

                db.RunNonSelect(sqlstr);
                save();
                DisplaySelectedLiuyan();
            }
        }
        private void save()
        {
            try
            {
                this.Validate();
                this.liuyanBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);
                MessageBox.Show("保存成功", "提示");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "保存失败");
            }
        }
        private void DisplaySelectedLiuyan()
        {
            string queryStr = "Select * from Liuyan";
            adapter = new SqlDataAdapter(queryStr, conn);
            if (dt1 != null) dt1.Clear();
            adapter.Fill(dt1);
            liuyanDataGridView.DataSource = dt1;
        }
    }
}
