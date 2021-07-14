using SqlSugar;
using System;
using System.Collections.Generic;
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

namespace ExpressionExplorer
{
    public class Thing
    {
        public Thing()
        {
            Id = Guid.NewGuid().ToString();
            Created = DateTimeOffset.Now;
            Name = Guid.NewGuid().ToString().Split("-")[0];
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; private  set; }

        public string GetId() => Id;

        public static IList<Thing> Things(int count)
        {
            var things = new List<Thing>();
            while (count -- > 0)
            {
                things.Add(new Thing());
            }

            return things;
        }

        public override string ToString() => $"({Id}:{Name}@{Created})";
    }
}
