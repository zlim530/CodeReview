using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
        DataSet ds;

        private void test(){
            DataSet set = new DataSet();
            string dbstr = "Data Source=.;Initial Catalog=FaceSign;Integrated Security=True";
            string sql = "select id,create_time,update_time,feature,image,stu_info_id,name FROM checkInfo";
            SqlDataAdapter adapter = new SqlDataAdapter(sql,dbstr);
            adapter.Fill(set);
            string name = set.Tables[0].Rows[0][0].ToString();
            MessageBox.Show(name);

            DataTable dt = set.Tables[0];
            DataRow row = dt.Rows[0];
            string s = row[1].ToString();
            string s1 = row["stu_info_id"].ToString();

        }


        // 查找按钮事件
        private void btnCMSearch_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=.;Initial Catalog=FaceSign;Integrated Security=True");
            conn.Open();

            // DataSet set = new DataSet();
            // string dbstr = "Data Source=.;Initial Catalog=FaceSign;Integrated Security=True";

            string strSQL = "select id,create_time,update_time,feature,image,stu_info_id,name FROM checkInfo WHERE name LIKE'" + txtCIName.Text.Trim() + "%'"; ;
            if (txtCMNumber.Text.Trim() != "")
            {
                //strSQL = "select * FROM checkInfo WHERE stu_info_id LIKE'" + txtCMNumber.Text.Trim() + "%'";
                String str = "AND stu_info_id LIKE'" + txtCMNumber.Text.Trim() + "%'";
                strSQL += str;
            }

            // SqlDataAdapter adapter = new SqlDataAdapter(strSQL,dbstr);
            // adapter.Fill(set);
            // dataGridView1CI.DataSource = set.Tables["checkInfo"];

            db.RunNonSelect(strSQL);
            /* DataSet  */ds = db.getDataSet(strSQL, "checkInfo");

            dt = ds.Tables["checkInfo"];
            // dataGridView1CI.DataSource = ds;
            // dataGridView1CI.DataMember = "checkInfo";
            dataGridView1CI.DataSource = dt;
        }

        // 选择相片按钮事件
        string fileName = string.Empty;
        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                image = new Bitmap(openFileDialog1.FileName);
                pictureBox图像.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox图像.Image = image;
                pictureBox图像.ImageLocation = fileName;
                
            }

        }

        // 保存相片按钮事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable changeDt = dt.GetChanges();
            DateTime dtime = DateTime.Now.ToLocalTime();
            string time = dtime.ToString("yyyy-MM-dd HH:mm:ss");
            int id = Convert.ToInt32(dataGridView1CI.CurrentRow.Cells[0].Value.ToString());

            // ========================================================================================

            //FileStream fs = new FileStream(fileName, FileMode.Open);
            //byte[] imageBytes = new byte[fs.Length];
            //BinaryReader br = new BinaryReader(fs);
            //imageBytes = br.ReadBytes(Convert.ToInt32(fs.Length));//图片转换成二进制流
            //string str = Convert.ToBase64String(imageBytes);//二进制转成base64字符串


            //MemoryStream ms = new MemoryStream();
            //pictureBox图像.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //byte[] bt = new byte[1];
            ////转为字节
            //bt = ms.GetBuffer();
            //DataRow dr = dt.NewRow();
            ////对应字段
            //dr["image"] = bt;
            ////执行插入
            //dt.Rows.Add(dr);

            // ========================================================================================

            //if (changeDt == null)
            //{
            //    MessageBox.Show("没有执行任何操作.");
            //}
            //else
            //{
            //    foreach (DataRow dr in changeDt.Rows)
            //    {

            //        string strSQL = string.Empty;
            //        if (dr.RowState == System.Data.DataRowState.Modified) {
            //            strSQL = @"UPDATE [dbo].[CheckInfo] SET [update_time] = '" + time + @"'
            //                              ,[image] = '" + bt + @"'
            //                              WHERE id = '" + Convert.ToInt32(dr["id"]) + @"' ";

            //        }

            //        SqlCommand comm = new SqlCommand(strSQL, conn);
            //        try
            //        {
            //            comm.ExecuteNonQuery();
            //        }
            //        catch (Exception o)
            //        {
            //            MessageBox.Show(o.Message, "操作失败。");
            //        }
            //        save();

            //    }

            //}

                    


            


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

        private void dataGridView1CI_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1CI.Columns[e.ColumnIndex].Name.Equals("image"))
            {
                string path = e.Value.ToString();
                e.Value = GetImage(path);
            }

        }

        public byte[] imgToByte(Image b)
        {
            MemoryStream ms = new MemoryStream();
            b.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] bytes = ms.GetBuffer();
            ms.Close();
            return bytes;
        }

        public static Image BytesToImage(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }

        private void dataGridView1CI_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //imgToByte(BytesToImage((byte[])dataGridView1CI.CurrentRow.Cells[4].Value));
            //BytesToImage((byte[])dataGridView1CI.CurrentRow.Cells[4].Value);
            //GetImage(fileName);

            DataTable changeDt = dt.GetChanges();
            //DataRow dr = changeDt.AsEnumerable();
            DataRow dr;
            try
            {
                foreach (DataRow dr1 in changeDt.Rows)
                {
                    if (e.RowIndex < dt.Rows.Count)
                    {
                        dr = dt.Rows[e.RowIndex];
                        byte[] bt = (byte[])dr["image"];
                        MemoryStream ms = new MemoryStream(bt);
                        pictureBox图像.Image = new Bitmap(ms);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView1CI_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public System.Drawing.Image GetImage(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
            System.Drawing.Image result = System.Drawing.Image.FromStream(fs);

            fs.Close();

            return result;

        }

        public Image GetImage(byte[] str)
        {
            MemoryStream ms = new MemoryStream(str);
            Image img = System.Drawing.Image.FromStream(ms);
            return img;
        }

    }
}
