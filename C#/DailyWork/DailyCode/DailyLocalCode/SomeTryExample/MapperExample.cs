using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using cuyan.orm;

namespace DailyLocalCode.SomeTryExample
{
    public class TestName
    {
        public string Name { get; set; }
    }

    public class Student
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public bool IsGraduate { get; set; }
    }

    public class StudentModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public int IsGraduate { get; set; }
    }

    public class MapperExample
    {
        static void Main0(string[] args)
        {
            var _db = new CuyanClient()
            {
                ConnectionString = "Server=localhost;Initial Catalog=Test;Integrated Security=True"
            };

            var colorTemp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"======== 新增 =======");
            Console.ForegroundColor = colorTemp;
            if (true)
            {
                var result = _db.Add(new Student() { Gender = 1,Name = "Tom",IsGraduate = true});
                if (result > 0)
                    Console.WriteLine($"添加成功！受影响数据：{result}条。");
                else
                    Console.WriteLine($"添加失败！受影响数据：{result}条。");
            }
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"======== 查询 =======");
            Console.ForegroundColor = colorTemp;
            if (true)
            {
                var students = _db.Query<Student>("SELECT * FROM[Test].[dbo].[student]",null);
                if (students != null && students.Count > 0)
                {
                    Console.WriteLine($"查询到数据{students.Count}条：");
                    Console.WriteLine($"展示第一条：名字：{students[0].Name}，是否毕业：{students[0].IsGraduate}");
                    Console.WriteLine($"展示第二条：名字：{students[1].Name}，是否毕业：{students[1].IsGraduate}");
                }
            }
            Console.WriteLine("\n");
            Console.ReadLine();
        }
    }

    public static class Mapper
    {
        public static T1 Map<T, T1>(T t)
        {
            var source = Activator.CreateInstance(typeof(T));
            var result = Activator.CreateInstance(typeof(T1));
            if (source.GetType().Name == "List`1" || result.GetType().Name == "List`1")
            {
                throw new Exception("形参有误！请使用对象！");
            }
            var tpropertyInfos = source.GetType().GetProperties();
            var t1propertyInfos = result.GetType().GetProperties();
            foreach (var tinfo in tpropertyInfos)
            {
                foreach (var t1info in t1propertyInfos)
                {
                    if (tinfo.Name == t1info.Name)
                    {
                        try
                        {
                            object value = tinfo.GetValue(t,null);
                            var property = typeof(T1).GetProperty(tinfo.Name);
                            if (property != null && property.CanWrite && !(value is DBNull))
                            {
                                property.SetValue(result, value, null);
                            }
                        }
                        catch 
                        {
                        }
                    }
                }
            }
            
            return (T1)result;
        }

        public static List<T1> MapList<T, T1>(List<T> t)
        {
            List<T1> result = new List<T1>();
            if ( t == null)
            {
                throw new Exception("未将对象引用设置到对象的实例。");
            }
            foreach (var item in t)
            {
                result.Add(Map<T, T1>(item));
            }
            return result;
        }
    }

}



