

using System.Data;
using System.Data.SqlClient;

namespace FirstConnect
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }


    class Express
    {
        public string connString = "Data Source=xp;Initial Catalog=ExpressManager;Integrated Security=TRUE";
        //创建连接对象的变量  
        public SqlConnection conn;
        // 执行对数据表中数据的增加、删除、修改操作  
        public int NonQuery(string sql)
        {
            conn = new SqlConnection(connString);
            int a = -1;
            try
            {
                conn.Open();  //打开数据库  
                SqlCommand cmd = new SqlCommand(sql, conn);
                a = cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();    //关闭数据库  
                }
            }
            return a;

        }
        // 执行对数据表中数据的查询操作  
        public DataSet Query(string sql)
        {
            conn = new SqlConnection(connString);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();      //打开数据库  
                SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
                adp.Fill(ds);
            }
            catch
            {

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();      //关闭数据库  
            }
            return ds;
        }
    }

}
