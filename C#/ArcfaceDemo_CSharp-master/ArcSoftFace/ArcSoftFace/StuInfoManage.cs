using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace
{
    public partial class StuInfoManage : Form
    {
        public StuInfoManage()
        {
            InitializeComponent();
        }
        
        DateBase db = new DateBase();
        SqlConnection conn;
        DataTable dt;

        // 查找按钮事件
        private void btnSearch_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=.;Initial Catalog=FaceSign;Integrated Security=True");
            conn.Open();
            string strSQL = "select * FROM stuInfo WHERE name LIKE'" + txtSearch.Text.Trim() + "%'";
            if ( txtNumber.Text.Trim() != "") {
                String str = "AND id LIKE'" + txtNumber.Text.Trim() + "%'";
                strSQL += str;
            }
            db.RunNonSelect(strSQL);
            DataSet ds = db.getDataSet(strSQL, "stuInfo");

            // 绑定性别
            //strSQL = "SELECT DISTINCT sex FROM stuInfo;";
            //SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
            ////DataSet ds2 = db.getDataSet(strSQL, "table");
            //da.Fill(ds, "table");
            //sex.DataSource = ds.Tables["table"];
            //sex.ValueMember = "sex";
            //sex.DisplayMember = "sex";

            dt = ds.Tables["stuInfo"];
            dataGridView1Stu.DataSource = ds;
            dataGridView1Stu.DataMember = "stuInfo";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable changeDt = dt.GetChanges();
            DateTime dtime = DateTime.Now.ToLocalTime();
            string time = dtime.ToString("yyyy-MM-dd HH:mm:ss");

            if (changeDt == null){
                MessageBox.Show("没有执行任何操作.");
            }
            else {
                foreach (DataRow dr in changeDt.Rows)
                {
                    string strSQL = string.Empty;
                    if (dr.RowState == System.Data.DataRowState.Added)
                    {
                        strSQL = @"INSERT INTO [dbo].[StuInfo]([id],[create_time],[update_time],[sex],[name],[is_checked])
                             VALUES('" + Convert.ToInt32(dr["id"]) + @"'
                                   ,'" + time + @"'
                                   ,'" + time + @"'
                                   ,'" + dr["sex"].ToString() + @"'
                                   ,'" + dr["name"].ToString() + @"'
                                   ,'" + dr["is_checked"].ToString() + @"')";

                    }
                    else if (dr.RowState == System.Data.DataRowState.Modified)
                    {
                        strSQL = @"UPDATE [dbo].[StuInfo] SET [update_time] = '" + time + @"'
                              ,[sex] = '" + dr["sex"].ToString() + @"'
                              ,[name] = '" + dr["name"].ToString() + @"'
                              ,[is_checked] = '" + dr["is_checked"].ToString() + @"'
                              WHERE id = '" + Convert.ToInt32(dr["id"]) + @"' ";
                    }

                    SqlCommand comm = new SqlCommand(strSQL, conn);
                    try
                    {
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception o)
                    {
                        MessageBox.Show(o.Message, "操作失败。");
                    }
                    save();
                    FindAll();
                }
            }

            

        }

        private void save() {
            try {
                this.Validate();
                MessageBox.Show("保存成功");
            }
            catch (Exception e) {
                MessageBox.Show(e.Message,"保存失败");
            }
        }

        // 单击dataGridView1Stu显示当前行的数据
        private void dataGridView1Stu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //string txtname = txtName.ToString().Trim();
            //string txtnumber = txtNumberS.ToString().Trim();
            //string txtsex = ComboBoxSex.ToString().Trim();
            //string txtcheck = ComboBoxCheck.ToString().Trim();
            txtNumberS.Text = dataGridView1Stu.CurrentRow.Cells[0].Value.ToString();
            txtCtime.Text = dataGridView1Stu.CurrentRow.Cells[1].Value.ToString();
            txtUtime.Text = dataGridView1Stu.CurrentRow.Cells[2].Value.ToString();
            ComboBoxSex.Text = dataGridView1Stu.CurrentRow.Cells[3].Value.ToString();
            txtName.Text = dataGridView1Stu.CurrentRow.Cells[4].Value.ToString();
            ComboBoxCheck.Text = dataGridView1Stu.CurrentRow.Cells[5].Value.ToString();
            //txtName.Text = dataGridView1Stu[e.ColumnIndex, e.RowIndex].Value.ToString();
            //txtNumberS.Text = dataGridView1Stu[e.ColumnIndex, e.RowIndex].Value.ToString();
            //if (txtname.Length != 0 && txtnumber.Length != 0 && txtsex.Length != 0 && txtcheck.Length != 0)
            //{
            //    string sqlstr1 = @"INSERT INTO [dbo].[StuInfo]([id],[create_time],[update_time],[sex],[name],[is_checked])
            //                 VALUES('" + Convert.ToInt32(txtnumber) + @"'
            //                       ,'" + time + @"'
            //                       ,'" + time + @"'
            //                       ,'" + txtsex + @"'
            //                       ,'" + txtname + @"'
            //                       ,'" + txtcheck + @"')";
            //    SqlCommand comm = new SqlCommand(sqlstr1, conn);
            //    comm.ExecuteNonQuery();
            //    save();
            //}
            //if (txtname.Length == 0 && txtnumber.Length == 0 && txtsex.Length == 0 && txtcheck.Length == 0 && changeDt != null)
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            CheckInfoManage cm = new CheckInfoManage();
            cm.Owner = this;
            cm.Show();
        }

        // 删除行按钮事件
        private void btnDeleted_Click(object sender, EventArgs e)
        {
            DialogResult dr =  MessageBox.Show("您真的要删除吗？","系统提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.No) {
                return;
            }

            int id = (int)dataGridView1Stu.CurrentRow.Cells[0].Value;

            string dbstr = "Data Source=.;Initial Catalog=FaceSign;Integrated Security=True";
            conn = new SqlConnection(dbstr);
            conn.Open();

            string sql = "delete from stuInfo where id = @id";
            SqlParameter sp = new SqlParameter("@id",id);

            SqlCommand cmd = new SqlCommand(sql,conn);
            cmd.Parameters.Add(sp);

            int r = cmd.ExecuteNonQuery();
            if (r == 1)
            {
                MessageBox.Show("删除成功");
            }
            else {
                MessageBox.Show("删除失败");
            }
            FindAll();
            //conn.Close();

        }

        private void FindAll() {
            conn = new SqlConnection("Data Source=.;Initial Catalog=FaceSign;Integrated Security=True");
            conn.Open();
            string strSQL = "select * FROM stuInfo";
            
            db.RunNonSelect(strSQL);
            DataSet ds = db.getDataSet(strSQL, "stuInfo");

            dt = ds.Tables["stuInfo"];
            dataGridView1Stu.DataSource = ds;
            dataGridView1Stu.DataMember = "stuInfo";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dt == null)
            {
                // 没有绑定数据的时候添加行
                dataGridView1Stu.Rows.Add();
                int rc = dataGridView1Stu.Rows.Count - 1;
                dataGridView1Stu.Rows[rc].HeaderCell.Value = "N";
            }
            else { 
                DataRow dr = dt.NewRow();
                int index = dataGridView1Stu.RowCount == 0 ? 0 : dataGridView1Stu.CurrentRow.Index + 1;
                dt.Rows.InsertAt(dr,index);
                dataGridView1Stu.Rows[index].HeaderCell.Value = "N";
            }
        }

        // 重置签到信息
        private void btnReset_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=.;Initial Catalog=FaceSign;Integrated Security=True");
            conn.Open();

            string is_checked = "否";
            string sql = "update stuInfo set is_checked = @is_checked";
            SqlParameter ps = new SqlParameter("@is_checked", is_checked);

            SqlCommand cmd = new SqlCommand(sql,conn);

            cmd.Parameters.Add(ps);
            int r = cmd.ExecuteNonQuery();
            if (r != 0)
            {
                MessageBox.Show("重置成功");
            }
            else {
                MessageBox.Show("重置失败");    
            }
            FindAll();

        }



    }


}
