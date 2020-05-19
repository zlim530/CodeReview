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
    public partial class Check : Form
    {
        private SqlConnection conn;
        //private SqlDataAdapter adapter;
        private DataSet ds = new DataSet();
        public Check()
        {
            InitializeComponent();
        }
        public static string fenshu;
        public static string userName = "";
        private void insert()
        {
            string sql5 = "insert into Vip (姓名) values ('"+Common.DataBase.userName+"')";

            db.RunNonSelect(sql5);
            this.Validate();
            this.vipBindingSource.EndEdit();
                        
        }
        private void insert1()
        {


            string sql6 = "insert into Order1 (送餐时间,送餐地点,菜品数量,客户姓名,客户联系方式,总价) values ('" + textBox3 + "','" + textBox1 + "','" + fenshu + "','" + Common.DataBase.userName + "','" + textBox2 + "','" + label6 + "')";

            db.RunNonSelect(sql6);
            this.Validate();
            this.order1BindingSource.EndEdit();

        }
        Common.DataBase db = new Common.DataBase();
        
        private void Check_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.Vip”中。您可以根据需要移动或移除它。
            this.vipTableAdapter.Fill(this.orderSystemDataSet.Vip);
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.Users”中。您可以根据需要移动或移除它。
            this.usersTableAdapter.Fill(this.orderSystemDataSet.Users);
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.Order1”中。您可以根据需要移动或移除它。
            this.order1TableAdapter.Fill(this.orderSystemDataSet.Order1);
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.DetailMenu”中。您可以根据需要移动或移除它。
            this.detailMenuTableAdapter.Fill(this.orderSystemDataSet.DetailMenu);
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.DetailMenu”中。您可以根据需要移动或移除它。
            this.detailMenuTableAdapter.Fill(this.orderSystemDataSet.DetailMenu);
            // TODO: 这行代码将数据加载到表“orderSystemDataSet1.Vip”中。您可以根据需要移动或移除它。
           
            toolStripStatusLabel2.Text = "当前用户：" + Common.DataBase.userName;
            toolStripStatusLabel3.Text = "当前时间：" + DateTime.Now.ToLocalTime();
           
            conn = Common.DataBase.getConnection();

           
            string sql = String.Format("select sum(份数) from DetailMenu");
            SqlCommand comm = new SqlCommand(sql, conn);
            label4.Text = comm.ExecuteScalar().ToString();
            int fenshu = int.Parse(label4.Text);

            string sql1 = String.Format("select sum(单价) from DetailMenu");
            SqlCommand comm1 = new SqlCommand(sql1, conn);
            label6.Text = comm1.ExecuteScalar().ToString();
            double sb = double.Parse(label6.Text);
            

            string sql2 = String.Format("select sum(总价) from Order1 group by 客户姓名");
            SqlCommand comm2 = new SqlCommand(sql2, conn);
            label9.Text = comm2.ExecuteScalar().ToString();
            double sb1 = double.Parse(label9.Text);
            sb1 = sb1+sb;
            string sb2 = sb1.ToString();
            label9.Text = sb2;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql4 = String.Format("select 用户类型 from Users where 用户名='" + Common.DataBase.userName + "'");
            SqlCommand comm4 = new SqlCommand(sql4, conn);
            string sb3 = comm4.ExecuteScalar().ToString();
            //textBox3.Text = comm4.ExecuteScalar().ToString();
            
            DialogResult dr;
            if (sb3 == "非会员")
            {
                if (int.Parse(label9.Text) > 100)
                {

                    dr = MessageBox.Show("累计消费超过100，是否注册成为会员！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        MessageBox.Show("恭喜您成为了本店会员，您的会员号为："+sb6);
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
                double sum = double.Parse(label6.Text);
                sum = sum * 0.9;

                MessageBox.Show("您的本次消费额为：" + sum, "显示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            insert1();
            MessageBox.Show("结账成功");

        }


      }

      
    }

