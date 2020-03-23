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
    public partial class CheckInfoAdd : Form
    {
        public CheckInfoAdd()
        {
            InitializeComponent();
        }

        Bitmap image;
        DateBase db = new DateBase();
        SqlConnection conn;
        DataTable dt;

        private void btnSave_Click(object sender, EventArgs e)
        {

            DateTime dtime = DateTime.Now.ToLocalTime();
            string time = dtime.ToString("yyyy-MM-dd HH:mm:ss");

            string txtname = txtName.ToString().Trim();
            string txtnumber = txtNumber.ToString().Trim();

            string dbstr = "Data Source=.;Initial Catalog=FaceSign;Integrated Security=True";
            conn = new SqlConnection(dbstr);
            conn.Open();

            string sql = "insert into stuInfo(create_time,update_time,feature,image,stu_info_id,name) values(@create_time,@update_time,@feature,@image,@stu_info_id,@name)";

            SqlParameter[] ps = {
                new SqlParameter("@create_time",time),
                new SqlParameter("@update_time",time),
                new SqlParameter("@feature",null),
                new SqlParameter("@image",image),
                new SqlParameter("@stu_info_id",txtnumber),
                new SqlParameter("@name",txtname),
            };
            SqlCommand cmd = new SqlCommand(sql,conn);

            cmd.Parameters.AddRange(ps);
            int r = cmd.ExecuteNonQuery();

            if (r == 1)
            {
                MessageBox.Show("添加成功");
                this.Close();
            }
            else {
                MessageBox.Show("添加失败");
            }
            
        }

        string fileName = string.Empty;
        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                image = new Bitmap(openFileDialog1.FileName);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Image = image;
                pictureBox.ImageLocation = fileName;

            }
        }


    }
}
