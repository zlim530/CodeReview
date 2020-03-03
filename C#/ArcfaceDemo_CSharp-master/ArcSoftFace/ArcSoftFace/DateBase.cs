using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace
{
    class DateBase
    {
        public static string userName = "";
        public static string userPassword = "";
        public static SqlConnection myConnection;
        public static string connectionString = "Data Source=.;Initial Catalog=FaceSign;Integrated Security=True";//Properties.Settings.Default.OrderSystemConnectionString;
        public static SqlConnection getConnection()
        {
            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            return myConnection;

        }

        public void close_Conn()
        {
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
                myConnection.Dispose();
            }

        }
        public SqlDataReader getDataReader(string sqlStr)
        {
            getConnection();
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = sqlStr;
            SqlDataReader myread = myCommand.ExecuteReader();
            return myread;
        }

        public void RunNonSelect(string sqlStr)
        {
            getConnection();
            SqlCommand cmd = new SqlCommand(sqlStr, myConnection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            close_Conn();
        }
        public DataSet getDataSet(string sqlStr, string tableName)
        {
            getConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, myConnection);
            DataSet myDataSet = new DataSet();
            adapter.Fill(myDataSet, tableName);
            close_Conn();
            return myDataSet;

        }
    }
}
