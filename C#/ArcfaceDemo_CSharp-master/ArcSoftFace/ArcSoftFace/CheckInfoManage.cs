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
    public partial class CheckInfoManage : Form
    {
        public CheckInfoManage()
        {
            InitializeComponent();
        }

        private String FaceLibraryPath = "C://Users//Lim//Desktop//code//人脸库/";

        Bitmap image;
        DateBase db = new DateBase();
        SqlConnection conn;
        DataTable dt;

        // 查找按钮事件
        private void btnCMSearch_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=.;Initial Catalog=FaceSign;Integrated Security=True");
            conn.Open();
            string strSQL = "select id,create_time,update_time,feature,image,stu_info_id,name FROM checkInfo WHERE name LIKE'" + txtCIName.Text.Trim() + "%'"; ;
            if (txtCMNumber.Text.Trim() != "")
            {
                //strSQL = "select * FROM checkInfo WHERE stu_info_id LIKE'" + txtCMNumber.Text.Trim() + "%'";
                String str = "AND stu_info_id LIKE'" + txtCMNumber.Text.Trim() + "%'";
                strSQL += str;
            }
            db.RunNonSelect(strSQL);
            DataSet ds = db.getDataSet(strSQL, "checkInfo");

            dt = ds.Tables["checkInfo"];
            dataGridView1CI.DataSource = ds;
            dataGridView1CI.DataMember = "checkInfo";
            //dataGridView1CI.DataSource = dt;
        }

        // 选择相片按钮事件
        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(openFileDialog1.FileName);
                pictureBox图像.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox图像.Image = image;
            }
        }

        // 保存相片按钮事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable changeDt = dt.GetChanges();
            DateTime dtime = DateTime.Now.ToLocalTime();
            string time = dtime.ToString("yyyy-MM-dd HH:mm:ss");

            int id = Convert.ToInt32(dataGridView1CI.CurrentRow.Cells[0].Value.ToString());

            string strSQL = @"UPDATE [dbo].[CheckInfo] SET [update_time] = '" + time + @"'
                              ,[image] = '" + image + @"'
                              WHERE id = '" + id + @"' ";

            //this.dataGridView1CI.CurrentRow.Cells[4].Value = image;

            //string dbstr = "Data Source=.;Initial Catalog=FaceSign;Integrated Security=True";
            //string strsql = "update ";
            //SqlDataAdapter adapter = new SqlDataAdapter(strsql, dbstr);


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

        private void save()
        {
            try
            {
                this.Validate();
                MessageBox.Show("保存成功");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "保存失败");
            }
        }

        //private void dataGridView1CI_CellClick(object sender, DataGridViewCellEventArgs e)
        //{

            //dataGridView1CI.Rows[e.RowIndex].Cells[4].Value = image;

            //txtNumberS.Text = dataGridView1Stu.CurrentRow.Cells[0].Value.ToString();
            //txtCtime.Text = dataGridView1Stu.CurrentRow.Cells[1].Value.ToString();
            //txtUtime.Text = dataGridView1Stu.CurrentRow.Cells[2].Value.ToString();
            //ComboBoxSex.Text = dataGridView1Stu.CurrentRow.Cells[3].Value.ToString();
            //txtName.Text = dataGridView1Stu.CurrentRow.Cells[4].Value.ToString();
            //ComboBoxCheck.Text = dataGridView1Stu.CurrentRow.Cells[5].Value.ToString();


        //}




    }
}
