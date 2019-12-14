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
    public partial class Order : Form
    {
        string connStr = Properties.Settings.Default.OrderSystemConnectionString;
       // private DataTable dt1 = new DataTable();
        private DataTable dt2 = new DataTable();
        private SqlConnection conn;
        private SqlDataAdapter adapter;
        DataTable table = new DataTable();
        Common.DataBase db = new Common.DataBase();
        private DataSet ds = new DataSet();

        private void DisplaySelectedCourse()
        {
            string queryStr = "select * from DetailMenu";
            adapter = new SqlDataAdapter(queryStr, conn);
            if (dt2 != null) dt2.Clear();
            adapter.Fill(dt2);
            detailMenuDataGridView.DataSource = dt2;          
        }
        private void savee()
        {
            this.Validate();
            detailMenuBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);
        }

        private void Sunn()
        {
            string sql=String.Format("select sum(小计) from DetailMenu");
             SqlCommand comm=new SqlCommand(sql,conn);
            label6.Text = comm.ExecuteScalar().ToString();
            string sqll = String.Format("select sum(份数) from DetailMenu");
            SqlCommand coml = new SqlCommand(sqll, conn);
            label5.Text = coml.ExecuteScalar().ToString();
        }    
        public Order()
        {
            InitializeComponent();
        }

        private void Order_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.DetailMenu”中。您可以根据需要移动或移除它。
            this.detailMenuTableAdapter.Fill(this.orderSystemDataSet.DetailMenu);
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.Menu”中。您可以根据需要移动或移除它。
            this.menuTableAdapter.Fill(this.orderSystemDataSet.Menu);
            bindingNavigator1.BindingSource = detailMenuBindingSource;
            conn = Common.DataBase.getConnection();
            this.detailMenuTableAdapter.Fill(this.orderSystemDataSet.DetailMenu);          
            this.menuTableAdapter.Fill(this.orderSystemDataSet.Menu);
            comboBox1.SelectedIndex = 0;
           // conn.Close();
            //图片PictureBox.Image = menuDataGridView.SelectedRows[0].Cells[7].Value;

            toolStripStatusLabel1.Text = "当前时间：" + DateTime.Now.ToShortDateString();
            toolStripStatusLabel2.Text = "当前用户：" + Common.DataBase.userName;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("湘菜");
                    comboBox2.Items.Add("粤菜");
                    comboBox2.Items.Add("川菜");
                    comboBox2.Items.Add("东北菜");
                    comboBox2.SelectedIndex = 0;
                    break;
                case 1:
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("荤菜");
                    comboBox2.Items.Add("素菜");
                    comboBox2.SelectedIndex = 0;
                    break;
                case 2:
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("酒类");
                    comboBox2.Items.Add("饮料");
                    comboBox2.Items.Add("茶水");
                    comboBox2.SelectedIndex = 0;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string conditionStr = "";
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    conditionStr = "菜系 ='" + comboBox2.Text.Trim() + "'";
                    break;
                case 1:
                    conditionStr = "荤素 ='" + comboBox2.Text.Trim() + "'";
                    break;
                case 2:
                    conditionStr = "酒水 ='" + comboBox2.Text.Trim() + "'";
                    break;
            }
            menuBindingSource.Filter = conditionStr;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (menuDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先单击最左边的空白处，选择要点菜单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string bianhao1 = menuDataGridView.SelectedRows[0].Cells[0].Value.ToString();
               string sqlstr1="select * from DetailMenu where 编号='" + bianhao1 + "'";
            SqlDataReader dr=db.getDataReader (sqlstr1 );
            dr.Read ();
            if (dr.HasRows)
            {
                MessageBox.Show("已选该菜，请重新选择！或通过份数添加！","提示");
                return;
            }
            else
            {
                string bianhao = menuDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                string caiming = menuDataGridView.SelectedRows[0].Cells[1].Value.ToString();
                string danjia = menuDataGridView.SelectedRows[0].Cells[6].Value.ToString();
                string fenshu = 份数NumericUpDown.Value.ToString();
                double a = double.Parse(menuDataGridView.SelectedRows[0].Cells[6].Value.ToString());
                double b = double.Parse(份数NumericUpDown.Value.ToString());
                double c = a * b;
                string xiaoji = c.ToString();
                string sqlstr = "insert into DetailMenu(编号,菜名,单价,小计,份数)" + "values('" + bianhao + "','" + caiming + "','" + danjia + "','" + xiaoji + "','" + fenshu + "')";
                db.RunNonSelect(sqlstr);
                DisplaySelectedCourse();
                Sunn();
                savee();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            detailMenuDataGridView.EndEdit();
            if (detailMenuDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先单击最左边的空白处，选择要删除的行，" + "按住CTRL或Shift键可同时选择多行");
            }
            else
            {
                if (MessageBox.Show("确定要删除选定的行吗？", "小心", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string bianhao2 =detailMenuDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                    string sqlstr2 = "delete from DetailMenu where 编号='" + bianhao2 + "'";
                    db.RunNonSelect(sqlstr2);
                    savee();
                    DisplaySelectedCourse();
                    Sunn ();
                }
            }
           
        }
   
    }
}
