using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using OrderSystem.Common;
using SpeakVoice;
using System.Speech.Recognition;
namespace OrderSystem
{
    public partial class Order : Form
    {
        string connStr = Properties.Settings.Default.OrderSystemConnectionString;
       private DataTable dt1 = new DataTable();
        private DataTable dt2 = new DataTable();
        private SqlConnection conn;
        private SqlDataAdapter adapter;
        DataTable table = new DataTable();
        Common.DataBase db = new Common.DataBase();
        private DataSet ds = new DataSet();


        public static bool isInitializeSpeechToText = false;

        public Order()
        {
            InitializeComponent();

            conn = new SqlConnection("Data Source=.;Initial Catalog=OrderSystem;Integrated Security=True");
            conn.Open();
            conn = Common.DataBase.getConnection();

            TakeToSepak(SpeakDinner.set);


        }
        private void DisplaySelectedCourse(string str="")
        {
            string queryStr = "";
            if (str == "")
            {
                 queryStr = "select * from DetailMenu";
            }else
            {
                queryStr = "select * from [DetailMenu] where 菜名='" + str + "'";
            }
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
        

        private void Order_Load(object sender, EventArgs e)
        {
            
            //this.menuTableAdapter.Fill(this.orderSystemDataSet.Menu);
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.DetailMenu”中。您可以根据需要移动或移除它。
            this.detailMenuTableAdapter.Fill(this.orderSystemDataSet.DetailMenu);
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.Menu”中。您可以根据需要移动或移除它。
            this.menuTableAdapter.Fill(this.orderSystemDataSet.Menu);
            bindingNavigator1.BindingSource = detailMenuBindingSource;
            conn = Common.DataBase.getConnection();
            //this.detailMenuTableAdapter.Fill(this.orderSystemDataSet.DetailMenu);          
            this.menuTableAdapter.Fill(this.orderSystemDataSet.Menu);
            comboBox1.SelectedIndex = 0;
           // conn.Close();
            //图片PictureBox.Image = menuDataGridView.SelectedRows[0].Cells[7].Value;

            toolStripStatusLabel1.Text = "当前用户："+Common.DataBase.userName;
            toolStripStatusLabel2.Text = "当前时间："+ DateTime.Now.ToLocalTime();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    comboBox2.Items.Clear();
                    break;
                case 1:
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("湘菜");
                    comboBox2.Items.Add("粤菜");
                    comboBox2.Items.Add("川菜");
                    comboBox2.Items.Add("东北菜");
                    comboBox2.SelectedIndex = 0;
                    break;
                case 2:
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("荤菜");
                    comboBox2.Items.Add("素菜");
                    comboBox2.SelectedIndex = 0;
                    break;
                case 3:
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("酒类");
                    comboBox2.Items.Add("饮料");
                    comboBox2.Items.Add("茶水");
                    comboBox2.SelectedIndex = 0;
                    break;
            }
        }