namespace cuyan.orm
{
    public class CuyanClient : SqlRun
    {
        public override string ConnectionString { get; set; }
        #region Tool
        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }
            return ConvertTo<T>(rows);
        }
        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;
            if (rows != null)
            {
                list = new List<T>();
                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }
            return list;
        }
        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();
                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        object value = row[column.ColumnName];
                        prop.SetValue(obj, value, null);
                    }
                    catch
                    {
                        //throw;    
                    }
                }
            }
            return obj;
        }
        /// <summary>
        /// Gets the sp pkeys.
        /// </summary>
        /// <param name="tname">The tname.</param>
        /// <returns></returns>
        public List<SP_PKEYS> GetSP_PKEYS(string tname)
        {
            //查询标识列
            return Query<SP_PKEYS>($"EXEC SP_PKEYS @table_name = '{tname}'", null);
        }
        /// <summary>
        /// 确定指定的标识规范是否为标识规范
        /// </summary>
        /// <param name="IsIdentitys">The is identitys.</param>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if the specified is identitys is identity; otherwise, <c>false</c>.
        /// </returns>
        private bool IsPK(List<SP_PKEYS> PKeyss, string name)
        {
            if (PKeyss != null)
            {
                foreach (var PKeys in PKeyss)
                {
                    if (PKeys.COLUMN_NAME.ToLower() == name.ToLower())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Gets the information schemacolumns.
        /// </summary>
        /// <param name="tname">The tname.</param>
        /// <param name="COLUMN_NAME">Name of the column.</param>
        /// <returns></returns>
        public List<TableInfo> GetINFORMATION_SCHEMACOLUMNS(string tname, string COLUMN_NAME)
        {
            //查询标识列
            return Query<TableInfo>($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tname}' AND  COLUMNPROPERTY(OBJECT_ID('{tname}'), COLUMN_NAME, '{COLUMN_NAME}') = 1", null);
        }
        /// <summary>
        /// 确定指定的标识规范是否为标识规范
        /// </summary>
        /// <param name="IsIdentitys">The is identitys.</param>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if the specified is identitys is identity; otherwise, <c>false</c>.
        /// </returns>
        private bool IsIdentity(List<TableInfo> IsIdentitys, string name)
        {
            if (IsIdentitys != null)
            {
                foreach (var Identity in IsIdentitys)
                {
                    if (Identity.COLUMN_NAME.ToLower() == name.ToLower())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
        #region 获取sql语句
        public string GetAddSql<T>(T t)
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            string sql = $"INSERT INTO {t.GetType().Name} ";
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties();
            if (properties.Length <= 0)
                throw new Exception("该类成员数为 0 。");
            var tname = t.GetType().Name;
            //查询标识列
            var IsIdentitys = Query<TableInfo>($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.columns WHERE TABLE_NAME = '{tname}' AND  COLUMNPROPERTY(OBJECT_ID('{tname}'), COLUMN_NAME, 'IsIdentity') = 1", null);
            string paramstr = " (";
            string paramValuestr = " VALUES (";
            bool fasheng = false;
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                if (IsIdentity(IsIdentitys, name))
                {
                    continue;
                }
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    if (fasheng)
                    {
                        paramstr += ",";
                        paramValuestr += ",";
                    }
                    fasheng = true;
                    paramstr += name;
                    paramValuestr += $"@{name}";
                    parms.Add(new SqlParameter() { ParameterName = name, Value = value });
                }
            }
            paramstr += ")";
            paramValuestr += ")";
            sql = sql + paramstr + paramValuestr;
            string parmstemp = "{";
            for (int i = 0; i < parms.Count; i++)
            {
                if (i > 0)
                {
                    parmstemp += ",";
                }
                SqlParameter item = parms[i];
                parmstemp += $"{item.ParameterName}:{item.Value}";
            }
            parmstemp += "}";
            return $"{parmstemp}" + sql;
        }
        public void GetAddSql<T>(T t, out string sql, out List<SqlParameter> parms)
        {
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties();
            if (properties.Length <= 0)
                throw new Exception("该类成员数为 0 。");
            sql = $"INSERT INTO {t.GetType().Name} ";
            parms = new List<SqlParameter>();
            var tname = t.GetType().Name;
            //查询标识列
            var IsIdentitys = GetINFORMATION_SCHEMACOLUMNS(tname, "IsIdentity");
            string paramstr = " (";
            string paramValuestr = " VALUES (";
            bool fasheng = false;
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                if (IsIdentity(IsIdentitys, name))
                {
                    continue;
                }
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    if (value != null)
                    {
                        if (fasheng)
                        {
                            paramstr += ",";
                            paramValuestr += ",";
                        }
                        fasheng = true;
                        paramstr += name;
                        paramValuestr += $"@{name}";
                        parms.Add(new SqlParameter() { ParameterName = name, Value = value });
                    }
                }
            }
            paramstr += ")";
            paramValuestr += ")";
            sql = sql + paramstr + paramValuestr;
        }
        public void GetUpdateSql<T>(T t, out string sql, out List<SqlParameter> parms)
        {
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties();
            if (properties.Length <= 0)
                throw new Exception("该类成员数为 0 。");
            sql = $"UPDATE {t.GetType().Name} ";
            parms = new List<SqlParameter>();
            var tname = t.GetType().Name;
            var pks = GetSP_PKEYS(tname);
            string paramSet = " SET ";
            string paramWhere = " WHERE ";
            bool fasheng1 = false;
            bool fasheng2 = false;
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    if (!IsPK(pks, name))
                    {
                        if (fasheng1)
                            paramSet += ",";
                        paramSet += $"{name} = @{name}";
                        fasheng1 = true;
                    }
                    else
                    {
                        if (fasheng2)
                            paramWhere += ",";
                        paramWhere += $"{name} = @{name}";
                        fasheng2 = true;
                    }
                    parms.Add(new SqlParameter() { ParameterName = name, Value = value });
                }
            }
            sql = sql + paramSet + paramWhere;
        }

        public void GetDeleteSql<T>(T t, out string sql, out List<SqlParameter> parms)
        {
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties();
            if (properties.Length <= 0)
                throw new Exception("该类成员数为 0 。");
            sql = $"DELETE {t.GetType().Name} ";
            parms = new List<SqlParameter>();
            var tname = t.GetType().Name;
            var pks = GetSP_PKEYS(tname);
            string paramWhere = " WHERE ";
            bool fasheng = false;
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    if (IsPK(pks, name))
                    {
                        if (fasheng)
                            paramWhere += ",";
                        paramWhere += $"{name} = @{name}";
                        fasheng = true;
                    }
                    parms.Add(new SqlParameter() { ParameterName = name, Value = value });
                }
            }
            sql = sql + paramWhere;
        }
        #endregion
        #region 操作数据

        /// <summary>
        /// 查询数据，返回List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public List<T> Query<T>(string sql, SqlParameter[] parms = null)
        {
            var data = QueryDataTable(sql, parms, CommandType.Text);
            return data.Rows.Count > 0 ? ConvertTo<T>(data) as List<T> : default;
        }
        /// <summary>
        /// 新增数据，sql语句
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public int Add(string sql, SqlParameter[] parms = null)
        {
            var result = Execute(sql, parms, CommandType.Text);
            return result;
        }
        /// <summary>
        /// 新增数据，对象数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">未将对象引用设置到对象的实例</exception>
        public int Add<T>(T t)
        {
            if (t == null)
                throw new Exception("未将对象引用设置到对象的实例");
            GetAddSql(t, out string sql, out List<SqlParameter> parms);
            return Execute(sql, parms.ToArray(), CommandType.Text);
        }
        /// <summary>
        /// 修改数据，根据主键
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">未将对象引用设置到对象的实例</exception>
        public int Update<T>(T t)
        {
            if (t == null)
                throw new Exception("未将对象引用设置到对象的实例");
            GetUpdateSql(t, out string sql, out List<SqlParameter> parms);
            return Execute(sql, parms.ToArray(), CommandType.Text);
        }
        /// <summary>
        /// 删除数据，根据主键
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">未将对象引用设置到对象的实例</exception>
        public int Delete<T>(T t)
        {
            if (t == null)
                throw new Exception("未将对象引用设置到对象的实例");
            GetDeleteSql(t, out string sql, out List<SqlParameter> parms);
            return Execute(sql, parms.ToArray(), CommandType.Text);
        }
        #endregion
    }


    public class SqlRun
    {
        // 超时时间
        private static int Timeout = 1000;

        public virtual string ConnectionString { get; set; }


        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="database">数据库(配置文件内connectionStrings的name)</param>
        /// <returns>数据库连接</returns>
        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// 获取SqlCommand
        /// </summary>
        /// <param name="conn">SqlConnection</param>
        /// <param name="transaction">SqlTransaction</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="sql">SQL</param>
        /// <param name="parms">SqlParameter数组</param>
        /// <returns></returns>
        private SqlCommand GetCommand(SqlConnection conn, SqlTransaction transaction, CommandType cmdType, string sql, SqlParameter[] parms)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = Timeout;
            if (transaction != null)
                cmd.Transaction = transaction;
            if (parms != null && parms.Length != 0)
                cmd.Parameters.AddRange(parms);
            return cmd;
        }

        /// <summary>
        /// 查询数据，返回DataTable
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <returns>DataTable</returns>
        public DataTable QueryDataTable(string sql, SqlParameter[] parms, CommandType cmdType)
        {
            if (string.IsNullOrEmpty(sql) || (sql + "").Trim() == "")
            {
                throw new Exception("未设置参数：sql");
            }

            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();

                    using (SqlCommand cmd = GetCommand(conn, null, cmdType, sql, parms))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Text.StringBuilder log = new System.Text.StringBuilder();
                log.Append("查询数据出错：");
                log.Append(ex);
                throw new Exception(log.ToString());
            }
        }

        /// <summary>
        /// 查询数据，返回DataSet
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <returns>DataSet</returns>
        public DataSet QueryDataSet(string sql, SqlParameter[] parms, CommandType cmdType)
        {
            if (string.IsNullOrEmpty(sql) || (sql + "").Trim() == "")
            {
                throw new Exception("未设置参数：sql");
            }

            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();

                    using (SqlCommand cmd = GetCommand(conn, null, cmdType, sql, parms))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            return ds;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Text.StringBuilder log = new System.Text.StringBuilder();
                log.Append("查询数据出错：");
                log.Append(ex);
                throw new Exception(log.ToString());
            }
        }

        /// <summary>
        /// 执行命令获取唯一值(第一行第一列)
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <returns>获取值</returns>
        public object QueryScalar(string sql, SqlParameter[] parms, CommandType cmdType)
        {
            if (string.IsNullOrEmpty(sql) || (sql + "").Trim() == "")
            {
                throw new Exception("未设置参数：sql");
            }
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();

                    using (SqlCommand cmd = GetCommand(conn, null, cmdType, sql, parms))
                    {
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Text.StringBuilder log = new System.Text.StringBuilder();
                log.Append("处理出错：");
                log.Append(ex);
                throw new Exception(log.ToString());
            }
        }

        /// <summary>
        /// 执行命令更新数据
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <returns>更新的行数</returns>
        public int Execute(string sql, SqlParameter[] parms, CommandType cmdType)
        {
            if (string.IsNullOrEmpty(sql) || (sql + "").Trim() == "")
            {
                throw new Exception("未设置参数：sql");
            }

            //返回(增删改)的更新行数
            int count = 0;

            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();

                    using (SqlCommand cmd = GetCommand(conn, null, cmdType, sql, parms))
                    {
                        if (cmdType == CommandType.StoredProcedure)
                            cmd.Parameters.AddWithValue("@RETURN_VALUE", "").Direction = ParameterDirection.ReturnValue;

                        count = cmd.ExecuteNonQuery();

                        if (count <= 0)
                            if (cmdType == CommandType.StoredProcedure)
                                count = (int)cmd.Parameters["@RETURN_VALUE"].Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Text.StringBuilder log = new System.Text.StringBuilder();
                log.Append("处理出错：");
                log.Append(ex);
                throw new Exception(log.ToString());
            }
            return count;
        }

        /// <summary>
        /// 查询数据，返回DataTable
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <param name="values">参数</param>
        /// <returns>DataTable</returns>
        public DataTable QueryDataTable(string sql, CommandType cmdType, IDictionary<string, object> values)
        {
            SqlParameter[] parms = DicToParams(values);
            return QueryDataTable(sql, parms, cmdType);
        }

        /// <summary>
        /// 执行存储过程查询数据，返回DataSet
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <param name="values">参数
        /// <returns>DataSet</returns>
        public DataSet QueryDataSet(string sql, CommandType cmdType, IDictionary<string, object> values)
        {
            SqlParameter[] parms = DicToParams(values);
            return QueryDataSet(sql, parms, cmdType);
        }

        /// <summary>
        /// 执行命令获取唯一值(第一行第一列)
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <param name="values">参数</param>
        /// <returns>唯一值</returns>
        public object QueryScalar(string sql, CommandType cmdType, IDictionary<string, object> values)
        {
            SqlParameter[] parms = DicToParams(values);
            return QueryScalar(sql, parms, cmdType);
        }

        /// <summary>
        /// 执行命令更新数据
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句或存储过程名</param>
        /// <param name="cmdType">查询类型(SQL语句/存储过程名)</param>
        /// <param name="values">参数</param>
        /// <returns>更新的行数</returns>
        public int Execute(string sql, CommandType cmdType, IDictionary<string, object> values)
        {
            SqlParameter[] parms = DicToParams(values);
            return Execute(sql, parms, cmdType);
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="type">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="direction">参数方向(输入/输出)</param>
        /// <param name="value">参数值</param>
        /// <returns>新参数对象</returns>
        public static SqlParameter[] DicToParams(IDictionary<string, object> values)
        {
            if (values == null) return null;

            SqlParameter[] parms = new SqlParameter[values.Count];
            int index = 0;
            foreach (KeyValuePair<string, object> kv in values)
            {
                SqlParameter parm = null;
                if (kv.Value == null)
                {
                    parm = new SqlParameter(kv.Key, DBNull.Value);
                }
                else
                {
                    Type t = kv.Value.GetType();
                    parm = new SqlParameter(kv.Key, NetToSql(kv.Value.GetType()));
                    parm.Value = kv.Value;
                }

                parms[index++] = parm;
            }
            return parms;
        }


        /// <summary>
        /// .net类型转换为Sql类型
        /// </summary>
        /// <param name="t">.net类型</param>
        /// <returns>Sql类型</returns>
        public static SqlDbType NetToSql(Type t)
        {
            SqlDbType dbType = SqlDbType.Variant;
            switch (t.Name)
            {
                case "Int16":
                    dbType = SqlDbType.SmallInt;
                    break;
                case "Int32":
                    dbType = SqlDbType.Int;
                    break;
                case "Int64":
                    dbType = SqlDbType.BigInt;
                    break;
                case "Single":
                    dbType = SqlDbType.Real;
                    break;
                case "Decimal":
                    dbType = SqlDbType.Decimal;
                    break;

                case "Byte[]":
                    dbType = SqlDbType.VarBinary;
                    break;
                case "Boolean":
                    dbType = SqlDbType.Bit;
                    break;
                case "String":
                    dbType = SqlDbType.NVarChar;
                    break;
                case "Char[]":
                    dbType = SqlDbType.Char;
                    break;
                case "DateTime":
                    dbType = SqlDbType.DateTime;
                    break;
                case "DateTime2":
                    dbType = SqlDbType.DateTime2;
                    break;
                case "DateTimeOffset":
                    dbType = SqlDbType.DateTimeOffset;
                    break;
                case "TimeSpan":
                    dbType = SqlDbType.Time;
                    break;
                case "Guid":
                    dbType = SqlDbType.UniqueIdentifier;
                    break;
                case "Xml":
                    dbType = SqlDbType.Xml;
                    break;
                case "Object":
                    dbType = SqlDbType.Variant;
                    break;
            }
            return dbType;
        }

    }


    public class TableInfo
    {
        /// <summary>
        /// 表目录(数据库名称)
        /// </summary>
        /// <value>
        /// The table catalog.
        /// </value>
        public string TABLE_CATALOG { get; set; }
        /// <summary>
        /// 表架构()
        /// </summary>
        /// <value>
        /// The table schema.
        /// </value>
        public string TABLE_SCHEMA { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TABLE_NAME { get; set; }
        /// <summary>
        /// 列名称（字段名）
        /// </summary>
        /// <value>
        /// The name of the column.
        /// </value>
        public string COLUMN_NAME { get; set; }
        /// <summary>
        /// 序数位置
        /// </summary>
        /// <value>
        /// The ordinal position.
        /// </value>
        public string ORDINAL_POSITION { get; set; }


        /// <summary>
        /// 列默认值
        /// </summary>
        /// <value>
        /// The column default.
        /// </value>
        public string COLUMN_DEFAULT { get; set; }

        /// <summary>
        /// 是可为空的吗（是否为空）
        /// </summary>
        /// <value>
        /// The is nullable.
        /// </value>
        public string IS_NULLABLE { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public string DATA_TYPE { get; set; }

        /// <summary>
        /// 字符最大长度
        /// </summary>
        /// <value>
        /// The table catalog.
        /// </value>
        public string CHARACTER_MAXIMUM_LENGTH { get; set; }
        /// <summary>
        /// 字符八位字节的长度
        /// </summary>
        /// <value>
        /// The length of the character octet.
        /// </value>
        public string CHARACTER_OCTET_LENGTH { get; set; }
        /// <summary>
        /// 数字精度
        /// </summary>
        /// <value>
        /// The numeric precision.
        /// </value>
        public string NUMERIC_PRECISION { get; set; }
        /// <summary>
        /// 数字精度基数
        /// </summary>
        /// <value>
        /// The numeric precision radix.
        /// </value>
        public string NUMERIC_PRECISION_RADIX { get; set; }
        /// <summary>
        /// 数字刻度
        /// </summary>
        /// <value>
        /// The numeric scale.
        /// </value>
        public string NUMERIC_SCALE { get; set; }
        /// <summary>
        /// 日期时间精度
        /// </summary>
        /// <value>
        /// The datetime precision.
        /// </value>
        public string DATETIME_PRECISION { get; set; }
        /// <summary>
        /// 字符集目录
        /// </summary>
        /// <value>
        /// The character set catalog.
        /// </value>
        public string CHARACTER_SET_CATALOG { get; set; }
        /// <summary>
        /// 字符集架构
        /// </summary>
        /// <value>
        /// The character set schema.
        /// </value>
        public string CHARACTER_SET_SCHEMA { get; set; }
        /// <summary>
        /// 字符集的名称
        /// </summary>
        /// <value>
        /// The name of the character set.
        /// </value>
        public string CHARACTER_SET_NAME { get; set; }
        /// <summary>
        /// 排序规则目录
        /// </summary>
        /// <value>
        /// The collation catalog.
        /// </value>
        public string COLLATION_CATALOG { get; set; }
        /// <summary>
        /// 排序规则架构
        /// </summary>
        /// <value>
        /// The collation schema.
        /// </value>
        public string COLLATION_SCHEMA { get; set; }
        /// <summary>
        /// 排序规则的名称
        /// </summary>
        /// <value>
        /// The name of the collation.
        /// </value>
        public string COLLATION_NAME { get; set; }
        /// <summary>
        /// 域目录
        /// </summary>
        /// <value>
        /// The domain catalog.
        /// </value>
        public string DOMAIN_CATALOG { get; set; }
        /// <summary>
        /// 域架构
        /// </summary>
        /// <value>
        /// The domain schema.
        /// </value>
        public string DOMAIN_SCHEMA { get; set; }
        /// <summary>
        /// 域的名称
        /// </summary>
        /// <value>
        /// The name of the domain.
        /// </value>
        public string DOMAIN_NAME { get; set; }
    }


    public class SP_PKEYS
    {
        /// <summary>
        /// 表限定符
        /// </summary>
        /// <value>
        /// The table qualifier.
        /// </value>
        public string TABLE_QUALIFIER { get; set; }

        /// <summary>
        /// 表所有者
        /// </summary>
        /// <value>
        /// The table owner.
        /// </value>
        public string TABLE_OWNER { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TABLE_NAME { get; set; }
        /// <summary>
        /// 列名称（字段名）
        /// </summary>
        /// <value>
        /// The name of the column.
        /// </value>
        public string COLUMN_NAME { get; set; }
        /// <summary>
        /// 序数位置
        /// </summary>
        /// <value>
        /// The ordinal position.
        /// </value>
        public string KEY_SEQ { get; set; }

        /// <summary>
        /// 主键名称
        /// </summary>
        /// <value>
        /// The name of the pk.
        /// </value>
        public string PK_NAME { get; set; }
    }
}