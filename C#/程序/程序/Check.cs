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
    public partial class Check : Form
    {
        private SqlConnection conn;

        public  bool isInitializeSpeechToText = false;
        private DataSet ds = new DataSet();
        public Check()
        {
            InitializeComponent();
            TakeToSepak(SpeakDinner.set);

        }
        // public static string fenshu;
        public static string userName = "";
        private void insert()
        {
            string sql5 = "insert into Vip (姓名) values ('"+Common.DataBase.userName+"')";

            db.RunNonSelect(sql5);
            this.Validate();
            this.vipBindingSource.EndEdit();
                        
        }
        private void savee()
        {
            this.Validate();
            dOrdersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);
        }
        private void insert1()
        {

            //double sb = double.Parse(label6.Text);
            //int fenshu = int.Parse(label4.Text);
            string m1=textBox3.Text.Trim();
            string m2 = textBox1.Text.Trim();
            string m3 = label4.Text.Trim();
            string m4 = textBox2.Text.Trim();
            string m5 = label6.Text.Trim();
            string sql6 = "insert into Order1 (送餐时间,送餐地点,菜品数量,客户姓名,客户联系方式,总价) values ('" + m1 + "','" + m2 + "','" + m3 + "','" + Common.DataBase.userName + "','" + m4 + "','" + m5 + "')";

            db.RunNonSelect(sql6);
            this.Validate();
            this.order1BindingSource.EndEdit();

        }
        private void insert2() 
        {
            //string sqlstr = "insert into dOrders(编号,菜名,单价,份数,小计) select 编号,菜名,单价,份数,小计 from DetailMenu";
            string sqlstr = "insert into dOrders(菜名,单价,份数,小计) select 菜名,单价,份数,小计 from DetailMenu";
            db.RunNonSelect(sqlstr);
            savee();
        }
        private void insert3()
        {
            //string sqlstr1 = "insert into sales(编号,菜名,销量,价格) select 编号,菜名,sum(份数),单价 from dOrders group by 菜名,编号,单价";
            string sqlstr1 = "insert into sales(菜名,销量,价格) select 菜名,sum(份数),单价 from dOrders group by 菜名,单价";
            db.RunNonSelect(sqlstr1);
            this.Validate();
            salesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);
        }
        private void insert4()
        {
            //string sqlstrr = "delete from sales where 菜名 in (select 菜名 from DetailMenu where 菜名=DetailMenu.菜名)";
            string sqlstrr = "delete from sales";
            db.RunNonSelect(sqlstrr);
           
        }
       
        Common.DataBase db = new Common.DataBase();

        private void Check_Load(object sender, EventArgs e)
        {
            
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.sales”中。您可以根据需要移动或移除它。
            this.salesTableAdapter.Fill(this.orderSystemDataSet.sales);
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.Vip”中。您可以根据需要移动或移除它。
            this.vipTableAdapter.Fill(this.orderSystemDataSet.Vip);
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.Users”中。您可以根据需要移动或移除它。
            // this.usersTableAdapter.Fill(this.orderSystemDataSet.Users);
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.Order1”中。您可以根据需要移动或移除它。
            this.order1TableAdapter.Fill(this.orderSystemDataSet.Order1);
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.DetailMenu”中。您可以根据需要移动或移除它。
            
           // this.detailMenuDataGridView.DataSource = this.orderSystemDataSet.DetailMenu;
            //this.detailMenuDataGridView.Update();
            this.detailMenuTableAdapter.Fill(this.orderSystemDataSet.DetailMenu);
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.DetailMenu”中。您可以根据需要移动或移除它。
            toolStripStatusLabel2.Text = "当前用户：" + Common.DataBase.userName;
            toolStripStatusLabel3.Text = "当前时间：" + DateTime.Now.ToLocalTime();

            conn = Common.DataBase.getConnection();


            string sql = String.Format("select sum(份数) from DetailMenu");
            SqlCommand comm = new SqlCommand(sql, conn);
            label4.Text = comm.ExecuteScalar().ToString();


            string sql1 = String.Format("select sum(小计) from DetailMenu");
            SqlCommand comm1 = new SqlCommand(sql1, conn);

            label6.Text = comm1.ExecuteScalar().ToString();
            double sb = 0;
            if (!string.IsNullOrEmpty(label6.Text))
            {
                sb = double.Parse(label6.Text);
            }
            else { }



            string sql7 = String.Format("select * from Order1 where 客户姓名='" + Common.DataBase.userName + "'");
            SqlCommand comm7 = new SqlCommand(sql7, conn);
            SqlDataReader sdr1 = comm7.ExecuteReader();
            sdr1.Read();
            if (sdr1.HasRows)
            {
                sdr1.Close();
                string sql8 = String.Format("select sum(总价) from Order1 where 客户姓名='" + Common.DataBase.userName + "'");
                SqlCommand comm8 = new SqlCommand(sql8, conn);
                label9.Text = comm8.ExecuteScalar().ToString();
                double sb1 = double.Parse(label9.Text);
                sb1 = sb1 + sb;
                string sb2 = sb1.ToString();
                label9.Text = sb2;
               
            }
            else
            {
                label9.Text = label6.Text;
            }
            

           /* string sql2 = String.Format("select sum(总价) from Order1 group by 客户姓名");
            SqlCommand comm2 = new SqlCommand(sql2, conn);
            label9.Text = comm2.ExecuteScalar().ToString();
            double sb1 = double.Parse(label9.Text);
            sb1 = sb1+sb;
            string sb2 = sb1.ToString();
            label9.Text = sb2;*/

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql4 = String.Format("select 用户类型 from Users where 用户名='" + Common.DataBase.userName + "'");
            SqlCommand comm4 = new SqlCommand(sql4, conn);
          
            string sb3 = comm4.ExecuteScalar().ToString();
            
            DialogResult dr,dr1;
            if (sb3 == "非会员")
            {
                if (int.Parse(label9.Text) > 100)
                {

                    dr = MessageBox.Show("累计消费超过100，本店会员9折优惠！！是否注册成为会员！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        string sql3 = "update Users set 用户类型='会员' where 用户名='" + Common.DataBase.userName + "'";

                        db.RunNonSelect(sql3);

                        this.Validate();
                        this.usersBindingSource.EndEdit();
                        insert();
                        string sql6 = String.Format("select 会员号 from Vip where 姓名='" + Common.DataBase.userName + "'");
                        SqlCommand comm6 = new SqlCommand(sql6, conn);
                        string sb6 = comm6.ExecuteScalar().ToString();
                        MessageBox.Show("恭喜您成为了本店会员，您的会员号为：" + sb6);
                        double sum = double.Parse(label6.Text);
                        sum = sum * 0.9;

                        MessageBox.Show("您的本次消费额为：" + sum, "显示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    }
                    else
                    {
                        double sum = double.Parse(label6.Text);

                        MessageBox.Show("您的本次消费额为：" + sum, "显示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        insert1();
                        dr1 = MessageBox.Show("结账成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (dr1 == DialogResult.OK)
                        {
                            MainForm chec = new MainForm();
                            chec.ShowDialog();
                            this.Close();

                        }
                        //conn.Close();
                    }
                  
                }
                else
                {
                    double sum = double.Parse(label6.Text);
                    
                    MessageBox.Show("您的本次消费额为：" + sum, "显示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }


            }
            else
            {
                double sum = 0;
                if (!string.IsNullOrEmpty(label6.Text))
                {
                     sum = double.Parse(label6.Text);
                    sum = sum * 0.9;
                }
                MessageBox.Show("您的本次消费额为：" + sum, "显示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            insert1();
            insert2();
            insert4();
           insert3();
          

            dr1=MessageBox.Show("结账成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (dr1 == DialogResult.OK)
            {
                MainForm chec = new MainForm();
                chec.ShowDialog();
                this.Close();

            }
            // insert5();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.Show();
            this.Hide();

        }
        // 调用了语音订餐初始化函数
        public void TakeToSepak(string[] sets)
        {
            // string[]  set = { "订餐", "下菜", "提交", "确认", "结账", "取消", "搜索", "玫瑰花茶", "客家酿豆腐", "川菜回锅肉", "红烧牛肉", "宫保鸡丁", "凉拌广东菜心", "客家酿豆腐", "鲜虾肠粉", "玫瑰花茶", "川菜回锅肉", "水煮鱼片" };
            if (!isInitializeSpeechToText)
            {
                SpRecognition.Initialize(
                    sets,
                    RecognizeMode.Multiple);

                SpRecognition.GetTextFromSpeech += new EventHandler(GetTextFromSpeech);
                isInitializeSpeechToText = true;

            }
            SpRecognition.Start();

        }

        private void GetTextFromSpeech(object sender, EventArgs e)
        {

            string strTxt = "";
            Order order = new Order();
            var txt = sender as string;
            switch (txt)
            {

                case "订餐":
                    order.Show();
                    break;
                case "下菜":

                    break;
                case "玫瑰花茶":

                    strTxt = "玫瑰花茶";
                    break;
                case "客家酿豆腐":
                    strTxt = "客家酿豆腐";
                    break;
                case "川菜回锅肉":
                    strTxt = "川菜回锅肉";

                    break;
                case "红烧牛肉":
                    strTxt = "红烧牛肉";
                    break;
                case "宫保鸡丁":
                    strTxt = "宫保鸡丁";

                    break;
                case "凉拌广东菜心":
                    strTxt = "凉拌广东菜心";
                    break;
                case "鲜虾肠粉":
                    strTxt = "鲜虾肠粉";
                    break;
                case "水煮鱼片":
                    strTxt = "水煮鱼片";
                    break;

                case "结账":
                    button1_Click(null, null);
                    break;
            }

           
        }



    }

      
    }

