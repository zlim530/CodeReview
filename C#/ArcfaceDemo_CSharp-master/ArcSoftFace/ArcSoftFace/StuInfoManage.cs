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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=FaceSign;Integrated Security=True");
            conn.Open();
            string strSQL = "select * FROM stuInfo WHERE name LIKE'" + txtSearch.Text.Trim() + "%'";
            if ( txtNumber.Text.Trim() != "") {
                String str = "AND id LIKE'" + txtNumber.Text.Trim() + "%'";
                strSQL += str;
            }
            db.RunNonSelect(strSQL);
            DataSet ds = db.getDataSet(strSQL, "stuInfo");
            dataGridView1Stu.DataSource = ds;
            dataGridView1Stu.DataMember = "stuInfo";


        }

    }
}
