using SqlSugar;
using System;
using System.Threading.Tasks;

namespace DailyLocalCode
{
    public class SQLSugarExample
    {
        static void Main0(string[] args)
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Data Source=.;Initial Catalog=Test;Integrated Security=True",
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            });
        }

        static void Main1(string[] args)
        {
            Closure1();
        }

        static void Closure1()
        {
            for (int i = 0; i < 5; i++)
            {
                int j = i;
                //Task.Run(() => Console.WriteLine(i));
                Task.Run(() => Console.WriteLine(j));
            }
        }

    }
}