        // 搜索按钮
        private void button1_Click(object sender, EventArgs e)
        {

            string conditionStr = "";
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    conditionStr = "菜系 ='" + comboBox2.Text.Trim() + "'";
                    break;
                case 2:
                    conditionStr = "荤素 ='" + comboBox2.Text.Trim() + "'";
                    break;
                case 3:
                    conditionStr = "菜系 ='" + comboBox2.Text.Trim() + "'";
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
                //double a = 0;
                string bianhao = menuDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                string caiming = menuDataGridView.SelectedRows[0].Cells[1].Value.ToString();
                string danjia = menuDataGridView.SelectedRows[0].Cells[6].Value.ToString();

                string fenshu = 份数NumericUpDown.Value.ToString();
                //double a = double.Parse(menuDataGridView.SelectedRows[0].Cells[6].Value.ToString());
                double a = Convert .ToDouble (danjia);
                double b = double.Parse(份数NumericUpDown.Value.ToString());
                double c = a * b;

                string xiaoji = c.ToString();
                string sqlstr = "insert into DetailMenu(编号,菜名,单价,份数,小计)" + "values('" + bianhao + "','" + caiming + "','" + danjia + "','" + fenshu + "','" + xiaoji + "')";
                //string sqlstr1 = "insert into dOrders(编号,菜名,单价,份数,小计)" + "values('" + bianhao + "','" + caiming + "','" + danjia + "','" + fenshu + "','" + xiaoji + "')";            
                db.RunNonSelect(sqlstr);
                //db.RunNonSelect(sqlstr1);
                DisplaySelectedCourse();
                Sunn();
                //savee();
               
            }
        }
        public void button3_Click(object sender, EventArgs e)
        {
            Check  pass1 = new Check();
            pass1.Owner = this;
            pass1.Show();
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


        // 调用了语音订餐的初始化函数
        public  void TakeToSepak(string[] sets)
        {
            // string[]  set = { "订餐", "下菜", "提交", "确认", "结账", "取消", "搜索", "玫瑰花茶", "客家酿豆腐", "川菜回锅肉", "红烧牛肉", "宫保鸡丁", "凉拌广东菜心", "客家酿豆腐", "鲜虾肠粉", "玫瑰花茶", "川菜回锅肉", "水煮鱼片" };
            if (!isInitializeSpeechToText)
            {
                SpRecognition.Initialize(
                    sets,
                    RecognizeMode.Multiple);

                SpRecognition.GetTextFromSpeech += new EventHandler(GetTextFromSpeech);     // 调用了Order类中的GetTextFromSpeech()方法
                isInitializeSpeechToText = true;

            }
            SpRecognition.Start();

        }
        // 进行语音订餐识别以及数据表具体操作 
        private  void GetTextFromSpeech(object sender, EventArgs e)
        {

            string strTxt = ""; string strOrder = "";
                Order order = new Order();
            var txt = sender as string;

            //string queryStr = "";
            //SqlCommand sqlcmd = new SqlCommand();
            //SqlDataAdapter adp = new SqlDataAdapter(sqlcmd);
            //DataTable dt = new System.Data.DataTable();
            //var restult = 0;
            switch (txt)
            {

                case "订餐":
                    order.Show();
                    break;
                case "下菜":
                    strOrder = "下菜";
                    break;
                case "玫瑰花茶":
    
                    strTxt = "玫瑰花茶";

                    //queryStr = "select * from [DetailMenu] where 菜名='" + strTxt + "'";
                    
                    //sqlcmd.CommandText = queryStr;
                    //sqlcmd.Connection = conn;


                    ////adp.Fill(dt);
                    //restult = sqlcmd.ExecuteNonQuery();
                    //if (restult != -1)
                    //{
                    //    return;
                    //}

                    break;
                case "客家酿豆腐":
                    strTxt = "客家酿豆腐";

                    //queryStr = "select * from [DetailMenu] where 菜名='" + strTxt + "'";

                    //sqlcmd.CommandText = queryStr;
                    //sqlcmd.Connection = conn;


                    //restult = sqlcmd.ExecuteNonQuery();
                    //if (restult != -1)
                    //{
                    //    return;
                    //}
                    DisplaySelectedCourse(strTxt);

                    break;
                case "川菜回锅肉":
                    strTxt = "川菜回锅肉";

                    //queryStr = "select * from [DetailMenu] where 菜名='" + strTxt + "'";

                    //sqlcmd.CommandText = queryStr;
                    //sqlcmd.Connection = conn;


                    //restult = sqlcmd.ExecuteNonQuery();
                    //if (restult != -1)
                    //{
                    //    return;
                    //}
                    DisplaySelectedCourse(strTxt);

                    break;
                case "红烧牛肉":
                    strTxt = "红烧牛肉";

                    //queryStr = "select * from [DetailMenu] where 菜名='" + strTxt + "'";

                    //sqlcmd.CommandText = queryStr;
                    //sqlcmd.Connection = conn;


                    //restult = sqlcmd.ExecuteNonQuery();
                    //if (restult != -1)
                    //{
                    //    return;
                    //}
                    DisplaySelectedCourse(strTxt);

                    break;
                case "宫保鸡丁":
                    strTxt = "宫保鸡丁";

                    //queryStr = "select * from [DetailMenu] where 菜名='" + strTxt + "'";

                    //sqlcmd.CommandText = queryStr;
                    //sqlcmd.Connection = conn;


                    //restult = sqlcmd.ExecuteNonQuery();
                    //if (restult != -1)
                    //{
                    //    return;
                    //}
                    DisplaySelectedCourse(strTxt);

                    break;
                case "凉拌广东菜心":
                    strTxt = "凉拌广东菜心";

                    //queryStr = "select * from [DetailMenu] where 菜名='" + strTxt + "'";

                    //sqlcmd.CommandText = queryStr;
                    //sqlcmd.Connection = conn;


                    //restult = sqlcmd.ExecuteNonQuery();
                    //if (restult != -1)
                    //{
                    //    return;
                    //}
                    DisplaySelectedCourse(strTxt);

                    break;
                case "鲜虾肠粉":
                    strTxt = "鲜虾肠粉";

                    //queryStr = "select * from [DetailMenu] where 菜名='" + strTxt + "'";

                    //sqlcmd.CommandText = queryStr;
                    //sqlcmd.Connection = conn;


                    //restult = sqlcmd.ExecuteNonQuery();
                    //if (restult != -1)
                    //{
                    //    return;
                    //}
                    DisplaySelectedCourse(strTxt);

                    break;
                case "水煮鱼片":
                    strTxt = "水煮鱼片";

                    //queryStr = "select * from [DetailMenu] where 菜名='" + strTxt + "'";

                    //sqlcmd.CommandText = queryStr;
                    //sqlcmd.Connection = conn;


                    //restult = sqlcmd.ExecuteNonQuery();
                    //if (restult != -1)
                    //{
                    //    return;
                    //}
                    DisplaySelectedCourse(strTxt);

                    break;

                case "提交":
                    Check pass1 = new Check();
                    pass1.Show();
                    break;
            }

            DisplaySelectedCourse();

            if (!string.IsNullOrEmpty(strOrder))
            {
                DisplaySelectedCourse();
                Sunn();
                savee();
            }
            else
            {
                string sqlCount = "select * from [DetailMenu] where 菜名='" + strTxt + "'";// "select count(*) from DetailMenu ";
                SqlCommand sqlcnd = new SqlCommand(sqlCount, conn);
                var cnt = sqlcnd.ExecuteScalar() == null ? 0 : (int)sqlcnd.ExecuteScalar();
                if (cnt > 0)
                {
                    return;
                }
                else
                {
                    string sqlstr1 = "select * from [Menu] where 菜名='" + strTxt + "'";
                    SqlDataReader dr = db.getDataReader(sqlstr1);
                    dr.Read();

                    if (dr.HasRows)
                    {

                        string bianhao = dr["编号"].ToString(); 
                        string caiming = dr["菜名"].ToString();
                        string danjia = dr["价格"].ToString(); 

                        string fenshu = 份数NumericUpDown.Value.ToString();
                        double a = Convert.ToDouble(danjia);
                        double b = double.Parse(份数NumericUpDown.Value.ToString());
                        double c = a * b;

                        string xiaoji = c.ToString();
                        string sqlstr = "insert into DetailMenu(编号,菜名,单价,份数,小计)" + "values('" + bianhao + "','" + caiming + "','" + danjia + "','" + fenshu + "','" + xiaoji + "')";
                        db.RunNonSelect(sqlstr);

                    }
                }
            }

          
        }

    }
}
