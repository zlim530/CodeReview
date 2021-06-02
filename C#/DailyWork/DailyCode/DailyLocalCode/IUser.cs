using Npgsql;
using System;

namespace DailyLocalCode
{
    public interface IUser
    {
        /*
        默认接口方法
        接口是用来约束行为的，在 C# 8 以前，接口中只能进行方法的定义，下面的代码在 C# 8 以前是会报编译错误的：
        */
        string GetName() => "ZLim530";
        /*
        接口默认方法最大的好处是，当在接口中进行方法扩展时，之前的实现类可以不受影响，而在 C# 8 之前，接口中如果要添加方法，所有的实现类需要进行新增接口方法的实现，否则编译失败。 
        */
    }

    public interface IA
    {
        void Test() => Console.WriteLine("Invoke IA.Test");
    }

    public interface IB : IA
    {
        void Test() => Console.WriteLine("Invoke IB.Test");
    }
    public interface IC : IA
    {
        void Test() => Console.WriteLine("Invoke IC.Test");
    }

    public class D : IB, IC { }

    public class E : IB, IC
    {
        public void Test() => Console.WriteLine("Invoke E.Test");
    }

    public class A
    {
        public void Test() => Console.WriteLine("Invoke A.Test");
    }

    public class F : A, IA { }

    public class DailyLocalCode
    {
        /// <summary>
        /// 默认接口方法
        /// </summary>
        /// <param name="args"></param>
        static void Main0(string[] args)
        {
            //接口是用来约束行为的，在 C# 8 以前，接口中只能进行方法的定义，下面的代码在 C# 8 以前是会报编译错误的：
            D d = new D();
            //d.Test();
            //上面的代码是无法通过编译的，因为接口的默认方法不能被继承，所以类 D 中没有 Test 方法可以调用
            //所以，必须通过接口类型来进行相关方法的调用：
            IA d1 = new D();
            IB d2 = new D();
            IC d3 = new D();
            d1.Test();  // Invoke IA.Test
            d2.Test();  // Invoke IB.Test
            d3.Test();  // Invoke IC.Test

            //也正是因为必须通过接口类型来进行调用，所以也就不存在菱形问题。而当具体的类中有对接口方法实现的时候，就会调用类上实现的方法：
            IA e1 = new E();
            IB e2 = new E();
            IC e3 = new E();
            e1.Test();  // Invoke E.Test
            e2.Test();  // Invoke E.Test
            e3.Test();  // Invoke E.Test

            //类可能同时继承类和接口，这时会优先调用类中的方法：
            F f = new F();
            IA f2 = new F();
            f.Test();   // Invoke A.Test
            f2.Test();  // Invoke A.Test
            /*
            关于默认接口方法，总结如下：

            默认接口方法可以让我们在往底层接口中扩展方法的时候变得比较平滑；
            默认方法，会优先调用类中的实现，如果类中没有实现，才会去调用接口中的默认方法；
            默认方法不能够被继承，当类中没有自己实现的时候是不能从类上直接调用的。 
            */
        }

        /// <summary>
        /// using 变量声明
        /// </summary>
        /// <param name="args"></param>
        static void Main1(string[] args)
        {
            //var connString = "Host=localhost;Useranme=gpadmin;Password=123456;Database=postgres;Port=54320";
            //using (var conn = new NpgsqlConnection(connString))
            //{
            //    conn.Open();

            //    using (var cmd = new NpgsqlCommand("select * from user_test", conn))
            //    {
            //        using (var reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                Console.WriteLine(reader["user_name"]);
            //            }
            //        }
            //    }
            //}
            //使用 using 变量声明后的代码如下：
            var connString = "Host=localhost;Useranme=gpadmin;Password=123456;Database=postgres;Port=54320";
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using var cmd = new NpgsqlCommand("",conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
                Console.WriteLine(reader["user_name"]);
            Console.ReadKey();
        }
    }
}
