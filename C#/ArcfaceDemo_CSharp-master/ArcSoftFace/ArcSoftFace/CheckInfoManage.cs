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

        DateBase db = new DateBase();
        SqlConnection conn;
        DataTable dt;




    }
}
