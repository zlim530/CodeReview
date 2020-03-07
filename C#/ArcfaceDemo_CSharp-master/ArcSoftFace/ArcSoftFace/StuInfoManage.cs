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
            strSQL = "SELECT DISTINCT sex FROM stuInfo;";
            SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
            //DataSet ds2 = db.getDataSet(strSQL, "table");
            da.Fill(ds, "table");
            sex.DataSource = ds.Tables["table"];
            sex.ValueMember = "sex";
            sex.DisplayMember = "sex";

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
                    else if (dr.RowState == System.Data.DataRowState.Deleted)
                    {
                        int id = Convert.ToInt32(dr["id", DataRowVersion.Original].ToString());
                        string name = dr["name", DataRowVersion.Original].ToString();
                        strSQL = @"DELETE FROM [dbo].[StuInfo] WHERE id = '" + id + @"' AND name = '" + name + @"'";

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

        private void dataGridView1Stu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string txtname = txtName.ToString().Trim();
            string txtnumber = txtNumberS.ToString().Trim();
            string txtsex = ComboBoxSex.ToString().Trim();
            string txtcheck = ComboBoxCheck.ToString().Trim();
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
    }


}
